using Leopotam.EcsLite;

namespace RhysTween {
  internal class ChangeSystem<T> : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    readonly EcsFilter _filter;
    readonly Lerp<T> _lerp;

    public ChangeSystem(EcsFilter filter, Lerp<T> lerp) {
      _filter = filter;
      _lerp = lerp;
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      var configPool = _world.GetPool<TweenConfig<T>>();
      var statePool = _world.GetPool<TweenState>();
      foreach (var entity in _filter) {
        ref var config = ref configPool.Get(entity);
        ref var state = ref statePool.Get(entity);
        var value = _lerp(config.From, config.To, state.Progress);
        config.OnChange(value);
      }
    }
  }
}
