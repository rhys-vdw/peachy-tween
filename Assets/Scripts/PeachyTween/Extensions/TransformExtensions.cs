using UnityEngine;

namespace PeachyTween {
  public static class TransformExtensions {
#region Rotation

    public static Tween TweenRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy.Tween(transform.rotation, endValue, v => transform.rotation = v, duration);

    public static Tween TweenRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.rotation, Quaternion.Euler(endValue), v => transform.rotation = v, duration);

    public static Tween TweenLookAt(this Transform transform, Vector3 forward, float duration) =>
      TweenLookAt(transform, forward, duration, Vector3.up);

    public static Tween TweenLookAt(this Transform transform, Vector3 forward, float duration, Vector3 up) =>
      Peachy.Tween(transform.rotation, Quaternion.LookRotation(forward, up), v => transform.rotation = v, duration);

#endregion
#region Local rotation

    public static Tween TweenLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy.Tween(transform.localRotation, endValue, v => transform.localRotation = v, duration);

    public static Tween TweenLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.localRotation, Quaternion.Euler(endValue), v => transform.localRotation = v, duration);

#endregion
#region Position

    public static Tween TweenPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.position, endValue, v => transform.position = v, duration);

    public static Tween TweenPositionX(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.position.x,
        endValue,
        v => transform.position = transform.position.WithX(v),
        duration
      );

    public static Tween TweenPositionY(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.position.y,
        endValue,
        v => transform.position = transform.position.WithY(v),
        duration
      );

    public static Tween TweenPositionZ(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.position.z,
        endValue,
        v => transform.position = transform.position.WithZ(v),
        duration
      );

#endregion
#region Local position

    public static Tween TweenLocalPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy.Tween(transform.localPosition, endValue, v => transform.localPosition = v, duration);

    public static Tween TweenLocalPositionX(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localPosition.x,
        endValue,
        v => transform.localPosition = transform.localPosition.WithX(v),
        duration
      );

    public static Tween TweenLocalPositionY(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localPosition.y,
        endValue,
        v => transform.localPosition = transform.localPosition.WithY(v),
        duration
      );

    public static Tween TweenLocalPositionZ(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localPosition.z,
        endValue,
        v => transform.localPosition = transform.localPosition.WithZ(v),
        duration
      );

#endregion
  }
}