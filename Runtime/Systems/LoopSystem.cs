using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class LoopSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public LoopSystem(EcsWorld.Mask mask) {
      _filter = mask.Inc<Loop>().Inc<Complete>().End();
    }


    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Inc<Loop>().Inc<Complete>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var loop = ref _world.GetComponent<Loop>(entity);
        if (loop.Remaining == 0) {
          Debug.LogWarning($"Invalid Loop component found with 0 remaining loops");
        }

        // Decrement non-infinite loops.
        if (loop.Remaining > 0) {
          loop.Remaining--;
        }

        if (loop.Remaining == 0) {
          // Stop looping.
          _world.DelComponent<Loop>(entity);
        } else {
          // Rewind.
          _world.DelComponent<Complete>(entity);
          ref var state = ref _world.GetComponent<TweenState>(entity);
          state.Elapsed %= state.Duration;
        }
      }
    }
  }
}