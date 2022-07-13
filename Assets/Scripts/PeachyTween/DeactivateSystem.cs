using Leopotam.EcsLite;

namespace PeachyTween {

  internal class DeactivateSystem : IEcsSystem , IEcsPreInitSystem , IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().End();
    }

    public void Run(EcsSystems systems) {
      var activePool = _world.GetPool<Active>();
      foreach (var entity in _filter) {
        activePool.Del(entity);
      }
    }
  }
}
