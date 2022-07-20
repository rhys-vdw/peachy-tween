using UnityEngine;

namespace PeachyTween {
  public static class TransformExtensions {
#region Rotation

    /// <summary>
    /// Rotate a Transform in world space.<para/>
    ///
    /// Quaternion rotations will always take the shortest path. For rotations
    /// beyond 360 degrees use the <see cref="TweenRotation(Transform, Vector3, float)">Euler angles override</see>.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="endValue">The end world rotation.</param>
    /// <param name="duration">Length of tween in seconds.</param>
     public static Tween TweenRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy
        .Tween(transform.rotation, endValue, duration, v => transform.rotation = v)
        .SetTarget(transform);

    /// <summary>
    /// Rotate a Transform in world space.<para/>
    ///
    /// This supports rotating beyond 360 degrees.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="endValue">The end world rotation Euler angles in degrees.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.eulerAngles, endValue, duration, v => transform.eulerAngles = v)
        .SetTarget(transform);

    /// <summary>
    /// Rotate a Transform in world space to face a point.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="forward">The end forward vector.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLookAt(this Transform transform, Vector3 position, float duration) =>
      TweenLookRotation(transform, position - transform.position, duration, Vector3.up);

    /// <inheritdoc cref="TweenLookAt(Transform, Vector3, float)" />
    /// <param name="up">The up direction of the end rotation.</param>
    public static Tween TweenLookAt(this Transform transform, Vector3 position, float duration, Vector3 up) =>
      TweenLookRotation(transform, position - transform.position, duration, up);

    /// <summary>
    /// Rotate a Transform in world space to face a direction.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="forward">The end forward vector.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLookRotation(this Transform transform, Vector3 forward, float duration) =>
      TweenLookRotation(transform, forward, duration, Vector3.up);

    /// <inheritdoc cref="TweenLookRotation(Transform, Vector3, float)" />
    /// <param name="up">The up direction of the end rotation.</param>
    public static Tween TweenLookRotation(this Transform transform, Vector3 forward, float duration, Vector3 up) =>
      Peachy
        .Tween(transform.rotation, Quaternion.LookRotation(forward, up), duration, v => transform.rotation = v)
        .SetTarget(transform);

#endregion
#region Local rotation

    /// <summary>
    /// Rotate a Transform in local space.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="endValue">The end local rotation.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLocalRotation(this Transform transform, Quaternion endValue, float duration) =>
      Peachy
        .Tween(transform.localRotation, endValue, duration, v => transform.localRotation = v)
        .SetTarget(transform);

    /// <summary>
    /// Rotate a Transform in local space.
    ///
    /// This supports rotating beyond 360 degrees.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="endValue">The end local rotation Euler angles in degrees.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLocalRotation(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.localEulerAngles, endValue, duration, v => transform.localEulerAngles = v)
        .SetTarget(transform);

#endregion
#region Position

    public static Tween TweenPosition(this Transform transform, Vector3 endValue, float duration) =>
      Peachy
        .Tween(transform.position, endValue, duration, v => transform.position = v)
        .SetTarget(transform);

    public static Tween TweenPosition2D(this Transform transform, Vector2 endValue, float duration) {
      var z = transform.position.z;
      return Peachy
        .Tween((Vector2) transform.position, endValue, duration, v => transform.position = v.WithZ(z))
        .SetTarget(transform);
    }

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