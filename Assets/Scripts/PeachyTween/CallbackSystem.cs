using Leopotam.EcsLite;

namespace PeachyTween {
  internal class CallbackSystem<T>
    : IEcsSystem
    , IEcsPreInitSystem
    , IEcsRunSystem
    where T : struct, ICallback
  {
    EcsWorld _world;
    readonly EcsFilter _filter;

    public CallbackSystem(EcsFilter filter) {
      _filter = filter;
    }

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        _world.Invoke<T>(entity);
      }
    }
  }
}