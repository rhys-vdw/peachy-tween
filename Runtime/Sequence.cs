using System;
using UnityEngine;
using Leopotam.EcsLite;

namespace PeachyTween {
  /// <summary>
  /// Reference to a sequence.
  /// </summary>
  /// <remarks>
  /// You must create a sequence using <see cref="Peachy.Sequence()"><c>Peachy.Sequence</c></see>.
  /// </remarks>
  /// <example>
  /// <code>
  /// // Create a new sequence.
  /// var sequence = Peachy.Sequence()
  ///     .Append(
  ///         transform
  ///           .TweenPosition(new Vector3(5, 0, 1), 2f)
  ///           .Ease(Ease.SineOut)
  ///     ).AppendCallback(
  ///         () => Debug.Log("Done moving!")
  ///     ).Append(
  ///         transform.TweenRotation(new Vector3(90, 0, 0), 0.5f)
  ///     ).Insert(
  ///         transform.ShakeRotation(
  ///             magnitude: 3f,
  ///             duration: 1.5f,
  ///             oscillationCount: 10,
  ///             decay: 1f,
  ///             randomness: 0.5f
  ///         ),
  ///         time: 1f
  ///     );
  ///
  /// // Get its tween to manipulate it.
  /// var tween = sequence.ToTween();
  /// tween.LoopForever();
  /// </code>
  /// </example>
  /// <seealso cref="Peachy.Sequence()"/>
  public readonly struct Sequence {
    readonly EcsPackedEntity _entity;

    internal Sequence(EcsPackedEntity entity) {
      _entity = entity;
    }

#region Tween

    /// <summary>
    /// Cast this sequence into its underlying tween.
    /// </summary>
    /// <seealso cref="ToTween"/>
    /// <param name="sequence">The sequnce to cast.</param>
    /// <returns>The sequence's tween</returns>
    public static explicit operator Tween(Sequence sequence) =>
      sequence.ToTween();

    /// <summary>
    /// Get the underlying tween that can be used to control this sequence.
    /// </summary>
    /// <returns>The sequence's tween</returns>
    public Tween ToTween() => new Tween(_entity);

#endregion
#region Tween operations

    /// <summary>
    /// Add a tween to the sequence that starts when the last appended tween ends.
    /// </summary>
    /// <param name="tween">The tween to append.</param>
    /// <returns>This sequence.</returns>
    public Sequence Append(Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Append(sequenceEntity, tweenEntity);
      }
      return this;
    }

    /// <summary>
    /// Add a tween to the sequence that starts at the same time as the last appended tween.
    /// </summary>
    /// <param name="tween">The tween to join.</param>
    /// <returns>This sequence.</returns>
    public Sequence Join(Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Join(sequenceEntity, tweenEntity);
      }
      return this;
    }

    /// <summary>
    /// Add a tween to the sequence that starts at the specified time.
    /// </summary>
    /// <param name="time">The start time of the tween in seconds.</param>
    /// <param name="tween">The tween to insert.</param>
    /// <returns>This sequence.</returns>
    public Sequence Insert(float time, Tween tween) {
      if (time < 0) {
        throw new ArgumentOutOfRangeException(nameof(time), "Must be greater than zero");
      }
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Insert(sequenceEntity, tweenEntity, time);
      }
      return this;
    }

#endregion
#region Interval

    /// <summary>
    /// Add a time delay to the sequence after the last appended tween.
    /// </summary>
    /// <param name="delay">The duration of the interval in seconds.</param>
    /// <returns>This sequence.</returns>
    public Sequence AppendInterval(float delay) {
      if (Entity(out var sequenceEntity)) {
        Core.AppendInterval(sequenceEntity, delay);
      }
      return this;
    }

#endregion
#region Callbacks

    /// <summary>
    /// Add a callback that will be invoked after the appended tween completes.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <returns>This sequence.</returns>
    public Sequence AppendCallback(Action callback) {
      if (Entity(out var sequenceEntity)) {
        Core.AppendCallback(sequenceEntity, callback);
      }
      return this;
    }

    /// <summary>
    /// Add a callback to the sequence that will be invoked at the specified time.
    /// </summary>
    /// <param name="time">The time to invoke the callback in seconds.</param>
    /// <param name="callback">The callback.</param>
    /// <returns>This sequence.</returns>
    public Sequence InsertCallback(float time, Action callback) {
      if (Entity(out var sequenceEntity)) {
        Core.InsertCallback(sequenceEntity, time, callback);
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
