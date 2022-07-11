using System;

namespace RhysTween {
  internal struct Update { }
  internal struct FixedUpdate { }
  internal struct ManualUpdate { }

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

  internal struct Time {
    public float DeltaTime;
  }

  internal struct Loop {
    public int Remaining;
  }

  internal struct Complete { }

  internal struct OnComplete {
    public Action Callback;
  }
}