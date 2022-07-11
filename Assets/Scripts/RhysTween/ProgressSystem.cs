using Leopotam.EcsLite;
using UnityEngine;

namespace RhysTween {
  public delegate T Lerp<T>(T from, T to, float t);

  internal class ProgressSystem : IEcsSystem, IEcsInitSystem, IEcsRunSystem {
    interface ISubSystem {
      void Run(EcsWorld world, in Time time);
    }

    class SubSystem<T> : ISubSystem {
      readonly EcsFilter _filter;
      readonly Lerp<T> _lerp;

      public SubSystem(EcsFilter filter, Lerp<T> lerp) {
        _filter = filter;
        _lerp = lerp;
      }

      public void Run(EcsWorld world, in Time time) {
        var statePool = world.GetPool<TweenState<T>>();
        var onChangePool = world.GetPool<OnChange<T>>();
        foreach (var entity in _filter) {
          ref var state = ref statePool.Get(entity);
          state.NormalizedTime = (float) ((time.Current - state.StartTime) / state.Duration);
          if (state.NormalizedTime > 1) {
            state.NormalizedTime = 1;
          }
          if (state.NormalizedTime == 1) {
            var completedPool = world.GetPool<Complete>();
            completedPool.Add(entity);
          } else if (onChangePool.Has(entity)) {
            var value = _lerp(state.From, state.To, state.NormalizedTime);
            ref var onChange = ref onChangePool.Get(entity);
            onChange.Callback(value);
          }
        }
      }
    }

    EcsWorld _world;
    TweenRunner _runner;
    ISubSystem[] _subSystems;

    public void Init(EcsSystems systems) {
      _world = systems.GetWorld();
      _runner = systems.GetShared<TweenRunner>();
      _subSystems = new ISubSystem[] {
        CreateTypeSubsystem<float>(Mathf.Lerp),
        CreateTypeSubsystem<Vector2>(Vector2.Lerp),
        CreateTypeSubsystem<Vector3>(Vector3.Lerp),
        CreateTypeSubsystem<Quaternion>(Quaternion.Lerp)
      };
    }

    ISubSystem CreateTypeSubsystem<T>(Lerp<T> lerp) {
      var filter = _world.Filter<TweenState<T>>().End();
      return CreateSubsystem(filter, lerp);
    }

    ISubSystem CreateSubsystem<T>(EcsFilter filter, Lerp<T> lerp) =>
      new SubSystem<T>(filter, lerp);

    public void Run(EcsSystems systems) {
      ref var time = ref _runner.GetTime();
      foreach (var subSystem in _subSystems) {
        subSystem.Run(_world, time);
      }
    }
  }
}