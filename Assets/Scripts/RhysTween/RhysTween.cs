using System;

namespace RhysTween {
  public static class RhysTween {
    static TweenRunner Runner => TweenRunner.Instance;

    public static Tween Tween<T>(T from, T to, float duration, Action<T> onChange = null) {
      var runner = Runner;
      var tween = runner.CreateTween(new TweenState<T>(from, to, duration));
      if (onChange != null) {
        runner.SetOnChange(tween, onChange);
      }
      return tween;
    }

    public static Tween OnComplete(this Tween tween, Action onComplete) {
      Runner.SetOnComplete(tween, onComplete);
      return tween;
    }
  }
}