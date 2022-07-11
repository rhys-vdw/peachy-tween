using Leopotam.EcsLite;
using UnityEngine;

namespace RhysTween {
  internal class LoopSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _loopFilter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _loopFilter = _world.Filter<Loop>().Inc<Complete>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _loopFilter) {
        ref var loop = ref _world.GetComponent<Loop>(entity);
        if (loop.Remaining == 0) {
          Debug.LogWarning($"Invalid Loop component found with 0 remaining loops");
          _world.DelComponent<Loop>(entity);
          continue;
        }

        // Decrement non-infinite loops.
        if (loop.Remaining > 0) {
          loop.Remaining--;
          if (loop.Remaining == 0) {
            _world.DelComponent<Loop>(entity);
          }
        }

        // Remove completion.
        _world.DelComponent<Complete>(entity);
        ref var state = ref _world.GetComponent<TweenState>(entity);
        state.Elapsed = 0;
      }
    }
  }
}