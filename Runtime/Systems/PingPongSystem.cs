using Leopotam.EcsLite;

namespace PeachyTween {
  internal class PingPongSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public PingPongSystem(EcsWorld.Mask mask) {
      _filter = mask.Inc<Complete>().Inc<Loop>().Inc<PingPong>().End();
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        _world.ToggleComponent<Reverse>(entity);
      }
    }
  }
}