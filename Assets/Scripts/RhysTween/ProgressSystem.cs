using Leopotam.EcsLite;
using UnityEngine;

namespace RhysTween {

  internal class ProgressSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    TweenRunner _runner;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _runner = systems.GetShared<TweenRunner>();
      _filter = _world.Filter<TweenState>().End();
    }

    public void Run(EcsSystems systems) {
      ref var time = ref _runner.GetTime();
      var statePool = _world.GetPool<TweenState>();
      foreach (var entity in _filter) {
        ref var state = ref statePool.Get(entity);
        state.Elapsed += time.DeltaTime;
        if (state.Elapsed >= state.Duration) {
          state.Elapsed = state.Duration;
          _world.AddComponent<Complete>(entity);
        }
      }
    }
  }
}