using Leopotam.EcsLite;

namespace RhysTween {
  internal class OnCompleteSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _completeFilter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _completeFilter = _world.Filter<Active>().Inc<Complete>().End();
    }

    public void Run(EcsSystems systems) {
      var callbackPool = _world.GetPool<OnComplete>();
      foreach (var entity in _completeFilter) {
        if (callbackPool.Has(entity)) {
          callbackPool.Get(entity).Callback();
        }
      }
    }
  }
}