using System;
using UnityEngine;
using Leopotam.EcsLite;

namespace PeachyTween {
  /// <summary>
  /// Reference to a sequence.
  /// </summary>
  public readonly struct Sequence {
    readonly EcsPackedEntity _entity;

    internal Sequence(EcsPackedEntity entity) {
      _entity = entity;
    }

#region Sequence

    public Sequence Append(Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Append(sequenceEntity, tweenEntity);
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
    // public Sequence Restart() => Rewind().Resume();

    /// <summary>
    /// Return the tween to the start.<para/>
    /// Equivalent to <c cref="GoTo">GoTo(0)</c>.<para/>
    /// </summary>
    /// <seealso cref="GoTo"/>
    /// <returns>This tween.</returns>
    public Sequence Rewind() => GoTo(0);

    /// <summary>
    /// Set the interal time of a tween.<para/>
    /// </summary>
    /// <param name="elapsed">
    /// The time to set the tween to. <c>0</c> will rewind the tween to the
    /// start, and passing the tween's duration will fast-forward to the end.
    /// </param>
    /// <returns>This tween.</returns>
    public Sequence GoTo(float elapsed) {
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
    public Sequence Complete() {
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
    public Sequence Reverse() {
      if (Entity(out var entity)) {
        Core.Reverse(entity);
      }
      return this;
    }

    /// <summary>
    /// Run this tween from end to start.
    /// </summary>
    /// <returns>This tween.</returns>
    public Sequence From() {
      if (Entity(out var entity)) {
        Core.From(entity);
      }
      return this;
    }

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
    public Sequence Preserve() {
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
    public Sequence ClearPreserve() {
      if (Entity(out var entity)) {
        Core.ClearPreserve(entity);
      }
      return this;
    }

#endregion
#region Private

    bool Entity(out int entity) {
      if (!TryEntity(out entity)) {
        Debug.LogWarning($"Sequence is not alive");
        return false;
      }
      return true;
    }

    bool TryEntity(out int entity) => Core.TryEntity(_entity, out entity);

#endregion
  }
}
