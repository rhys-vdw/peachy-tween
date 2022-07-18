using UnityEngine;

namespace PeachyTween {
  public static class TransformExtensions {
#region Rotation

    public static Tween TweenRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy
        .Tween(transform.rotation, endValue, duration, v => transform.rotation = v)
        .SetTarget(transform);

    public static Tween TweenRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.rotation, Quaternion.Euler(endValue), duration, v => transform.rotation = v)
        .SetTarget(transform);

    public static Tween TweenLookAt(this Transform transform, Vector3 forward, float duration) =>
      TweenLookAt(transform, forward, duration, Vector3.up);

    public static Tween TweenLookAt(this Transform transform, Vector3 forward, float duration, Vector3 up) =>
      Peachy
        .Tween(transform.rotation, Quaternion.LookRotation(forward, up), duration, v => transform.rotation = v)
        .SetTarget(transform);

#endregion
#region Local rotation

    public static Tween TweenLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy
        .Tween(transform.localRotation, endValue, duration, v => transform.localRotation = v)
        .SetTarget(transform);

    public static Tween TweenLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.localRotation, Quaternion.Euler(endValue), duration, v => transform.localRotation = v)
        .SetTarget(transform);

#endregion
#region Position

    public static Tween TweenPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.position, endValue, duration, v => transform.position = v)
        .SetTarget(transform);

    public static Tween TweenPositionX(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.position.x,
        endValue,
        duration,
        v => transform.position = transform.position.WithX(v)
      ).SetTarget(transform);

    public static Tween TweenPositionY(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.position.y,
        endValue,
        duration,
        v => transform.position = transform.position.WithY(v)
      ).SetTarget(transform);

    public static Tween TweenPositionZ(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.position.z,
        endValue,
        duration,
        v => transform.position = transform.position.WithZ(v)
      ).SetTarget(transform);

#endregion
#region Local position

    public static Tween TweenLocalPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.localPosition, endValue, duration, v => transform.localPosition = v)
        .SetTarget(transform);

    public static Tween TweenLocalPositionX(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localPosition.x,
        endValue,
        duration,
        v => transform.localPosition = transform.localPosition.WithX(v)
      ).SetTarget(transform);

    public static Tween TweenLocalPositionY(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localPosition.y,
        endValue,
        duration,
        v => transform.localPosition = transform.localPosition.WithY(v)
      ).SetTarget(transform);

    public static Tween TweenLocalPositionZ(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localPosition.z,
        endValue,
        duration,
        v => transform.localPosition = transform.localPosition.WithZ(v)
      ).SetTarget(transform);

#endregion
#region Scale

    public static Tween TweenScale(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.localScale, endValue, duration, v => transform.localScale = v)
        .SetTarget(transform);

    public static Tween TweenScaleX(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localScale.x,
        endValue,
        duration,
        v => transform.localScale = transform.localScale.WithX(v)
      ).SetTarget(transform);

    public static Tween TweenScaleY(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localScale.y,
        endValue,
        duration,
        v => transform.localScale = transform.localScale.WithY(v)
      ).SetTarget(transform);

    public static Tween TweenScaleZ(this Transform transform, float endValue, float duration) =>
      Peachy.Tween(
        transform.localScale.z,
        endValue,
        duration,
        v => transform.localScale = transform.localScale.WithZ(v)
      ).SetTarget(transform);

#endregion
  }
}