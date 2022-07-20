
using System;
using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace PeachyTween {
  class RunState {
    public float DeltaTime { get; private set; }
    public EcsFilter GroupFilter { get; private set; }
    public int? Entity { get; private set; }

    public void Set(EcsFilter groupFilter, float deltaTime) {
      DeltaTime = deltaTime;
      GroupFilter = groupFilter;
      Entity = null;
    }

    public void Set(int entity, float deltaTime) {
      DeltaTime = deltaTime;
      GroupFilter = null;
      Entity = entity;
    }
  }

  static class Core {
#region Tween factory

    public static EcsPackedEntity CreateTween<T>(T from, T to, float duration, Action<T> onChange) {
      var entity = _world.NewEntity();
      _world.AddComponent(entity, new TweenConfig<T>(from, to, onChange));
      _world.AddComponent(entity, new TweenState(duration));
      SetGroup<Update>(entity);
      return _world.PackEntity(entity);
    }

#endregion
#region Pause

    public static void Pause(int entity) =>
      _world.EnsureComponent<Paused>(entity);

    public static void Resume(int entity) =>
      _world.DelComponent<Paused>(entity);

    public static bool IsPaused(int entity) =>
      _world.HasComponent<Paused>(entity);

#endregion
#region Preserve

    public static void Preserve(int entity) =>
      _world.EnsureComponent<Preserve>(entity);

    public static void ClearPreserve(int entity) =>
      _world.DelComponent<Preserve>(entity);

#endregion
#region Control

    public static void GoTo(int entity, float elapsed) {
      _world.DelComponent<Complete>(entity);
      ref var tweenState = ref _world.GetComponent<TweenState>(entity);
      tweenState.Elapsed = elapsed;
    }

    public static void Complete(int entity) {
      ref var tweenState = ref _world.GetComponent<TweenState>(entity);
      GoTo(entity, tweenState.Duration);
    }

    public static bool IsComplete(int entity) =>
      _world.HasComponent<Complete>(entity);

    /// <summary>
    /// Run this tween from end to start.
    /// </summary>
    public static void From(int entity) =>
      _world.ToggleComponent<Reverse>(entity);

    public static void Reverse(int entity) {
      _world.ToggleComponent<Reverse>(entity);
      ref var state = ref _world.GetComponent<TweenState>(entity);
      state.Elapsed = state.Duration - state.Elapsed;
    }

#endregion
#region Target

    static EcsFilter _targetFilter;

    public static void SetTarget<T>(int entity, T target) where T : class {
      _ = target ?? throw new ArgumentNullException(nameof(target));

      if (_targetFilter == null) {
        _targetFilter = _world.Filter<Target>().End();
      }
      ref var t = ref _world.EnsureComponent<Target>(entity);
      t.Object = target;
    }

    public static bool TryGetTarget(int entity, out object target) {
      if (_world.TryGetComponent<Target>(entity, out var t)) {
        target = t.Object;
        return true;
      }
      target = default;
      return false;
    }

    public static void KillAllWithTarget(object target, bool complete) {
      if (_targetFilter == null) {
        return;
      }
      var targetPool = _world.GetPool<Target>();
      foreach (var entity in _targetFilter) {
        ref var t = ref targetPool.Get(entity);
        if (t.Object == target) {
          Kill(entity, complete);
        }
      }
    }

#endregion
#region Ping-pong

    public static void PingPong(int entity) {
      _world.EnsureComponent<PingPong>(entity);
    }

    public static void ClearPingPong(int entity) =>
      _world.DelComponent<PingPong>(entity);

#endregion
#region Kill

    public static void Kill(int entity, bool complete) {
      if (complete && !IsComplete(entity)) {
        // Cancel any loops.
        _world.DelComponent<Loop>(entity);

        // Cancel preserve.
        _world.DelComponent<Preserve>(entity);

        // The complete system will kill this entity.
        Complete(entity);
      } else {
        // Kill the tween.
        _world.EnsureComponent<Kill>(entity);
      }
    }

#endregion
#region Rotation

    public static void Rotate(int entity) =>
      _world.EnsureComponent<Rotate>(entity);

#endregion
#region Group

    static readonly Dictionary<Type, EcsFilter> _groupFilters = new();

    public static void SetGroup<TGroup>(int entity) where TGroup : struct {
      // Remove existing group.
      ClearGroup(entity);

      // Create a filter for this group if it doesn't exist.
      if (!_groupFilters.ContainsKey(typeof(TGroup))) {
        _groupFilters.Add(
          typeof(TGroup),
          _world.Filter<TGroup>().Exc<Complete>().Exc<Paused>().End()
        );
      }

      // Tag entity.
      _world.AddComponent<TGroup>(entity);
    }

    public static void ClearGroup(int entity) {
      foreach (var (key, _) in _groupFilters) {
        _world.DelComponent(key, entity);
      }
    }

#endregion
#region Loop

    public static void SetLooping(int entity, int remaining) {
      if (remaining == 0) {
        _world.DelComponent<Loop>(entity);
      } else {
        ref var loop = ref _world.EnsureComponent<Loop>(entity);
        loop.Remaining = remaining;
      }
    }

#endregion
#region Easing

    public static void ClearEase(int entity) =>
      _world.DelComponent<Eased>(entity);

    public static void Ease(int entity, EaseFunc easeFunc) {
      ref var ease = ref _world.EnsureComponent<Eased>(entity);
      ease.Func = easeFunc;
    }

#endregion
#region Lerp

    public static void Lerp<T>(int entity, LerpFunc<T> lerp) {
      if (!_world.HasComponent<TweenConfig<T>>(entity)) {
        throw new InvalidOperationException($"Tween is not operating on a {typeof(T).Name} value");
      }
      ref var l = ref _world.EnsureComponent<OverrideLerp<T>>(entity);
      l.Func = lerp;
    }

#endregion
#region Callbacks

    public static void AddHandler<T>(int entity, Action handler) where T : struct, ICallback {
      _world.AddHandler<T>(entity, handler);
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
      InitializeEcs();
      InitializeUnityLifecycle();
    }
#pragma warning restore IDE0051

    internal static void InitializeEcs() {
      _world = new ();
      _systems = new EcsSystems(_world, _runState)
        .Add(new ActivateGroupSystem())
        .Add(new ElapsedSystem())
        .Add(new PingPongSystem())
        .Add(new CallbackSystem<OnLoop>(FilterActive().Inc<Complete>().Inc<Loop>().End()))
        .Add(new LoopSystem())
        .Add(new ProgressSystem())
        .Add(new ReverseSystem())
        .Add(new EaseSystem())
        .Add(ChangeSystemExc<float, Rotate>(Mathf.LerpUnclamped))
        .Add(ChangeSystemInc<float, Rotate>(Mathf.LerpAngle))
        .Add(ChangeSystemExc<Vector2, Rotate>(Vector2.LerpUnclamped))
        .Add(ChangeSystemInc<Vector2, Rotate>(LerpFuncs.SlerpUnclamped))
        .Add(ChangeSystemExc<Vector3, Rotate>(Vector3.LerpUnclamped))
        .Add(ChangeSystemInc<Vector3, Rotate>(Vector3.SlerpUnclamped))
        .Add(ChangeSystem<Vector4>(Vector4.LerpUnclamped))
        .Add(ChangeSystemExc<Quaternion, Rotate>(Quaternion.LerpUnclamped))
        .Add(ChangeSystemInc<Quaternion, Rotate>(Quaternion.SlerpUnclamped))
        .Add(ChangeSystem<Color>(Color.LerpUnclamped))
        .Add(new CallbackSystem<OnComplete>(FilterActive().Inc<Complete>().End()))
        .Add(new CompleteSystem())
        .Add(new CallbackSystem<OnKill>(FilterActive().Inc<Kill>().End()))
        .Add(new KillSystem())
        .Add(new DeactivateSystem());
      _systems.Init();
    }

    static void InitializeUnityLifecycle() {
      var go = new GameObject($"{nameof(PeachyTween)}::{nameof(UnityLifecycle)}");
      _lifecycle = go.AddComponent<UnityLifecycle>();
      UnityEngine.Object.DontDestroyOnLoad(_lifecycle);
    }

    internal static void Destroy() {
      _world.Destroy();
      _groupFilters.Clear();
      _targetFilter = null;
    }

#endregion
#region Run

    public static void Run<TGroup>(float deltaTime) where TGroup : struct {
      if (_groupFilters.TryGetValue(typeof(TGroup), out var filter)) {
        _runState.Set(filter, deltaTime);
        _systems.Run();
      }
    }

    public static void ManualUpdate(int entity, float deltaTime) {
      _runState.Set(entity, deltaTime);
      _systems.Run();
    }

    public static void Sync(int entity) => ManualUpdate(entity, 0);

#endregion
#region Private

    static EcsWorld.Mask FilterActive() =>
      _world.Filter<Active>();

    static EcsWorld.Mask FilterActiveByType<TValue>() =>
      _world.Filter<TweenConfig<TValue>>().Inc<Active>().Exc<Kill>();

    static ChangeSystem<TValue> ChangeSystemExc<TValue, TExc>(LerpFunc<TValue> lerp) where TExc : struct =>
      ChangeSystem(FilterActiveByType<TValue>().Exc<TExc>().End(), lerp);

    static ChangeSystem<TValue> ChangeSystemInc<TValue, TInc>(LerpFunc<TValue> lerp) where TInc : struct =>
      ChangeSystem(FilterActiveByType<TValue>().Inc<TInc>().End(), lerp);

    static ChangeSystem<TValue> ChangeSystem<TValue>(LerpFunc<TValue> lerp) =>
      ChangeSystem(FilterActiveByType<TValue>().End(), lerp);

    static ChangeSystem<T> ChangeSystem<T>(EcsFilter filter, LerpFunc<T> lerp) =>
      new (filter, lerp);

    internal static bool TryEntity(EcsPackedEntity packedEntity, out int entity) =>
      packedEntity.Unpack(_world, out entity);

#endregion
  }
}