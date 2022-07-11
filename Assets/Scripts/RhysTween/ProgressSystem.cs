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
      var configPool = _world.GetPool<TweenConfig<T>>();
      var statePool = _world.GetPool<TweenState>();
      foreach (var entity in _filter) {
        ref var config = ref configPool.Get(entity);
        ref var state = ref statePool.Get(entity);

        // Get normalized time.
        var t = (float) ((time.Current - state.StartTime) / state.Duration);
        if (t > 1) {
          t = 1;
        }

        // Trigger change events.
        var value = _lerp(config.From, config.To, t);
        config.OnChange(value);

        // Complete handler.
        if (t == 1) {
          var completedPool = _world.GetPool<Complete>();
          completedPool.Add(entity);
        }
      }
    }
  }
}