using Leopotam.EcsLite;

namespace PeachyTween {

  internal class ActivateGroupSystem : IEcsSystem , IEcsPreInitSystem , IEcsRunSystem {
    EcsWorld _world;
    RunState _runState;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _runState = systems.GetShared<RunState>();
    }

    public void Run(EcsSystems systems) {
      var activePool = _world.GetPool<Active>();
      if (_runState.GroupFilter != null) {
        foreach (var entity in _runState.GroupFilter) {
          activePool.Add(entity);
        }
      }
      if (_runState.Entity is int single) {
        activePool.Add(single);
      }
    }
  }
}
