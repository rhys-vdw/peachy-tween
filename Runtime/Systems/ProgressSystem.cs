using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class ProgressSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public ProgressSystem(EcsWorld.Mask mask) {
      _filter = mask.End();
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      var statePool = _world.GetPool<TweenState>();
      var activePool = _world.GetPool<Active>();
      foreach (var entity in _filter) {
        ref var state = ref statePool.Get(entity);
        ref var active = ref activePool.Get(entity);
        active.Progress = Mathf.Clamp01(state.Elapsed / state.Duration);
      }
    }
  }
}