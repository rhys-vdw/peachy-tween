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
          var elapsed = sequenceState.Elapsed - member.StartTime;
          if (elapsed > 0 && elapsed <= tweenState.Duration) {
            tweenState.Elapsed = elapsed;
            activePool.Add(entity);
          }
          _world.SetHasComponent<Complete>(entity, elapsed > tweenState.Duration);
        }
      }
    }
  }
}