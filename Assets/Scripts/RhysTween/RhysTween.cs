using System;
using UnityEngine;

namespace RhysTween {
  public static class RhysTween {
    static TweenRunner Runner => TweenRunner.Instance;

#region Tween factories

    public static Tween TRotation(this Transform transform, Quaternion endValue, float duration) =>
      Tween(transform.rotation, v => transform.rotation = v, endValue, duration);

    public static Tween TRotation(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.eulerAngles, v => transform.eulerAngles = v, endValue, duration);

    public static Tween TLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Tween(transform.localRotation, v => transform.rotation = v, endValue, duration);

    public static Tween TLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.localEulerAngles, v => transform.localEulerAngles = v, endValue, duration);

    public static Tween TPosition(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.position, v => transform.position = v, endValue, duration);

    public static Tween TLocalPosition(this Transform transform, Vector3 endValue, float duration) =>
      Tween(transform.localPosition, v => transform.localPosition = v, endValue, duration);

    public static Tween Tween<T>(T from, Action<T> onChange, T to, float duration) {
      var runner = Runner;
      var tween = runner.CreateTween(from, onChange, to, duration);
      return tween;
    }

#endregion
#region Update

    public static Tween SetUpdate(this Tween tween) {
      Runner.SetUpdate<Update>(tween);
      return tween;
    }

    public static Tween SetFixedUpdate(this Tween tween) {
      Runner.SetUpdate<FixedUpdate>(tween);
      return tween;
    }

    public static Tween SetManualUpdate(this Tween tween) {
      Runner.SetUpdate<ManualUpdate>(tween);
      return tween;
    }

#endregion
#region Loop

    public static Tween Loop(this Tween tween, int count) {
      if (count < 0) {
        throw new ArgumentOutOfRangeException(nameof(count), count, "Must not be negative");
      }
      if (count > 0) {
        Runner.SetLooping(tween, count);
      }
      return tween;
    }

    public static Tween LoopForever(this Tween tween) {
      Runner.SetLooping(tween, -1);
      return tween;
    }

#endregion
#region Callbacks

    public static Tween OnComplete(this Tween tween, Action onComplete) {
      Runner.SetOnComplete(tween, onComplete);
      return tween;
    }

#endregion
  }
}