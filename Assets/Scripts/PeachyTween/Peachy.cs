using System;
using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace PeachyTween {
  internal class RunState {
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

  public struct Tween {
    readonly internal EcsPackedEntity _entity;

    public Tween(EcsPackedEntity entity) {
      _entity = entity;
    }
  }

  public static class Peachy {
#region Tween factory

    public static Tween Tween<T>(T from, T to, Action<T> onChange, float duration) {
      var entity = _world.NewEntity();
      _world.AddComponent(entity, new TweenConfig<T>(from, to, onChange));
      _world.AddComponent(entity, new TweenState(duration));
      SetGroup<Update>(entity);
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
#region Preserve

    public static Tween Preserve(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.EnsureComponent<Preserve>(entity);
      }
      return tween;
    }

    public static Tween ClearPreserve(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.DelComponent<Preserve>(entity);
      }
      return tween;
    }

#endregion
#region Control

    public static Tween Restart(this Tween tween) => tween.GoTo(0);

    public static Tween GoTo(this Tween tween, float elapsed) {
      if (Entity(tween, out var entity)) {
        GoTo(entity, elapsed);
      }
      return tween;
    }

    public static Tween Complete(this Tween tween) {
      if (Entity(tween, out var entity)) {
        Complete(entity);
      }
      return tween;
    }

    public static bool IsComplete(this Tween tween) =>
      Entity(tween, out var entity) &&
      IsComplete(entity);

    public static Tween Reverse(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.ToggleComponent<Reverse>(entity);
        ref var state = ref _world.GetComponent<TweenState>(entity);
        state.Elapsed = state.Duration - state.Elapsed;
      }
      return tween;
    }

    static void GoTo(int entity, float elapsed) {
      _world.DelComponent<Complete>(entity);
      ref var tweenState = ref _world.GetComponent<TweenState>(entity);
      tweenState.Elapsed = elapsed;
      ManualUpdate(entity, 0);
    }

    static void Complete(int entity) {
      ref var tweenState = ref _world.GetComponent<TweenState>(entity);
      GoTo(entity, tweenState.Duration);
    }

    static bool IsComplete(int entity) => _world.HasComponent<Complete>(entity);

#endregion
#region Ping-pong

    public static Tween PingPong(this Tween tween) {
      if (Entity(tween, out int entity)) {
        _world.EnsureComponent<PingPong>(entity);
      }
      return tween;
    }

    public static Tween ClearPingPong(this Tween tween) {
      if (Entity(tween, out int entity)) {
        _world.DelComponent<PingPong>(entity);
      }
      return tween;
    }

#endregion
#region Kill

    public static void Kill(this Tween tween, bool complete = false) {
      if (EntityNoWarn(tween, out var entity)) {
        if (complete && !IsComplete(entity)) {
          // Cancel any loops.
          _world.DelComponent<Loop>(entity);

          // Cancel preserve.
          _world.DelComponent<Preserve>(entity);

          // This kills the tween.
          Complete(entity);
        } else {
          // Immediately kill the tween.
          _world.KillTween(entity);
        }
      }
    }

    internal static void KillTween(this EcsWorld world, int entity) {
      world.Invoke<OnKill>(entity);
      world.DelEntity(entity);
    }

#endregion
#region Rotation

    public static Tween Slerp(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.EnsureComponent<Slerp>(entity);
      }
      return tween;
    }

    public static Tween Angle(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.EnsureComponent<Angle>(entity);
      }
      return tween;
    }

#endregion
#region Group

    static readonly Dictionary<Type, EcsFilter> _groupFilters = new();

    public static Tween SetUpdate(this Tween tween) => tween.SetGroup<Update>();

    public static Tween SetFixedUpdate(this Tween tween) => tween.SetGroup<FixedUpdate>();

    public static Tween SetLateUpdate(this Tween tween) => tween.SetGroup<LateUpdate>();

    public static Tween SetManualUpdate(this Tween tween) => tween.ClearGroup();

    public static Tween ClearGroup(this Tween tween) {
      if (Entity(tween, out var entity)) {
        ClearGroup(entity);
      }
      return tween;
    }

    public static Tween SetGroup<TGroup>(this Tween tween) where TGroup : struct {
      if (Entity(tween, out var entity)) {
        SetGroup<TGroup>(entity);
      }
      return tween;
    }

    static void SetGroup<TGroup>(int entity) where TGroup : struct {
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

    static void ClearGroup(int entity) {
      foreach (var (key, _) in _groupFilters) {
        _world.DelComponent(key, entity);
      }
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
#region Easing

    public static Tween ClearEase(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.DelComponent<Eased>(entity);
      }
      return tween;
    }

    public static Tween Ease(this Tween tween, AnimationCurve animationCurve) {
      if (animationCurve == null) {
        throw new ArgumentNullException(nameof(animationCurve));
      }
      return Ease(tween, animationCurve.Evaluate);
    }

    public static Tween Ease(this Tween tween, Ease ease) =>
      ease == PeachyTween.Ease.Linear
        ? ClearEase(tween)
        : Ease(tween, ease.ToFunc());

    public static Tween Ease(this Tween tween, EaseFunc easeFunc) {
      if (Entity(tween, out var entity)) {
        ref var ease = ref _world.EnsureComponent<Eased>(entity);
        ease.Func = easeFunc;
      }
      return tween;
    }

#endregion
#region Callbacks

    public static Tween OnLoop(this Tween tween, Action onComplete) =>
      tween.AddHandler<OnLoop>(onComplete);

    public static Tween OnComplete(this Tween tween, Action onComplete) =>
      tween.AddHandler<OnComplete>(onComplete);

    public static Tween OnKill(this Tween tween, Action onKill) =>
      tween.AddHandler<OnKill>(onKill);

    static Tween AddHandler<T>(this Tween tween, Action handler) where T : struct, ICallback {
      if (Entity(tween, out var entity)) {
        _world.AddHandler<T>(entity, handler);
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
        .Add(new ReverseSystem())
        .Add(new EaseSystem())
        .Add(ChangeSystemExc<float, Angle>(Mathf.LerpUnclamped))
        .Add(ChangeSystemInc<float, Angle>(Mathf.LerpAngle))
        .Add(ChangeSystem<Vector2>(Vector2.LerpUnclamped))
        .Add(ChangeSystemExc<Vector3, Slerp>(Vector3.LerpUnclamped))
        .Add(ChangeSystemInc<Vector3, Slerp>(Vector3.SlerpUnclamped))
        .Add(ChangeSystem<Vector4>(Vector4.LerpUnclamped))
        .Add(ChangeSystemExc<Quaternion, Slerp>(Quaternion.LerpUnclamped))
        .Add(ChangeSystemInc<Quaternion, Slerp>(Quaternion.SlerpUnclamped))
        .Add(ChangeSystem<Color>(Color.LerpUnclamped))
        .Add(new CallbackSystem<OnLoop>(FilterComplete().Inc<Loop>().End()))
        .Add(new PingPongSystem())
        .Add(new LoopSystem())
        .Add(new CallbackSystem<OnComplete>(FilterComplete().End()))
        .Add(new AutoKillSystem())
        .Add(new DeactivateSystem());
      _systems.Init();

      var go = new GameObject($"{nameof(PeachyTween)}::{nameof(UnityLifecycle)}");
      _lifecycle = go.AddComponent<UnityLifecycle>();
      UnityEngine.Object.DontDestroyOnLoad(_lifecycle);
    }
#pragma warning restore IDE0051

    internal static void Destroy() => _world.Destroy();

#endregion
#region Run

    public static void Run<TGroup>(float deltaTime) where TGroup : struct {
      if (_groupFilters.TryGetValue(typeof(TGroup), out var filter)) {
        _runState.Set(filter, deltaTime);
        _systems.Run();
      }
    }

    public static void ManualUpdate(this Tween tween, float deltaTime) {
      if (Entity(tween, out var entity)) {
        ManualUpdate(entity, deltaTime);
      }
    }

    static void ManualUpdate(int entity, float deltaTime) {
      _runState.Set(entity, deltaTime);
      _systems.Run();
    }

#endregion
#region Private

    static EcsWorld.Mask FilterComplete() =>
      _world.Filter<Complete>();

    static EcsWorld.Mask FilterActive<TValue>() =>
      _world.Filter<TweenConfig<TValue>>().Inc<Active>();

    static ChangeSystem<TValue> ChangeSystemExc<TValue, TExc>(Lerp<TValue> lerp) where TExc : struct =>
      ChangeSystem(FilterActive<TValue>().Exc<TExc>().End(), lerp);

    static ChangeSystem<TValue> ChangeSystemInc<TValue, TInc>(Lerp<TValue> lerp) where TInc : struct =>
      ChangeSystem(FilterActive<TValue>().Inc<TInc>().End(), lerp);

    static ChangeSystem<TValue> ChangeSystem<TValue>(Lerp<TValue> lerp) =>
      ChangeSystem(FilterActive<TValue>().End(), lerp);

    static ChangeSystem<T> ChangeSystem<T>(EcsFilter filter, Lerp<T> lerp) =>
      new (filter, lerp);

    static bool Entity(Tween tween, out int entity) {
      if (!EntityNoWarn(tween, out entity)) {
        Debug.LogWarning($"Tween is no longer active");
        return false;
      }
      return true;
    }

    static bool EntityNoWarn(Tween tween, out int entity) =>
      tween._entity.Unpack(_world, out entity);

#endregion
  }
}