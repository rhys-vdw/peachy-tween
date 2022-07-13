using System;

namespace PeachyTween {
  // State
  internal struct Active { }
  internal struct Paused { }
  internal struct Complete { }
  internal struct Preserve { }
  internal struct Reverse { }
  internal struct PingPong { }

  // Groups
  internal struct Update { }
  internal struct LateUpdate { }
  internal struct FixedUpdate { }

  // Lerp filter
  internal struct Slerp { }
  internal struct Angle { }

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
    public float Progress;

    public TweenState(float duration) {
      Elapsed = 0;
      Duration = duration;
      Progress = 0;
    }
  }

  internal struct Loop {
    public int Remaining;
  }

  internal struct Eased {
    public EaseFunc Func;
  }
}