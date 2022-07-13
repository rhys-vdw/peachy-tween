using UnityEngine;

namespace PeachyTween {
  public static class TransformUtility {
    public static Tween TRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy.Tween(transform.rotation, endValue, v => transform.rotation = v, duration);

    public static Tween TRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.eulerAngles, endValue, v => transform.eulerAngles = v, duration);

    public static Tween TLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy.Tween(transform.localRotation, endValue, v => transform.localRotation = v, duration);

    public static Tween TLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.localEulerAngles, endValue, v => transform.localEulerAngles = v, duration);

    public static Tween TPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.position, endValue, v => transform.position = v, duration);

    public static Tween TLocalPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.localPosition, endValue, v => transform.localPosition = v, duration);
  }
}