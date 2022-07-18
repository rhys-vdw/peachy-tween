using Leopotam.EcsLite;

namespace PeachyTween {

  internal class CompleteSystem : IEcsSystem , IEcsPreInitSystem , IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world
        .Filter<Active>()
        .Inc<Complete>()
        .Exc<Preserve>()
        .Exc<Kill>()
        .End();
    }

    public void Run(EcsSystems systems) {
      var killPool = _world.GetPool<Kill>();
      foreach (var entity in _filter) {
        killPool.Add(entity);
      }
    }
  }
}

