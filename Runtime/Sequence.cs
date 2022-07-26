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

#region Tween

    public static explicit operator Tween(Sequence sequence) =>
      sequence.ToTween();

    public Tween ToTween() => new Tween(_entity);

#endregion
#region Sequence operations

    public Sequence Append(Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Append(sequenceEntity, tweenEntity);
      }
      return this;
    }

    public Sequence Join(Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Join(sequenceEntity, tweenEntity);
      }
      return this;
    }

    public Sequence Insert(float time, Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Insert(sequenceEntity, tweenEntity, time);
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
