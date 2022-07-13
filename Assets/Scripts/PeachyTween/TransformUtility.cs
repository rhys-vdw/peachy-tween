using UnityEngine;

namespace PeachyTween {
  public static class TransformUtility {
    public static Tween TRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy.Tween(transform.rotation, v => transform.rotation = v, endValue, duration);

    public static Tween TRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.eulerAngles, v => transform.eulerAngles = v, endValue, duration);

    public static Tween TLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy.Tween(transform.localRotation, v => transform.localRotation = v, endValue, duration);

    public static Tween TLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.localEulerAngles, v => transform.localEulerAngles = v, endValue, duration);

    public static Tween TPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.position, v => transform.position = v, endValue, duration);

    public static Tween TLocalPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.localPosition, v => transform.localPosition = v, endValue, duration);
  }
}