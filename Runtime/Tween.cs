using System;
using UnityEngine;
using Leopotam.EcsLite;

namespace PeachyTween {
  public readonly struct Tween {
    readonly EcsPackedEntity _entity;

    internal Tween(EcsPackedEntity entity) {
      _entity = entity;
    }

#region Pause

    /// <summary>
    /// Pause the tween.
    /// </summary>
    /// <seealso cref="Resume"/>
    /// <returns>This tween.</returns>
    public Tween Pause() {
      if (Entity(out var entity)) {
        Core.Pause(entity);
      }
      return this;
    }

    /// <summary>
    /// Unpause the tween.
    /// </summary>
    /// <seealso cref="Pause"/>
    /// <returns>This tween.</returns>
    public Tween Resume() {
      if (Entity(out var entity)) {
        Core.Resume(entity);
      }
      return this;
    }

    /// <summary>
    /// Check if the tween is paused.
    /// </summary>
    /// <seealso cref="Pause"/>
    /// <seealso cref="Resume"/>
    /// <returns>`true` if the tween is paused; `false` if the tween is not paused.</returns>
    public bool IsPaused() =>
      Entity(out var entity) && Core.IsPaused(entity);

#endregion
#region Preserve

    /// <summary>
    /// Prevent tween from being automatically killed when it completes.
    /// </summary>
    /// <remarks>
    /// Use this to create a replayable tween.
    /// </remarks>
    /// <seealso cref="ClearPreserve"/>
    /// <returns>This tween.</returns>
    public Tween Preserve() {
      if (Entity(out var entity)) {
        Core.Preserve(entity);
      }
      return this;
    }

    /// <summary>
    /// Cancels <see cref="Preserve"><c>Preserve</c></see>, allowing the tween
    /// to be killed when it completes.
    /// </summary>
    /// <seealso cref="Preserve"/>
    /// <returns>This tween.</returns>
    public Tween ClearPreserve() {
      if (Entity(out var entity)) {
        Core.ClearPreserve(entity);
      }
      return this;
    }

#endregion
#region Control

    /// <summary>
    /// Restart the tween and unpause it.<para/>
    /// Equivalent to <c>tween.Rewind().Resume()</c><para/>
    /// </summary>
    /// <seealso cref="Rewind"/>
    /// <seealso cref="Resume"/>
    public Tween Restart() => Rewind().Resume();

    /// <summary>
    /// Return the tween to the start.<para/>
    /// Equivalent to <c cref="GoTo">GoTo(0)</c>.<para/>
    /// </summary>
    /// <seealso cref="GoTo"/>
    /// <returns>This tween.</returns>
    public Tween Rewind() => GoTo(0);

    /// <summary>
    /// Set the interal time of a tween.<para/>
    /// </summary>
    /// <param name="elapsed">
    /// The time to set the tween to. <c>0</c> will rewind the tween to the
    /// start, and passing the tween's duration will fast-forward to the end.
    /// </param>
    /// <returns>This tween.</returns>
    public Tween GoTo(float elapsed) {
      if (Entity(out var entity)) {
        Core.GoTo(entity, elapsed);
      }
      return this;
    }

    /// <summary>
    /// Complete the tween, including all loops.<para/>
    /// </summary>
    /// <remarks>
    /// When the tween is updated it will call its <c
    /// cref="OnComplete">OnComplete</c> callback, and will be killed if not
    /// <see cref="Preserve">preserved</see>.<para/>
    ///
    /// To complete the tween immediately, call <c cref="Sync">tween.Sync()</c>.
    /// </remarks>
    /// <returns>This tween.</returns>
    public Tween Complete() {
      if (Entity(out var entity)) {
        Core.Complete(entity);
      }
      return this;
    }

    /// <summary>
    /// Check if the tween has ended.<para/>
    /// </summary>
    /// <remarks>
    /// This is useful for tweens that are <see cref="Preserve">preserved</see>,
    /// as any other tween will be killed upon completion.
    /// </remarks>
    /// <returns>`true` if tween is complete; `false` if tween is not complete.</returns>
    public bool IsComplete() =>
      Entity(out var entity) &&
      Core.IsComplete(entity);

    /// <summary>
    /// Reverse the direction of this tween.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween Reverse() {
      if (Entity(out var entity)) {
        Core.Reverse(entity);
      }
      return this;
    }

    /// <summary>
    /// Run this tween from end to start.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween From() {
      if (Entity(out var entity)) {
        Core.From(entity);
      }
      return this;
    }

#endregion
#region Target

    /// <summary>
    /// Set the associated target of a <c cref="Tween">Tween</c> for
    /// killing by target.
    /// </summary>
    /// <remarks>
    /// This will replace any previously set target.<para/>
    ///
    /// This method is called by provided extension methods (e.g.
    /// <c cref="TrasnformExtensions.TweenPosition">TweenPosition</c>), and
    /// should be called by any custom extension methods.<para/>
    ///
    /// This does not change which object the Tween is currently acting
    /// on, its purpose is to link this tween to an object so that it will be
    /// killed when the target object is passed to
    /// <c cref="Peachy.KillAllWithTarget">KillAllWithTarget</c>.<para/>
    /// </remarks>
    /// <seealso cref="Peachy.KillAllWithTarget(object, bool)"/>
    /// <param name="tween">The tween.</param>
    /// <param name="target">Any instance of a reference type to become the target of this tween.</param>
    /// <returns>This tween.</returns>
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
    /// <returns><c>true</c> if a target has been set; <c>false</c> if no target has been set.</returns>
    public bool TryGetTarget(out object target) {
      if (Entity(out var entity)) {
        return Core.TryGetTarget(entity, out target);
      }
      target = default;
      return false;
    }

#endregion
#region Ping-pong

    /// <summary>
    /// Set the tween to <see cref="Reverse">reverse</see> at the start of each <see cref="Loop">loop</see>.
    /// </summary>
    /// <seealso cref="Loop"/>
    /// <seealso cref="ClearPingPong"/>
    /// <seealso cref="Reverse"/>
    /// <returns>This tween.</returns>
    public Tween PingPong() {
      if (Entity(out int entity)) {
        Core.PingPong(entity);
      }
      return this;
    }

    /// <summary>
    /// Cancel <see cref="PingPong"><c>PingPong</c></see>.
    /// </summary>
    /// <seealso cref="PingPong"/>
    /// <returns>This tween.</returns>
    public Tween ClearPingPong() {
      if (Entity(out int entity)) {
        Core.ClearPingPong(entity);
      }
      return this;
    }

#endregion
#region Kill

    /// <summary>
    /// Like <c cref="Kill">Kill</c> but updates the tween immediately.
    /// </summary>
    /// <remarks>
    /// This is equivalent to <c>tween.Kill(); tween.Sync()</c> but will not log a
    /// warning if the tween has previously been killed.
    /// </remarks>
    /// <seealso cref="Kill" />
    /// <inheritdoc cref="Kill" />
    /// <returns>This tween.</returns>
    public void KillSync(bool complete = false) {
      if (TryEntity(out var entity)) {
        Core.Kill(entity, complete);
        Core.Sync(entity);
      }
    }

    /// <summary>
    /// Deactivate this tween and delete it next time it's updated.
    /// </summary>
    /// <remarks>
    /// This marks a tween for deletion, but it will not be deleted
    /// until its next update. This means that callbacks such as
    /// <c cref="OnKill">OnKill</c> will not be triggered immediately. If you
    /// need callbacks to run immediately, then use
    /// <c cref="KillSync">KillSync</c> instead.
    /// </remarks>
    /// <seealso cref="KillSync"/>
    /// <param name="complete">
    /// Also complete this tween, updating its value to its end value and calling its <c cref="OnComplete">OnComplete</c> callback.
    /// </param>
    public void Kill(bool complete = false) {
      if (TryEntity(out var entity)) {
        Core.Kill(entity, complete);
      }
    }

    /// <summary>
    /// Check if this tween exists.
    /// </summary>
    /// <remarks>
    /// Tweens will are alive from creation until they complete or are <c
    /// cref="Kill">killed</c>. You can prevent automatic killing of complete
    /// tweens by <see cref="Preserve">preserving</see> them.
    /// </remarks>
    /// <seealso cref="Kill"/>
    /// <seealso cref="Preserve"/>
    /// <returns>`true` if the tween exists; `false` if the tween not been set or has been killed.</returns>
    public bool IsAlive() =>
      TryEntity(out _);

    /// <summary>
    /// Check if this tween is incomplete.
    /// </summary>
    /// <remarks>
    /// A tween is considered active until it's complete or killed. <see
    /// cref="Pause">Pausing</see> a tween does not decativate it.
    /// </remarks>
    /// <returns>
    /// `true` if the tween has not yet completed or been killed; `false` if the tween has completed or been killed.
    /// </returns>
    public bool IsActive() =>
      TryEntity(out var entity) && Core.IsActive(entity);

#endregion
#region Rotation

    /// <summary>
    /// Treat the value of the tween as a rotation in degrees, and rotate
    /// through the shorest angle to the end value.<para/>
    ///
    /// <strong>Only compatible with <c>float</c> tweens.</strong>
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween ShortestAngle() {
      if (Entity(out var entity)) {
        Core.ShortestAngle(entity);
      }
      return this;
    }

    /// <summary>
    /// Rotate the vector tween value around the origin, instead of taking the shortest path.
    /// through the shorest angle to the end value.<para/>
    ///
    /// <strong>Only compatible with <c>Vector2</c> and <c>Vector3</c> tweens.</strong>
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween Slerp() {
      if (Entity(out var entity)) {
        Core.Rotate(entity);
      }
      return this;
    }

#endregion
#region Run

    /// <summary>
    /// Update this tween is isolation, applying its changes immediately.
    /// </summary>
    /// <remarks>
    /// Typically used in combination with <see
    /// cref="SetManualUpdate"><c>SetManualUpdate</c></see>.
    /// </remarks>
    /// <seealso cref="SetManualUpdate"/>
    /// <param name="deltaTime">The amount of time to progress tweens by.</param>
    /// <returns>This tween.</returns>
    public void ManualUpdate(float deltaTime) {
      if (Entity(out var entity)) {
        Core.ManualUpdate(entity, deltaTime);
      }
    }

    /// <summary>
    /// Immediately apply any changes to this tween.
    /// </summary>
    /// <remarks>
    /// This will trigger callbacks.<para/>
    /// Equivalent to <c>tween.SetManualUpdate(0)</c>.
    /// </remarks>
    /// <seealso cref="KillSync"/>
    /// <seealso cref="SetManualUpdate"/>
    /// <param name="deltaTime">The amount of time to progress tweens by.</param>
    /// <returns>This tween.</returns>
    public Tween Sync() {
      if (Entity(out var entity)) {
        Core.Sync(entity);
      }
      return this;
    }

#endregion
#region Group

    /// <summary>
    /// Set tween to update on <c>Update</c>.
    /// </summary>
    /// <remarks>
    /// This is the default update group for new tweens.
    /// </remarks>
    /// <returns>This tween.</returns>
    public Tween SetUpdate() => SetGroup<Update>();

    /// <summary>
    /// Set tween to update on <c>Update</c> using unscaled time.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween SetUnscaledUpdate() => SetGroup<UnscaledUpdate>();

    /// <summary>
    /// Set tween to update on <c>LateUpdate</c>.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween SetLateUpdate() => SetGroup<LateUpdate>();

    /// <summary>
    /// Set tween to update on <c>LateUpdate</c> using unscaled time.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween SetUnscaledLateUpdate() => SetGroup<UnscaledLateUpdate>();

    /// <summary>
    /// Set tween to update on <c>FixedUpdate</c>.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween SetFixedUpdate() => SetGroup<FixedUpdate>();

    /// <summary>
    /// Set tween to update on <c>FixedUpdate</c> using unscaled time.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween SetUnscaledFixedUpdate() => SetGroup<UnscaledFixedUpdate>();

    /// <summary>
    /// Disable automatic update of this tween.
    /// </summary>
    /// <remarks>
    /// This is an alias of <c cref="ClearGroup">ClearGroup</c>.
    /// </remarks>
    /// <returns>This tween.</returns>
    public Tween SetManualUpdate() => ClearGroup();

    /// <summary>
    /// Remove the assigned update group from this tween.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween ClearGroup() {
      if (Entity(out var entity)) {
        Core.ClearGroup(entity);
      }
      return this;
    }

    /// <summary>
    /// Change the tween's update group.
    /// </summary>
    /// <remarks>
    /// Tweens default to the <c>Update</c> group, but custom groups can be added.<para/>
    /// This group can be updated on a custom update cycle using <see
    /// cref="Peachy.Run"><c>Peachy.Run</c></see>.
    /// </remarks>
    /// <typeparam name="TGroup">An empty struct that is used as an identifier for your custom group.</typeparam>
    /// <returns>This tween.</returns>
    public Tween SetGroup<TGroup>() where TGroup : struct {
      if (Entity(out var entity)) {
        Core.SetGroup<TGroup>(entity);
      }
      return this;
    }

#endregion
#region Loop

    /// <summary>
    /// Set the number of times this tween should loop.
    /// </summary>
    /// <param name="count">
    /// The number of times this tween should loop, cannot be negative.<para/>
    /// Setting to <c>0</c> will stop the tween from looping.
    /// </param>
    /// <returns>This tween.</returns>
    public Tween Loop(int count) {
      if (count < 0) {
        throw new ArgumentOutOfRangeException(nameof(count), count, "Must not be negative");
      }
      return SetLooping(count);
    }

    /// <summary>
    /// Stop the tween from looping.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween StopLoop() =>
      SetLooping(0);

    /// <summary>
    /// Set the tween to loop endlessly.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween LoopForever() =>
      SetLooping(-1);

    internal Tween SetLooping(int remaining) {
      if (Entity(out var entity)) {
        Core.SetLooping(entity, remaining);
      }
      return this;
    }

#endregion
#region Easing

    /// <summary>
    /// Clears the easing function, defaulting to linear interpolation.
    /// </summary>
    /// <seealso cref="Ease"/>
    /// <param name="easeFunc">An easing function.</param>
    /// <returns>This tween.</returns>
    public Tween ClearEase() {
      if (Entity(out var entity)) {
        Core.ClearEase(entity);
      }
      return this;
    }

    /// <summary>
    /// Set the easing function for this tween.
    /// </summary>
    /// <param name="ease">A standard easing function.</param>
    /// <returns>This tween.</returns>
    public Tween Ease(Ease ease) =>
      ease == PeachyTween.Ease.Linear
        ? ClearEase()
        : Ease(ease.ToFunc());

    /// <inheritdoc cref="Ease" />
    /// <param name="easeFunc">An easing function.</param>
    public Tween Ease(EaseFunc easeFunc) {
      if (Entity(out var entity)) {
        Core.Ease(entity, easeFunc);
      }
      return this;
    }

    /// <inheritdoc cref="Ease" />
    /// <param name="animationCurve">An animation curve from (0,0) to (1,1).</param>
    public Tween Ease(AnimationCurve animationCurve) {
      _ = animationCurve ?? throw new ArgumentNullException(nameof(animationCurve));
      return Ease(animationCurve.Evaluate);
    }

#endregion
#region Lerp

     /// <summary>
     /// Override the default lerp function for this tween.
     /// </summary>
     /// <seealso cref="Punch"/>
     /// <typeparam name="T">The type of the value being tweened.</typeparam>
     /// <param name="lerp">The lerp function for this tween.</param>
     /// <returns>This tween.</returns>
     public Tween Lerp<T>(LerpFunc<T> lerp) {
      if (Entity(out var entity)) {
        Core.Lerp(entity, lerp);
      }
      return this;
    }


#endregion
#region Shake

    /// <summary>
    /// Set the lerp function to shake.<para/>
    /// <b>Supported by <c>Vector3</c> tweens only.</b>
    /// </summary>
    /// <remarks>
    /// This overrides the default tween function to shake its values. This
    /// creates a lerp function that performs the <c cref="Punch">Punch</c> ease
    /// on each dimension of the tweened value.
    /// </remarks>
    /// <seealso cref="Punch"/>
    /// <param name="oscillationCount">Number of oscillations per axis.</param>
    /// <param name="decay">Rate at which amplitude and frequency decrease over time.</param>
    /// <param name="randomness">Maximum percentage change randomly applied to amplitude and frequency per axis.</param>
    /// <returns>This tween.</returns>
    public Tween Shake(
      int oscillationCount,
      float decay,
      float randomness
    ) => Shake(oscillationCount, decay, decay, randomness, randomness);

    /// <seealso cref="Punch"/>
    /// <param name="oscillationCount">Number of oscillations per axis.</param>
    /// <param name="frequencyDecay">Rate at which frequency decreases over time.</param>
    /// <param name="amplitudeDecay">Rate at which amplitude decreases over time.</param>
    /// <param name="frequencyRandomness">Maximum percentage change randomly applied to frequency per axis.</param>
    /// <param name="amplitudeRandomness">Maximum percentage change randomly applied to amplitude per axis.</param>
    /// <returns>This tween.</returns>
    /// <inheritdoc cref="Shake"/>
    public Tween Shake(
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay,
      float amplitudeRandomness,
      float frequencyRandomness
    ) => Lerp(LerpFuncFactory.CreateShake(
      oscillationCount: oscillationCount,
      amplitudeDecay: amplitudeDecay,
      frequencyDecay: frequencyDecay,
      amplitudeRandomness: amplitudeRandomness,
      frequencyRandomness: frequencyRandomness
    ));

    /// <summary>
    /// Set the lerp function to shake.<para/>
    /// <strong>Supported by <c>Vector2</c> tweens only.</strong><para/>
    /// </summary>
    /// <inheritdoc cref="Shake" path="remarks"/>
    /// <inheritdoc cref="Shake(int, float, float)" />
    /// <seealso cref="Punch"/>
    public Tween Shake2D(
      int oscillationCount,
      float decay,
      float randomness
    ) => Shake2D(oscillationCount, decay, decay, randomness, randomness);

    /// <summary>
    /// Set the lerp function to shake.<para/>
    /// <strong>Supported by <c>Vector2</c> tweens only.</strong><para/>
    /// </summary>
    /// <seealso cref="Punch"/>
    /// <inheritdoc cref="Shake(int, float, float, float, float)"/>
    public Tween Shake2D(
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay,
      float amplitudeRandomness,
      float frequencyRandomness
    ) => Lerp(LerpFuncFactory.CreateShake2D(
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
    /// The number of times the value will oscillate (half the period).<para/>
    ///
    /// A negative value will move the value away from the target on its first
    /// oscillation.
    /// </param>
    /// <param name="amplitudeDecay">
    /// Rate at which the amplitude of the wave decreases.<para/>
    ///
    /// - Higher values cause a more vigorous initial shake.<br/>
    /// - A value of zero will cause amplitude to stay constant.<br/>
    /// - Values below zero cause the amplitude to increase over time, tending towards infinity.<br/>
    /// </param>
    /// <param name="frequencyDecay">
    /// Rate at which the frequency of the wave decreases.<para/>
    ///
    /// Higher values cause a more vigorous initial shake. Values below zero
    /// cause the shake to increase in speed over time.
    /// </param>
    /// <returns>This tween.</returns>
    public Tween Punch(
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay
    ) => Ease(
      EaseFuncFactory.CreatePunch(oscillationCount, amplitudeDecay, frequencyDecay)
    );

#endregion
#region Callbacks

    /// <summary>
    /// Register a callback to be invoked every time this tween progresses.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween OnUpdate(Action handler) =>
      AddHandler<OnUpdate>(handler);

    /// <summary>
    /// Remove <c cref="OnUpdate">OnUpdate</c> callback.
    /// </summary>
    /// <seealso cref="OnUpdate"/>
    /// <returns>This tween.</returns>
    public Tween RemoveOnUpdate(Action handler) =>
      RemoveHandler<OnUpdate>(handler);

    /// <summary>
    /// Register a callback to be invoked every time this tween loops.
    /// </summary>
    /// <seealso cref="Loop"/>
    /// <returns>This tween.</returns>
    public Tween OnLoop(Action onComplete) =>
      AddHandler<OnLoop>(onComplete);

    /// <summary>
    /// Remove <c cref="OnLoop">OnLoop</c> callback.
    /// </summary>
    /// <seealso cref="OnLoop"/>
    /// <returns>This tween.</returns>
    public Tween RemoveOnLoop(Action onComplete) =>
      RemoveHandler<OnLoop>(onComplete);

    /// <summary>
    /// Register a callback to be invoked when this tween reaches the end value.
    /// </summary>
    /// <returns>This tween.</returns>
    public Tween OnComplete(Action onComplete) =>
      AddHandler<OnComplete>(onComplete);

    /// <summary>
    /// Remove <c cref="OnComplete">OnComplete</c> callback.
    /// </summary>
    /// <seealso cref="OnComplete"/>
    /// <returns>This tween.</returns>
    public Tween RemoveOnComplete(Action onComplete) =>
      RemoveHandler<OnComplete>(onComplete);

    /// <summary>
    /// Register a callback to be invoked when this tween dies, either due to
    /// completion or because it was <see cref="Kill">killed</see>.
    /// </summary>
    /// <seealso cref="Kill"/>
    /// <returns>This tween.</returns>
    public Tween OnKill(Action onKill) =>
      AddHandler<OnKill>(onKill);

    /// <summary>
    /// Remove <c cref="OnKill">OnKill</c> callback.
    /// </summary>
    /// <seealso cref="OnKill"/>
    /// <returns>This tween.</returns>
    public Tween RemoveOnKill(Action onKill) =>
      RemoveHandler<OnKill>(onKill);

    Tween AddHandler<T>(Action handler) where T : struct, ICallback {
      if (Entity(out var entity)) {
        Core.AddHandler<T>(entity, handler);
      }
      return this;
    }

    Tween RemoveHandler<T>(Action handler) where T : struct, ICallback {
      if (Entity(out var entity)) {
        Core.RemoveHandler<T>(entity, handler);
      }
      return this;
    }

#endregion
#region Private

    bool Entity(out int entity) {
      if (!TryEntity(out entity)) {
        Debug.LogWarning($"Tween is not alive");
        return false;
      }
      return true;
    }

    bool TryEntity(out int entity) => Core.TryEntity(_entity, out entity);

#endregion
  }
}
