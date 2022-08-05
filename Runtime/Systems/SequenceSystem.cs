using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class SequenceSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<SequenceMember>().End();
    }

    public void Run(EcsSystems systems) {
      var statePool = _world.GetPool<TweenState>();
      var activePool = _world.GetPool<Active>();
      foreach (var entity in _filter) {
        ref var member = ref _world.GetComponent<SequenceMember>(entity);
        if (activePool.Has(member.SequenceEntity)) {
          ref var tweenState = ref statePool.Get(entity);
          ref var sequenceState = ref statePool.Get(member.SequenceEntity);
          ref var sequenceActive = ref activePool.Get(member.SequenceEntity);

          // Update tween elapsed time.
          var easedElapse = sequenceActive.Progress * sequenceState.Duration;
          tweenState.Elapsed = easedElapse - member.StartTime;

          // Work out if the tween is complete.
          var isReversed = _world.HasComponent<Reverse>(member.SequenceEntity);
          var isComplete = isReversed
            ? tweenState.Elapsed <= 0
            : tweenState.Elapsed >= tweenState.Duration;

          if (isComplete) {
            if (!_world.HasComponent<Complete>(entity)) {
              // If we've exceeded duration, but the tween is yet to be marked
              // complete, activate it so it can be completed.
              activePool.Add(entity);

              // Mark the tween complete.
              _world.AddComponent<Complete>(entity);
            }
          } else {
            // Mark tween incomplete.
            _world.DelComponent<Complete>(entity);

            // If the tween is active, mark it active so it will progress.
            if (tweenState.Elapsed > 0 && tweenState.Elapsed < tweenState.Duration) {
              activePool.Add(entity);
            }
          }
        }
      }
    }
  }
}