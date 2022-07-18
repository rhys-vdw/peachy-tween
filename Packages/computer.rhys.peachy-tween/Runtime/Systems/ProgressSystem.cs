using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class ProgressSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().End();
    }

    public void Run(EcsSystems systems) {
      var statePool = _world.GetPool<TweenState>();
      foreach (var entity in _filter) {
        ref var state = ref statePool.Get(entity);
        state.Progress = Mathf.Min(state.Elapsed / state.Duration, 1f);
      }
    }
  }
}