using System;
using Leopotam.EcsLite;

namespace PeachyTween {
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
}