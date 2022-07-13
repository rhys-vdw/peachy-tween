using Leopotam.EcsLite;

namespace PeachyTween {
  internal class PingPongSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Complete>().Inc<Loop>().Inc<PingPong>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        _world.ToggleComponent<Reverse>(entity);
      }
    }
  }
}