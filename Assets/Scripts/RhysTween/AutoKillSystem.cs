using Leopotam.EcsLite;

namespace RhysTween {

  internal class AutoKillSystem : IEcsSystem , IEcsPreInitSystem , IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Complete>().Exc<Preserve>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        _world.KillTween(entity);
      }
    }
  }
}
