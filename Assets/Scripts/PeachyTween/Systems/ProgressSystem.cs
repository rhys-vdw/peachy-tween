using Leopotam.EcsLite;

namespace PeachyTween {
  internal class ProgressSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    RunState _runState;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().End();
      _runState = systems.GetShared<RunState>();
    }

    public void Run(EcsSystems systems) {
      var deltaTime = _runState.DeltaTime;
      var statePool = _world.GetPool<TweenState>();
      foreach (var entity in _filter) {
        ref var state = ref statePool.Get(entity);
        state.Elapsed += deltaTime;
        state.Progress = state.Elapsed / state.Duration;
        if (state.Elapsed >= state.Duration) {
          state.Elapsed = state.Duration;
          state.Progress = 1f;
          _world.AddComponent<Complete>(entity);
        }
      }
    }
  }
}