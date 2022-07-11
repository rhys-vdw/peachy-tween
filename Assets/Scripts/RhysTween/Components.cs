using System;

namespace RhysTween {
  internal struct TweenState<T> {
    public T From;
    public T To;
    public double StartTime;
    public float NormalizedTime;
    public float Duration;

    public TweenState(T from, T to, float duration) {
      From = from;
      To = to;
      StartTime = UnityEngine.Time.timeAsDouble;
      NormalizedTime = 0;
      Duration = duration;
    }
  }

  internal struct Time {
    public double Current;
  }

  internal struct Complete { }

  internal struct OnChange<T> {
    public Action<T> Callback;
  }

  internal struct OnComplete {
    public Action Callback;
  }
}