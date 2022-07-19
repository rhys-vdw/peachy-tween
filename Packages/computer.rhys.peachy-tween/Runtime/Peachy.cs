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

    internal Tween(EcsPackedEntity entity) {
      _entity = entity;
    }
  }

  public static class Peachy {
#region Tween factory

    public static Tween Tween(float from, float to, float duration, Action<float> onChange) =>
      CreateTween(from, to, duration, onChange);

    public static Tween Tween(Vector2 from, Vector2 to, float duration, Action<Vector2> onChange) =>
      CreateTween(from, to, duration, onChange);

    public static Tween Tween(Vector3 from, Vector3 to, float duration, Action<Vector3> onChange) =>
      CreateTween(from, to, duration, onChange);

    public static Tween Tween(Vector4 from, Vector4 to, float duration, Action<Vector4> onChange) =>
      CreateTween(from, to, duration, onChange);

    public static Tween Tween(Quaternion from, Quaternion to, float duration, Action<Quaternion> onChange) =>
      CreateTween(from, to, duration, onChange);

    public static Tween Tween(Color from, Color to, float duration, Action<Color> onChange) =>
      CreateTween(from, to, duration, onChange);

    static Tween CreateTween<T>(T from, T to, float duration, Action<T> onChange) {
      var entity = CreateTweenEntity(from, to, duration, onChange);
      return new Tween(_world.PackEntity(entity));
    }

    static int CreateTweenEntity<T>(T from, T to, float duration, Action<T> onChange) {
      var entity = _world.NewEntity();
      _world.AddComponent(entity, new TweenConfig<T>(from, to, onChange));
      _world.AddComponent(entity, new TweenState(duration));
      SetGroup<Update>(entity);
      return entity;
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
        Reverse(entity);
      }
      return tween;
    }

    /// <summary>
    /// Run this tween from end to start.
    /// </summary>
    public static Tween From(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.ToggleComponent<Reverse>(entity);
      }
      return tween;
    }

    static void GoTo(int entity, float elapsed) {
      _world.DelComponent<Complete>(entity);
      ref var tweenState = ref _world.GetComponent<TweenState>(entity);
      tweenState.Elapsed = elapsed;
    }

    static void Complete(int entity) {
      ref var tweenState = ref _world.GetComponent<TweenState>(entity);
      GoTo(entity, tweenState.Duration);
    }

    static bool IsComplete(int entity) => _world.HasComponent<Complete>(entity);

    public static void Reverse(int entity) {
      _world.ToggleComponent<Reverse>(entity);
      ref var state = ref _world.GetComponent<TweenState>(entity);
      state.Elapsed = state.Duration - state.Elapsed;
    }

#endregion
#region Target

    static EcsFilter _targetFilter;

    /// <summary>
    /// Set the associated target of a <c cref="Tween">Tween</c> for killing by
    /// target.
    ///
    /// This does not change which object the Tween is currently acting on, its
    /// purpose is to link this tween to an object so that it will be killed
    /// when the target object is passed to <c cref="Kill">Peachy.Kill</c>.
    ///
    /// This will replace any previously set target.
    ///
    /// This method is called by provided extension methods (e.g.
    /// <c cref="TrasnformExtensions.TweenPosition">TweenPosition</c>), and
    /// should be called by any custom extension methods.
    /// </summary>
    /// <param name="tween">The tween.</param>
    /// <param name="target">Any instance of a reference type to become the target of this tween.</param>
    public static Tween SetTarget<T>(this Tween tween, T target) where T : class {
      if (target == null) {
        throw new ArgumentNullException(nameof(target));
      }
      if (_targetFilter == null) {
        _targetFilter = _world.Filter<Target>().End();
      }
      if (Entity(tween, out int entity)) {
        ref var t = ref _world.EnsureComponent<Target>(entity);
        t.Object = target;
      }
      return tween;
    }

    /// <summary>
    /// Get the associated target of a <c cref="Tween">Tween</c>.
    /// </summary>
    /// <seealso cref="SetTarget"/>
    /// <param name="tween">The tween.</param>
    /// <param name="target">The previously set target.</param>
    /// <returns><c>true</c> if a target has been set; otherwise, <c>false</c>.</returns>
    public static bool TryGetTarget(this Tween tween, out object target) {
      if (
        Entity(tween, out var entity) &&
        _world.TryGetComponent<Target>(entity, out var t)
      ) {
        target = t.Object;
        return true;
      }
      target = default;
      return false;
    }

    /// <summary>
    /// Kill all <c cref="Tween">Tween</c>s targeting an object.
    /// </summary>
    /// <seealso cref="SetTarget"/>
    /// <param name="tween">The object.</param>
    /// <param name="target">The target object.</param>
    public static void KillAllWithTarget(object target, bool complete = false) {
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

    public static Tween Kill(this Tween tween, bool complete = false) {
      if (TryEntity(tween, out var entity)) {
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
      return tween;
    }

    static void Kill(int entity, bool complete = false) {
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

    public static bool IsValid(this Tween tween) =>
      TryEntity(tween, out _);

#endregion
#region Rotation

    public static Tween Angle(this Tween tween) => tween.Rotate();

    public static Tween Slerp(this Tween tween) => tween.Rotate();

    public static Tween Rotate(this Tween tween) {
      if (Entity(tween, out var entity)) {
        _world.EnsureComponent<Rotate>(entity);
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
#region Shake

    /// <summary>
    /// Set the lerp function to shake.
    ///
    /// <para>
    /// <b>Supported by Vector3 tweens only.</b>
    /// </para>
    /// <para>
    /// This overrides the default tween function to shake its values. This
    /// creates a lerp function that performs the <c cref="Punch">Punch<c> ease
    /// on each dimension of the tweened value.
    /// </para>
    /// </summary>
    /// <seealso cref="Punch"/>
    /// <param name="tween">The tween.</param>
    /// <param name="oscillationCount">Number of oscillations per axis.</param>
    /// <param name="decay">Rate at which amplitude and frequency decrease over time.</param>
    /// <param name="randomness">Maximum percentage change randomly applied to amplitude and frequency per axis.</param>
    public static Tween Shake(
      this Tween tween,
      int oscillationCount,
      float decay,
      float randomness
    ) => Shake(tween, oscillationCount, decay, decay, randomness, randomness);

    /// <summary>
    /// Set the lerp function to shake.
    ///
    /// <para>
    /// <b>Supported by Vector3 tweens only.</b>
    /// </para>
    /// <para>
    /// This overrides the default tween function to shake its values. This
    /// creates a lerp function that performs the <c cref="Punch">Punch<c> ease
    /// on each dimension of the tweened value.
    /// </para>
    /// </summary>
    /// <seealso cref="Punch"/>
    /// <param name="tween">The tween.</param>
    /// <param name="oscillationCount">Number of oscillations per axis.</param>
    /// <param name="frequencyDecay">Rate at which frequency decreases over time.</param>
    /// <param name="amplitudeDecay">Rate at which amplitude decreases over time.</param>
    /// <param name="frequencyRandomness">Maximum percentage change randomly applied to frequency per axis.</param>
    /// <param name="amplitudeRandomness">Maximum percentage change randomly applied to amplitude per axis.</param>
    public static Tween Shake(
      this Tween tween,
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay,
      float amplitudeRandomness,
      float frequencyRandomness
    ) {
      if (Entity(tween, out var entity)) {
        if (!_world.HasComponent<TweenConfig<Vector3>>(entity)) {
          throw new ArgumentException("Tween is not operating on a Vector3 value", nameof(tween));
        }
        ref var lerp = ref _world.EnsureComponent<OverrideLerp<Vector3>>(entity);
        lerp.Func = LerpFuncs.CreateShake(
          oscillationCount: oscillationCount,
          amplitudeDecay: amplitudeDecay,
          frequencyDecay: frequencyDecay,
          amplitudeRandomness: amplitudeRandomness,
          frequencyRandomness: frequencyRandomness
        );
      }
      return tween;
    }

#endregion
#region Punch

    /// <summary>
    /// Set the ease to oscillate and fade out.
    /// </summary>
    /// <param name="tween">The tween.</param>
    /// <param name="oscillationCount">
    /// The number of times the value will oscillation (half the period).
    ///
    /// Setting this value to a negative will move it away from the target on
    /// its first oscillation.
    /// </param>
    /// <param name="amplitudeDecay">
    /// Rate at which amplitude of wave decreases.
    ///
    /// <para>
    /// Higher values cause a more vigorous initial shake.<br/>
    /// A value of zero will cause amplitude to stay constant.<br/>
    /// Values below zero cause the amplitude to increase over time, tending towards infinity.<br/>
    /// </para>
    /// </param>
    /// <param name="frequencyDecay">
    /// Rate at which frequency of wave decreases.
    ///
    /// <para>
    /// Higher values cause a more vigorous initial shake. Values below zero
    /// cause the shake to increase in speed over time.
    /// </para>
    /// </param>
    public static Tween Punch(
      this Tween tween,
      int oscillationCount,
      float amplitudeDecay = 1f,
      float frequencyDecay = 1f
    ) {
      if (Entity(tween, out var entity)) {
        tween.Ease(EaseFuncs.CreatePunch(oscillationCount, amplitudeDecay, frequencyDecay));
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
      InitializeEcs();
      InitializeUnityLifecycle();
    }
#pragma warning restore IDE0051

    public static bool IsInitialized() => _world != null;

    static void InitializeEcs() {
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
        .Add(ChangeSystemInc<Vector2, Rotate>(MathUtility.SlerpUnclamped))
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

    public static void ManualUpdate(this Tween tween, float deltaTime) {
      if (Entity(tween, out var entity)) {
        ManualUpdate(entity, deltaTime);
      }
    }

    static void ManualUpdate(int entity, float deltaTime) {
      _runState.Set(entity, deltaTime);
      _systems.Run();
    }

    public static Tween Sync(this Tween tween) {
      if (Entity(tween, out var entity)) {
        Sync(entity);
      }
      return tween;
    }

    static void Sync(int entity) => ManualUpdate(entity, 0);

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

    static bool Entity(Tween tween, out int entity) {
      if (!TryEntity(tween, out entity)) {
        Debug.LogWarning($"Tween is invalid");
        return false;
      }
      return true;
    }

    static bool TryEntity(Tween tween, out int entity) =>
      tween._entity.Unpack(_world, out entity);

#endregion
  }
}