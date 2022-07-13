using Leopotam.EcsLite;

namespace PeachyTween {
  internal class EaseSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Ease>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var ease = ref _world.GetComponent<Ease>(entity);
        ref var state = ref _world.GetComponent<TweenState>(entity);
        state.Progress = ease.Func(state.Progress);
      }
    }
  }
}
