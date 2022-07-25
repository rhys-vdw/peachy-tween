using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class LoopSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;

    public LoopSystem(EcsWorld.Mask mask) {
      _filter = mask.Inc<Loop>().End();
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(EcsSystems systems) {
      foreach (var entity in _filter) {
        ref var loop = ref _world.GetComponent<Loop>(entity);
        if (loop.LoopCount == 0) {
          Debug.LogWarning($"Invalid Loop component found with 0 remaining loops");
        }

        ref var tweenState = ref _world.GetComponent<TweenState>(entity);

        // Calculate current loop.
        var prevLoop = loop.CurrentLoop;
        // NOTE: Using clamp here to ensure we don't do callback too many times.
        var nextLoop = Mathf.Clamp(
          Mathf.FloorToInt(tweenState.Elapsed / loop.LoopDuration),
          -1,
          loop.LoopCount
        );

        // Execute OnLoop callbacks.
        // TODO: This is probably wrong? Need to think about/test all different
        // permutations (including rewind GoTo etc)
        if (
          nextLoop > -1 &&
          (loop.LoopCount == -1 || nextLoop <= loop.LoopCount) &&
          prevLoop is int pl
        ) {
          var deltaLoop = Mathf.Abs(pl - nextLoop);
          for (var i = 0; i < deltaLoop; i++) {
            _world.Invoke<OnLoop>(entity);
          }
        }

        ref var active = ref _world.GetComponent<Active>(entity);

        // Update loop progress.
        if (
          loop.LoopCount == -1 ||
          nextLoop >= 0 &&
          nextLoop < loop.LoopCount
        ) {
          active.Progress = (tweenState.Elapsed % loop.LoopDuration) / loop.LoopDuration;
          loop.CurrentLoop = nextLoop;
        } else {
          // TODO: I don't think this should be necessary, but for some reason
          // when reversed there is one final value of 0 before the tween is
          // deactivated. Possibly an error in Reverse handling.
          active.Progress = loop.LoopCount == -1 ? 0 : 1;
          loop.CurrentLoop = null;
        }

        // Flip progress for ping-pong.
        if (_world.HasComponent<PingPong>(entity)) {
          var isReversed = _world.HasComponent<Reverse>(entity);
          var clampedLoop = Mathf.Clamp(nextLoop, 0, loop.LoopCount - 1);
          var isFlipped = clampedLoop % 2 == 1;
          if (isReversed != isFlipped) {
            active.Progress = 1f - active.Progress;
          }
        }
      }
    }
  }
}