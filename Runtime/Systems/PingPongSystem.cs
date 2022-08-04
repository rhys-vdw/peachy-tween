using Leopotam.EcsLite;

namespace PeachyTween {
  internal class PingPongSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Loop>().Inc<PingPong>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var loop = ref _world.GetComponent<Loop>(entity);
        // TODO: But this needs to be correct if you did `From` or `Reverse`.
        // Maybe ping-pong should not manipulate Reverse, but instead adjust
        // Progress??
        if (loop.CurrentLoop is int currentLoop) {
          _world.SetHasComponent<Reverse>(entity, currentLoop % 2 == 1);
        }
      }
    }
  }
}