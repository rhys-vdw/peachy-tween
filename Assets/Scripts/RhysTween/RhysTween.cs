using System;
using UnityEngine;

namespace RhysTween {
  public static class RhysTween {
    static TweenRunner Runner => TweenRunner.Instance;

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

    public static Tween Tween<T>(T from, Action<T> setter, T endValue, float duration) =>
      Tween(from, endValue, duration, setter);

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