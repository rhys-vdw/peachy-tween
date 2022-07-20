using Leopotam.EcsLite;

namespace PeachyTween {
  internal class EaseSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Eased>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var ease = ref _world.GetComponent<Eased>(entity);
        ref var active = ref _world.GetComponent<Active>(entity);
        active.Progress = ease.Func(active.Progress);
      }
    }
  }
}
