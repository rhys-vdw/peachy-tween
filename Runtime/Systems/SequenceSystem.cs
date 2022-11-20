using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedFilters;
using UnityEngine;

namespace PeachyTween {
  internal class SequenceSystem : IEcsSystem, IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void PreInit(EcsSystems systems) {
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
      var memberPool = _world.GetPool<SequenceMember>();

      // TODO: Reorder here.

      foreach (var entity in _filter) {
        ref var member = ref memberPool.Get(entity);
        if (activePool.Has(member.SequenceEntity)) {
          ref var state = ref statePool.Get(entity);
          ref var sequenceState = ref statePool.Get(member.SequenceEntity);
          ref var sequenceActive = ref activePool.Get(member.SequenceEntity);

          // Calculate previous state.
          var prevState = state.Elapsed switch {
            var e when e < 0 => State.Before,
            var e when e > state.Duration => State.After,
            _ => State.Active,
          };

          // Update tween elapsed time.
          var easedElapse = sequenceActive.Progress * sequenceState.Duration;
          state.Elapsed = easedElapse;

          // Calculate next state.
          var nextState = state.Elapsed switch {
            var e when e < 0 => State.Before,
            var e when e > state.Duration => State.After,
            _ => State.Active,
          };

          Debug.Log($"id={entity}, nextState={nextState}");

          if (nextState == State.Active) {
            // Mark tween incomplete.
            completePool.Del(entity);

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

          // Update tween values.
          ProgressSystem.ProgressTween(_world, entity);
        }
      }
    }
  }
}