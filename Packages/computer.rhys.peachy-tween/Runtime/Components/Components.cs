using System;

namespace PeachyTween {
  // State
  struct Active {
    public float Progress;
  }

  struct Paused { }
  struct Complete { }
  struct Preserve { }
  struct Reverse { }
  struct PingPong { }
  struct Kill { }
  struct OverrideLerp<T> {
    public Lerp<T> Func;
  }

  // Groups
  struct Update { }
  struct LateUpdate { }
  struct FixedUpdate { }

  // Lerp filter
  struct Rotate { }

  // Tween
  struct TweenConfig<T> {
    public T From;
    public T To;
    public Action<T> OnChange;

    public TweenConfig(T from, T to, Action<T> onChange) {
      From = from;
      To = to;
      OnChange = onChange;
    }
  }

  struct TweenState {
    public float Elapsed;
    public float Duration;

    public TweenState(float duration) {
      Elapsed = 0;
      Duration = duration;
    }
  }

  struct Loop {
    public int Remaining;
  }

  struct Eased {
    public EaseFunc Func;
  }

  struct Target {
    public object Object;
  }
}