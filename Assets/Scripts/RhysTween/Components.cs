using System;
using Leopotam.EcsLite;

namespace RhysTween {
  // State
  internal struct Active { }
  internal struct Paused { }
  internal struct Complete { }
  internal struct Preserve { }

  // Groups
  internal struct Update { }
  internal struct LateUpdate { }
  internal struct FixedUpdate { }
  internal struct Slerp { }

  // Tween
  internal struct TweenConfig<T> {
    public T From;
    public T To;
    public Action<T> OnChange;

    public TweenConfig(T from, T to, Action<T> onChange) {
      From = from;
      To = to;
      OnChange = onChange;
    }
  }

  internal struct TweenState {
    public float Elapsed;
    public float Duration;
    public float Progress => Elapsed / Duration;

    public TweenState(float duration) {
      Elapsed = 0;
      Duration = duration;
    }
  }

  internal struct Loop {
    public int Remaining;
  }

  // Callbacks
  interface ICallback {
    Action Callback { get; set; }
  }

  internal struct OnComplete : ICallback {
    public Action Callback { get; set; }
  }

  internal struct OnKill : ICallback {
    public Action Callback { get; set; }
  }

  static class CallbackUtility {
    internal static void Invoke<T>(this EcsWorld world, int entity) where T : struct, ICallback {
      var callbackPool = world.GetPool<T>();
      if (callbackPool.Has(entity)) {
        callbackPool.Get(entity).Callback();
      }
    }

    internal static void AddHandler<T>(this EcsWorld world, int entity, Action callback) where T : struct, ICallback {
      ref var cb = ref world.EnsureComponent<T>(entity);
      cb.Callback += callback;
    }
  }
}