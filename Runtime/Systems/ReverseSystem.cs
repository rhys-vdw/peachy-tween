using Leopotam.EcsLite;

namespace PeachyTween {
  internal class ReverseSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public ReverseSystem(EcsWorld.Mask mask) {
      _filter = mask.Inc<Reverse>().End();
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var active = ref _world.GetComponent<Active>(entity);
        active.Progress = 1 - active.Progress;
      }
    }
  }
}