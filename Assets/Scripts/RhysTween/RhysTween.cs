using System;
using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace RhysTween {
  internal class RunState {
    public float DeltaTime;
    public EcsFilter GroupFilter;
  }

  public struct Tween {
    readonly internal EcsPackedEntity _entity;

    public Tween(EcsPackedEntity entity) {
      _entity = entity;
    }
  }

  public static class RhysTween {
#region Tween factories

    public static Tween TRotation(this Transform transform, Quaternion endValue, float duration) =>
      Tween(transform.rotation, v => transform.rotation = v, endValue, duration);

    public static Tween TRotation(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.eulerAngles, v => transform.eulerAngles = v, endValue, duration);

    public static Tween TLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Tween(transform.localRotation, v => transform.localRotation = v, endValue, duration);

    public static Tween TLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.localEulerAngles, v => transform.localEulerAngles = v, endValue, duration);

    public static Tween TPosition(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.position, v => transform.position = v, endValue, duration);

    public static Tween TLocalPosition(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.localPosition, v => transform.localPosition = v, endValue, duration);

    public static Tween Tween<T>(T from, Action<T> onChange, T to, float duration) {
      var entity = _world.NewEntity();
      _world.AddComponent(entity, new TweenConfig<T>(from, to, onChange));
      _world.AddComponent(entity, new TweenState(duration));
      _world.AddComponent<Update>(entity);
      return new Tween(_world.PackEntity(entity));
    }

#endregion
#region Pause

    public static bool IsPaused(this Tween tween) =>
      Entity(tween, out var entity) && IsPaused(entity);

    public static void Pause(this Tween tween) {
      if (
        Entity(tween, out var entity) &&
        !IsPaused(entity)
      ) {
        _world.AddComponent<Paused>(entity);
      }
    }

    public static void Resume(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.DelComponent<Paused>(entity);
      }
    }

    static bool IsPaused(int entity) =>
      _world.HasComponent<Paused>(entity);

#endregion
#region Group

    public static Tween SetUpdate(this Tween tween) => tween.SetGroup<Update>();

    public static Tween SetFixedUpdate(this Tween tween) => tween.SetGroup<FixedUpdate>();

    public static Tween SetLateUpdate(this Tween tween) => tween.SetGroup<LateUpdate>();

    public static Tween SetManualUpdate(this Tween tween) => tween.SetGroup<ManualUpdate>();

    public static Tween SetGroup<TUpdate>(this Tween tween) where TUpdate : struct {
      if (Entity(tween, out var entity)) {
        // TODO: Make dynamic.
        _world.DelComponent<Update>(entity);
        _world.DelComponent<LateUpdate>(entity);
        _world.DelComponent<FixedUpdate>(entity);
        _world.DelComponent<ManualUpdate>(entity);
        _world.AddComponent<TUpdate>(entity);
      }
      return tween;
    }

    public static Tween Slerp(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.EnsureComponent<Slerp>(entity);
      }
      return tween;
    }

#endregion
#region Loop

    public static Tween Loop(this Tween tween, int count) {
      if (count < 0) {
        throw new ArgumentOutOfRangeException(nameof(count), count, "Must not be negative");
      }
      return tween.SetLooping(count);
    }

    public static Tween StopLoop(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.DelComponent<Loop>(entity);
      }
      return tween;
    }

    public static Tween LoopForever(this Tween tween) =>
      tween.SetLooping(-1);

    static Tween SetLooping(this Tween tween, int remaining = -1) {
      if (Entity(tween, out var entity)) {
        if (remaining == 0) {
          _world.DelComponent<Loop>(entity);
        } else {
          ref var loop = ref _world.EnsureComponent<Loop>(entity);
          loop.Remaining = remaining;
        }
      }
      return tween;
    }

#endregion
#region Callbacks

    public static Tween OnComplete(this Tween tween, Action onComplete) {
      if (Entity(tween, out var entity)) {
        ref var c = ref _world.EnsureComponent<OnComplete>(entity);
        c.Callback = onComplete;
      }
      return tween;
    }

#endregion
#region Ecs

    static EcsWorld _world;
    static EcsSystems _systems;
    static readonly RunState _runState = new ();
    static UnityLifecycle _lifecycle;

#pragma warning disable IDE0051
    [RuntimeInitializeOnLoadMethod]
    static void Initialize() {
      _world = new ();
      _systems = new EcsSystems(_world, _runState)
        .Add(new ActivateGroupSystem())
        .Add(new ProgressSystem())
        .Add(CreateChangeSystem<float>(Mathf.LerpUnclamped))
        .Add(CreateChangeSystem<Vector2>(Vector2.LerpUnclamped))
        .Add(CreateNonSlerpChangeSystem<Vector3>(Vector3.LerpUnclamped))
        .Add(CreateSlerpChangeSystem<Vector3>(Vector3.SlerpUnclamped))
        .Add(CreateNonSlerpChangeSystem<Quaternion>(Quaternion.LerpUnclamped))
        .Add(CreateSlerpChangeSystem<Quaternion>(Quaternion.SlerpUnclamped))
        .Add(CreateChangeSystem<Color>(Color.LerpUnclamped))
        .Add(new DeactivateGroupSystem())
        .Add(new LoopSystem())
        .Add(new CompleteSystem());
      _systems.Init();

      var go = new GameObject("RhysTween_UnityLifecycle");
      _lifecycle = go.AddComponent<UnityLifecycle>();
      UnityEngine.Object.DontDestroyOnLoad(_lifecycle);
    }
#pragma warning restore IDE0051

    internal static void Destroy() => _world.Destroy();

#endregion
#region Run

    static readonly Dictionary<Type, EcsFilter> _groupFilters = new();

    static EcsFilter GroupFilter<TGroup>() where TGroup : struct {
      if (!_groupFilters.TryGetValue(typeof(TGroup), out var filter)) {
        filter = _world.Filter<TGroup>().End();
        _groupFilters.Add(typeof(TGroup), filter);
      }
      return filter;
    }

    public static void Run<TGroup>(float deltaTime) where TGroup : struct{
      _runState.DeltaTime = deltaTime;
      _runState.GroupFilter = GroupFilter<TGroup>();
      _systems.Run();
    }

#endregion
#region Private

    static EcsWorld.Mask FilterChange<TValue>() =>
      _world.Filter<TweenConfig<TValue>>().Inc<Active>().Exc<Paused>();

    static ChangeSystem<TValue> CreateNonSlerpChangeSystem<TValue>(Lerp<TValue> lerp) =>
      CreateChangeSystem(FilterChange<TValue>().Exc<Slerp>().End(), lerp);

    static ChangeSystem<TValue> CreateSlerpChangeSystem<TValue>(Lerp<TValue> lerp) =>
      CreateChangeSystem(FilterChange<TValue>().Inc<Slerp>().End(), lerp);

    static ChangeSystem<TValue> CreateChangeSystem<TValue>(Lerp<TValue> lerp) =>
      CreateChangeSystem(FilterChange<TValue>().End(), lerp);

    static ChangeSystem<T> CreateChangeSystem<T>(EcsFilter filter, Lerp<T> lerp) =>
      new (filter, lerp);

    static bool Entity(Tween tween, out int entity) {
      if (!tween._entity.Unpack(_world, out entity)) {
        Debug.LogWarning($"Tween is no longer active");
        return false;
      }
      return true;
    }

#endregion
  }
}