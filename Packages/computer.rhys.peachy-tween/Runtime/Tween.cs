using System;
using UnityEngine;
using Leopotam.EcsLite;

namespace PeachyTween {
  public struct Tween {
    readonly internal EcsPackedEntity _entity;

    internal Tween(EcsPackedEntity entity) {
      _entity = entity;
    }

#region Pause

    public Tween Pause() {
      if (Entity(out var entity)) {
        Core.Pause(entity);
      }
      return this;
    }

    public Tween Resume() {
      if (Entity(out var entity)) {
        Core.Resume(entity);
      }
      return this;
    }

    public bool IsPaused() =>
      Entity(out var entity) && Core.IsPaused(entity);

#endregion
#region Preserve

    public Tween Preserve() {
      if (Entity(out var entity)) {
        Core.Preserve(entity);
      }
      return this;
    }

    public Tween ClearPreserve() {
      if (Entity(out var entity)) {
        Core.ClearPreserve(entity);
      }
      return this;
    }

#endregion
#region Control

    public Tween Restart() => GoTo(0);

    public Tween GoTo(float elapsed) {
      if (Entity(out var entity)) {
        Core.GoTo(entity, elapsed);
      }
      return this;
    }

    public Tween Complete() {
      if (Entity(out var entity)) {
        Core.Complete(entity);
      }
      return this;
    }

    public bool IsComplete() =>
      Entity(out var entity) &&
      Core.IsComplete(entity);

    public Tween Reverse() {
      if (Entity(out var entity)) {
        Core.Reverse(entity);
      }
      return this;
    }

    /// <summary>
    /// Run this tween from end to start.
    /// </summary>
    public Tween From() {
      if (Entity(out var entity)) {
        Core.From(entity);
      }
      return this;
    }

#endregion
#region Target

    /// <summary>
    /// <para>Set the associated target of a <c cref="Tween">Tween</c> for
    /// killing by target.</para>
    ///
    /// <para>This does not change which object the Tween is currently acting
    /// on, its purpose is to link this tween to an object so that it will be
    /// killed when the target object is passed to <c
    /// cref="Kill">Core.Kill</c>.</para>
    ///
    /// <para>This will replace any previously set target.</para>
    ///
    /// <para>This method is called by provided extension methods (e.g.
    /// <c cref="TrasnformExtensions.TweenPosition">TweenPosition</c>), and
    /// should be called by any custom extension methods.</para>
    /// </summary>
    /// <seealso cref="Peachy.KillAllWithTarget(object, bool)"/>
    /// <param name="tween">The tween.</param>
    /// <param name="target">Any instance of a reference type to become the target of this tween.</param>
    public Tween SetTarget<T>(T target) where T : class {
      if (Entity(out var entity)) {
        Core.SetTarget(entity, target);
      }
      return this;
    }

    /// <summary>
    /// Get the associated target of a <c cref="Tween">Tween</c>.
    /// </summary>
    /// <seealso cref="SetTarget"/>
    /// <param name="tween">The tween.</param>
    /// <param name="target">The previously set target.</param>
    /// <returns><c>true</c> if a target has been set; otherwise, <c>false</c>.</returns>
    public bool TryGetTarget(out object target) {
      if (Entity(out var entity)) {
        return Core.TryGetTarget(entity, out target);
      }
      target = default;
      return false;
    }

#endregion
#region Ping-pong

    public Tween PingPong() {
      if (Entity(out int entity)) {
        Core.PingPong(entity);
      }
      return this;
    }

    public Tween ClearPingPong() {
      if (Entity(out int entity)) {
        Core.ClearPingPong(entity);
      }
      return this;
    }

#endregion
#region Kill

    public Tween Kill(bool complete = false) {
      if (TryEntity(out var entity)) {
        Core.Kill(entity, complete);
      }
      return this;
    }

    /// <summary>
    /// Does this tween exist.
    /// </summary>
    /// <returns>`true` if the tween exists; `false` if the tween not been set or has been killed.</returns>
    public bool IsValid() =>
      TryEntity(out _);

#endregion
#region Rotation

    public Tween Angle() => Rotate();

    public Tween Slerp() => Rotate();

    public Tween Rotate() {
      if (Entity(out var entity)) {
        Core.Rotate(entity);
      }
      return this;
    }

#endregion
#region Run

    public void ManualUpdate(float deltaTime) {
      if (Entity(out var entity)) {
        Core.ManualUpdate(entity, deltaTime);
      }
    }

    public Tween Sync() {
      if (Entity(out var entity)) {
        Core.Sync(entity);
      }
      return this;
    }

#endregion
#region Group

    /// <summary>
    /// <para>Set tween to update on <c>Update</c>.</para>
    /// <para>This is the default update group for new tweens.</para>
    /// </summary>
    public Tween SetUpdate() => SetGroup<Update>();

    /// <summary>
    /// Set tween to update on <c>Update</c> using unscaled time.
    /// </summary>
    public Tween SetUnscaledUpdate() => SetGroup<UnscaledUpdate>();

    /// <summary>
    /// Set tween to update on <c>LateUpdate</c>.
    /// </summary>
    public Tween SetLateUpdate() => SetGroup<LateUpdate>();

    /// <summary>
    /// Set tween to update on <c>LateUpdate</c> using unscaled time.
    /// </summary>
    public Tween SetUnscaledLateUpdate() => SetGroup<UnscaledLateUpdate>();

    /// <summary>
    /// Set tween to update on <c>FixedUpdate</c>.
    /// </summary>
    public Tween SetFixedUpdate() => SetGroup<FixedUpdate>();

    /// <summary>
    /// Set tween to update on <c>FixedUpdate</c> using unscaled time.
    /// </summary>
    public Tween SetUnscaledFixedUpdate() => SetGroup<UnscaledFixedUpdate>();

    /// <summary>
    /// <para>Disable automatic update of this tween.</para>
    /// <para>This is an alias of <c cref="ClearGroup">ClearGroup</c>.</para>
    /// </summary>
    public Tween SetManualUpdate() => ClearGroup();

    /// <summary>
    /// Remove the assigned update group from this tween.
    /// </summary>
    public Tween ClearGroup() {
      if (Entity(out var entity)) {
        Core.ClearGroup(entity);
      }
      return this;
    }

    /// <summary>
    /// <para>Change the tween's update group.</para>
    /// <param>Tweens default to the <c>Update</c> group, but custom groups can be added.</param>
    /// </summary>
    public Tween SetGroup<TGroup>() where TGroup : struct {
      if (Entity(out var entity)) {
        Core.SetGroup<TGroup>(entity);
      }
      return this;
    }

#endregion
#region Loop

    public Tween Loop(int count) {
      if (count < 0) {
        throw new ArgumentOutOfRangeException(nameof(count), count, "Must not be negative");
      }
      return SetLooping(count);
    }

    public Tween StopLoop() =>
      SetLooping(0);

    public Tween LoopForever() =>
      SetLooping(-1);

    Tween SetLooping(int remaining) {
      if (Entity(out var entity)) {
        Core.SetLooping(entity, remaining);
      }
      return this;
    }

#endregion
#region Easing

    public Tween ClearEase() {
      if (Entity(out var entity)) {
        Core.ClearEase(entity);
      }
      return this;
    }

    public Tween Ease(AnimationCurve animationCurve) {
      _ = animationCurve ?? throw new ArgumentNullException(nameof(animationCurve));
      return Ease(animationCurve.Evaluate);
    }

    public Tween Ease(Ease ease) =>
      ease == PeachyTween.Ease.Linear
        ? ClearEase()
        : Ease(ease.ToFunc());

    public Tween Ease(EaseFunc easeFunc) {
      if (Entity(out var entity)) {
        Core.Ease(entity, easeFunc);
      }
      return this;
    }

#endregion
#region Lerp

    /// <summary>
    /// Set the lerp function.
    ///
    /// <para>
    /// Overrides the default lerp function for this tween.
    /// </para>
    /// </summary>
    /// <seealso cref="Punch"/>
    /// <param name="tween">The tween.</param>
    /// <param name="lerp">The lerp function for this tween.</param>
    public Tween Lerp<T>(LerpFunc<T> lerp) {
      if (Entity(out var entity)) {
        Core.Lerp(entity, lerp);
      }
      return this;
    }


#endregion
#region Shake

    /// <summary>
    /// Set the lerp function to shake.
    ///
    /// <para>
    /// <b>Supported by Vector2 tweens only.</b>
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
    public Tween Shake2D(
      int oscillationCount,
      float decay,
      float randomness
    ) => Shake2D(oscillationCount, decay, decay, randomness, randomness);

    /// <summary>
    /// Set the lerp function to shake.
    ///
    /// <para>
    /// <b>Supported by Vector2 tweens only.</b>
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
    public Tween Shake2D(
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay,
      float amplitudeRandomness,
      float frequencyRandomness
    ) => Lerp(LerpFuncs.CreateShake2D(
      oscillationCount: oscillationCount,
      amplitudeDecay: amplitudeDecay,
      frequencyDecay: frequencyDecay,
      amplitudeRandomness: amplitudeRandomness,
      frequencyRandomness: frequencyRandomness
    ));

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
    public Tween Shake(
      int oscillationCount,
      float decay,
      float randomness
    ) => Shake(oscillationCount, decay, decay, randomness, randomness);

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
    public Tween Shake(
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay,
      float amplitudeRandomness,
      float frequencyRandomness
    ) => Lerp(LerpFuncs.CreateShake(
      oscillationCount: oscillationCount,
      amplitudeDecay: amplitudeDecay,
      frequencyDecay: frequencyDecay,
      amplitudeRandomness: amplitudeRandomness,
      frequencyRandomness: frequencyRandomness
    ));

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
    public Tween Punch(
      int oscillationCount,
      float amplitudeDecay = 1f,
      float frequencyDecay = 1f
    ) => Ease(
      EaseFuncs.CreatePunch(oscillationCount, amplitudeDecay, frequencyDecay)
    );

#endregion
#region Callbacks

    public Tween OnLoop(Action onComplete) =>
      AddHandler<OnLoop>(onComplete);

    public Tween OnComplete(Action onComplete) =>
      AddHandler<OnComplete>(onComplete);

    public Tween OnKill(Action onKill) =>
      AddHandler<OnKill>(onKill);

    Tween AddHandler<T>(Action handler) where T : struct, ICallback {
      if (Entity(out var entity)) {
        Core.AddHandler<T>(entity, handler);
      }
      return this;
    }

#endregion
#region Private

    bool Entity(out int entity) {
      if (!TryEntity(out entity)) {
        Debug.LogWarning($"Tween is invalid");
        return false;
      }
      return true;
    }

    bool TryEntity(out int entity) => Core.TryEntity(_entity, out entity);

#endregion
  }
}
