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

    public Sequence Append(Tween tween) {
      if (
        Entity(out var sequenceEntity) &&
        tween.Entity(out var tweenEntity)
      ) {
        Core.Append(sequenceEntity, tweenEntity);
      }
      return this;
    }

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
