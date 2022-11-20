using Leopotam.EcsLite;
using UnityEngine;

namespace PeachyTween {
  internal class ProgressSystem : IEcsSystem, IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    RunState _runState;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Active>().Exc<SequenceMember>().End();
      _runState = systems.GetShared<RunState>();
    }

    public void Run(EcsSystems systems) {
      var statePool = _world.GetPool<TweenState>();
      var deltaTime = _runState.DeltaTime;
      foreach (var entity in _filter) {
        ref var state = ref statePool.Get(entity);
        ProgressTween(_world, entity, state.Elapsed + deltaTime);
      }
    }

    public static void ProgressTween(EcsWorld world, int entity, float elapsed) {
      var activePool = world.GetPool<Active>();
      var easePool = world.GetPool<Ease>();
      var loopPool = world.GetPool<Loop>();
      var reversePool = world.GetPool<Reverse>();
      var statePool = world.GetPool<TweenState>();

      // Mark complete.
      ref var state = ref statePool.Get(entity);
      state.Elapsed = elapsed;
      if (state.Elapsed >= state.Duration) {
        world.AddComponent<Complete>(entity);
      }

      // Normalize progress.
      ref var active = ref activePool.Get(entity);
      active.Progress = Mathf.Clamp01(state.Elapsed / state.Duration);

      // Reverse progress.
      if (reversePool.Has(entity)) {
        active.Progress = 1 - active.Progress;
      }

      // Loop progress.
      if (loopPool.Has(entity)) {
        Loop(world, entity);
      }

      // Ease progress.
      if (easePool.Has(entity)) {
        ref var ease = ref world.GetComponent<Eased>(entity);
        active.Progress = ease.Func(active.Progress);
      }
     }

    static void Loop(EcsWorld world, int entity) {
      ref var loop = ref world.GetComponent<Loop>(entity);
      if (loop.LoopCount == 0) {
        Debug.LogWarning($"Invalid Loop component found with 0 remaining loops");
      }

      ref var tweenState = ref world.GetComponent<TweenState>(entity);

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
          world.Invoke<OnLoop>(entity);
        }
      }

      ref var active = ref world.GetComponent<Active>(entity);

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
      if (world.HasComponent<PingPong>(entity)) {
        var isReversed = world.HasComponent<Reverse>(entity);
        var clampedLoop = Mathf.Clamp(nextLoop, 0, loop.LoopCount - 1);
        var isFlipped = clampedLoop % 2 == 1;
        if (isReversed != isFlipped) {
          active.Progress = 1f - active.Progress;
        }
      }
    }
  }
}