using System;
using Leopotam.EcsLite;

namespace PeachyTween {
  internal interface ICallback {
    Action Callback { get; set; }
  }

  internal struct OnLoop : ICallback {
    public Action Callback { get; set; }
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