using Leopotam.EcsLite;
using UnityEngine;

namespace RhysTween {
  public delegate T Lerp<T>(T from, T to, float t);

  internal class ProgressSystem<T> : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    EcsWorld _world;
    TweenRunner _runner;
    readonly EcsFilter _filter;
    readonly Lerp<T> _lerp;

    public ProgressSystem(EcsFilter filter, Lerp<T> lerp) {
      _filter = filter;
      _lerp = lerp;
    }

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _runner = systems.GetShared<TweenRunner>();
    }

    public void Run(EcsSystems systems) {
      ref var time = ref _runner.GetTime();
      var statePool = _world.GetPool<TweenState<T>>();
      var onChangePool = _world.GetPool<OnChange<T>>();
      foreach (var entity in _filter) {
        ref var state = ref statePool.Get(entity);

        // Get normalized time.
        state.NormalizedTime = (float) ((time.Current - state.StartTime) / state.Duration);
        if (state.NormalizedTime > 1) {
          state.NormalizedTime = 1;
        }

        // Trigger change events.
        if (onChangePool.Has(entity)) {
          var value = _lerp(state.From, state.To, state.NormalizedTime);
          ref var onChange = ref onChangePool.Get(entity);
          onChange.Callback(value);
        }

        // Complete handler.
        if (state.NormalizedTime == 1) {
          var completedPool = _world.GetPool<Complete>();
          completedPool.Add(entity);
        }
      }
    }
  }
}