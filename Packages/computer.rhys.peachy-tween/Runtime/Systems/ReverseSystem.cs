using Leopotam.EcsLite;

namespace PeachyTween {
  internal class ReverseSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Reverse>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var active = ref _world.GetComponent<Active>(entity);
        active.Progress = 1 - active.Progress;
      }
    }
  }
}