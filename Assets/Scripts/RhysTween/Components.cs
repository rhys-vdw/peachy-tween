using System;

namespace RhysTween {
  internal struct TweenState<T> {
    public T From;
    public T To;
    public Action<T> OnChange;
    public float Duration;
    public double StartTime;
    public float NormalizedTime;

    public TweenState(T from, T to, Action<T> onChange, float duration) {
      From = from;
      To = to;
      StartTime = UnityEngine.Time.timeAsDouble;
      NormalizedTime = 0;
      Duration = duration;
      OnChange = onChange;
    }
  }

  internal struct Time {
    public double Current;
  }

  internal struct Complete { }

  internal struct OnComplete {
    public Action Callback;
  }
}