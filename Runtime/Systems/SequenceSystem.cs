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

    enum State {
      Before,
      Active,
      After
    }

    public void Run(EcsSystems systems) {
      var statePool = _world.GetPool<TweenState>();
      var activePool = _world.GetPool<Active>();
      var completePool = _world.GetPool<Complete>();
      foreach (var entity in _filter) {
        ref var member = ref _world.GetComponent<SequenceMember>(entity);
        if (activePool.Has(member.SequenceEntity)) {
          ref var tweenState = ref statePool.Get(entity);
          ref var sequenceState = ref statePool.Get(member.SequenceEntity);
          ref var sequenceActive = ref activePool.Get(member.SequenceEntity);

          // Calculate previous state.
          var prevState = tweenState.Elapsed switch {
            var e when e < 0 => State.Before,
            var e when e > tweenState.Duration => State.After,
            _ => State.Active,
          };

          // Update tween elapsed time.
          var easedElapse = sequenceActive.Progress * sequenceState.Duration;
          tweenState.Elapsed = easedElapse - member.StartTime;

          // Calculate next state.
          var nextState = tweenState.Elapsed switch {
            var e when e < 0 => State.Before,
            var e when e > tweenState.Duration => State.After,
            _ => State.Active,
          };

          if (nextState == State.Active) {
            // Mark tween incomplete.
            _world.DelComponent<Complete>(entity);

            // Mark tween active.
            activePool.Add(entity);
          } else {
            // Ensure that tween is activated to do one last update.
            if (nextState != prevState) {
              activePool.Add(entity);
            }

            // Work out if the tween is complete.
            if (!completePool.Has(entity)) {
              var isReversed = _world.HasComponent<Reverse>(member.SequenceEntity);
              var isComplete = isReversed
                ? nextState == State.Before
                : nextState == State.After;

              if (isComplete) {
                completePool.Add(entity);
              }
            }
          }
        }
      }
    }
  }
}