using System;

namespace RhysTween {
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
    public double StartTime;
    public float Duration;

    public TweenState(double startTime, float duration) {
      StartTime = startTime;
      Duration = duration;
    }
  }

  internal struct Time {
    public double Current;
  }

  internal struct Loop {
    public int Remaining;
  }

  internal struct Complete { }

  internal struct OnComplete {
    public Action Callback;
  }
}