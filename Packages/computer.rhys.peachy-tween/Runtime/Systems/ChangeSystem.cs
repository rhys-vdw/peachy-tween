using Leopotam.EcsLite;

namespace PeachyTween {
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
      var activePool = _world.GetPool<Active>();
      foreach (var entity in _filter) {
        ref var config = ref configPool.Get(entity);
        ref var active = ref activePool.Get(entity);
        var value = _lerp(config.From, config.To, active.Progress);
        config.OnChange(value);
      }
    }
  }
}
