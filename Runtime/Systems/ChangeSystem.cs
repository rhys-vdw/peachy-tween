using Leopotam.EcsLite;

namespace PeachyTween {
  internal class ChangeSystem<T> : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    readonly EcsFilter _filter;
    readonly LerpFunc<T> _defaultLerp;

    public ChangeSystem(EcsFilter filter, LerpFunc<T> defaultLerp) {
      _filter = filter;
      _defaultLerp = defaultLerp;
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      var configPool = _world.GetPool<TweenConfig<T>>();
      var activePool = _world.GetPool<Active>();
      var lerpPool = _world.GetPool<OverrideLerp<T>>();
      foreach (var entity in _filter) {
        var lerp = lerpPool.Has(entity)
          ? lerpPool.Get(entity).Func
          : _defaultLerp;

        ref var config = ref configPool.Get(entity);
        ref var active = ref activePool.Get(entity);
        var value = lerp(config.From, config.To, active.Progress);
        config.OnChange(value);
      }
    }
  }
}
