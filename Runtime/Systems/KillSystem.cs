using Leopotam.EcsLite;

namespace PeachyTween {

  internal class KillSystem : IEcsSystem, IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Kill>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        _world.DelEntity(entity);
      }
    }
  }
}
