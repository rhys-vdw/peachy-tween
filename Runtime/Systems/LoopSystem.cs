using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class LoopSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _loopFilter;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _loopFilter = _world.Filter<Active>().Inc<Loop>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _loopFilter) {
        ref var loop = ref _world.GetComponent<Loop>(entity);
        if (loop.LoopCount == 0) {
          Debug.LogWarning($"Invalid Loop component found with 0 remaining loops");
        }

        ref var tweenState = ref _world.GetComponent<TweenState>(entity);

        // Calculate current loop.
        var prevLoop = loop.CurrentLoop;
        // NOTE: Using clamp here to ensure we don't do callback too many times.
        // TODO: Complete this.
        var nextLoop = Mathf.Clamp(
          Mathf.FloorToInt(tweenState.Elapsed / loop.LoopDuration),
          -1,
          loop.LoopCount
        );

        // Execute OnLoop callbacks.
        // TODO: This is probably wrong? Need to think about/test all different
        // permutations (including rewind GoTo etc)
        if (nextLoop > -1 && nextLoop <= loop.LoopCount && prevLoop is int pl) {
          var deltaLoop = Mathf.Abs(pl - nextLoop);
          for (var i = 0; i < deltaLoop; i++) {
            _world.Invoke<OnLoop>(entity);
          }
        }

        // Update loop progress.
        if (
          loop.LoopCount == -1 ||
          nextLoop >= 0 &&
          nextLoop < loop.LoopCount
        ) {
          ref var active = ref _world.GetComponent<Active>(entity);
          active.Progress = tweenState.Elapsed % loop.LoopDuration;
          loop.CurrentLoop = nextLoop;
        } else {
          loop.CurrentLoop = null;
        }
      }
    }
  }
}