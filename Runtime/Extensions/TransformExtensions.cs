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
    /// <param name="to">The end world rotation.</param>
    /// <param name="duration">Length of tween in seconds.</param>
     public static Tween TweenRotation(this Transform transform, Quaternion to, float duration) =>
      Peachy
        .Tween(transform.rotation, to, duration, v => transform.rotation = v)
        .SetTarget(transform);

    /// <summary>
    /// Rotate a Transform in world space.<para/>
    ///
    /// This supports rotating beyond 360 degrees.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="to">The end world rotation Euler angles in degrees.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenRotation(this Transform transform, Vector3 to, float duration) =>
      Peachy
        .Tween(transform.eulerAngles, to, duration, v => transform.eulerAngles = v)
        .SetTarget(transform);

    /// <summary>
    /// Rotate a Transform in world space to face a point.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="position">The point to rotate towards.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLookAt(this Transform transform, Vector3 position, float duration) =>
      TweenLookAt(transform, position, duration, Vector3.up);

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

    /// <summary>
    /// Shake the Transform's world rotation with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition" path="param" />
    public static Tween ShakeRotation(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(transform.eulerAngles, Random.onUnitSphere * magnitude, duration, v => transform.eulerAngles = v)
      .Shake(oscillationCount, decay, randomness)
      .SetTarget(transform);

#endregion
#region Local rotation

    /// <summary>
    /// Rotate a Transform in local space.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="to">The end local rotation.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLocalRotation(this Transform transform, Quaternion to, float duration) =>
      Peachy
        .Tween(transform.localRotation, to, duration, v => transform.localRotation = v)
        .SetTarget(transform);

    /// <summary>
    /// Rotate a Transform in local space.
    ///
    /// This supports rotating beyond 360 degrees.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="to">The end local rotation Euler angles in degrees.</param>
    /// <param name="duration">Length of tween in seconds.</param>
    public static Tween TweenLocalRotation(this Transform transform, Vector3 to, float duration) =>
      Peachy
        .Tween(transform.localEulerAngles, to, duration, v => transform.localEulerAngles = v)
        .SetTarget(transform);

    /// <summary>
    /// Shake the Transform's local rotation with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition"/>
    public static Tween ShakeLocalRotation(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(
        transform.localEulerAngles,
        Random.onUnitSphere * magnitude,
        duration,
        v => transform.localEulerAngles = v
      )
      .Shake(oscillationCount, decay, randomness)
      .SetTarget(transform);

#endregion
#region Position

    public static Tween TweenPosition(this Transform transform, Vector3 to, float duration) =>
      Peachy
        .Tween(transform.position, to, duration, v => transform.position = v)
        .SetTarget(transform);

    public static Tween TweenPosition2D(this Transform transform, Vector2 to, float duration) =>
      Peachy.Tween(
        (Vector2) transform.position,
        to,
        duration,
        v => transform.position = v.WithZ(transform.position.z)
      ).SetTarget(transform);

    public static Tween TweenPositionX(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.position.x,
        to,
        duration,
        v => transform.position = transform.position.WithX(v)
      ).SetTarget(transform);

    public static Tween TweenPositionY(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.position.y,
        to,
        duration,
        v => transform.position = transform.position.WithY(v)
      ).SetTarget(transform);

    public static Tween TweenPositionZ(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.position.z,
        to,
        duration,
        v => transform.position = transform.position.WithZ(v)
      ).SetTarget(transform);

    /// <summary>
    /// Shake the Transform's world position with a random vector.
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="magnitude">The magnitude of a random direction vector that informs the range of the shake.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="oscillationCount">Number of oscillations per axis.</param>
    /// <param name="decay">Rate at which amplitude and frequency decrease over time.</param>
    /// <param name="randomness">Maximum percentage change randomly applied to amplitude and frequency per axis.</param>
    public static Tween ShakePosition(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(transform.position, Random.onUnitSphere * magnitude, duration, v => transform.position = v)
      .Shake(oscillationCount, decay, randomness)
      .SetTarget(transform);

    /// <summary>
    /// Shake the Transform's world position on the XY axis with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition" path="param" />
    public static Tween ShakePosition2D(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(
        (Vector2) transform.position,
        VectorUtility.RandomOnUnitCircle() * magnitude,
        duration,
        v => transform.position = v.WithZ(transform.position.z)
      )
      .Shake2D(oscillationCount, decay, randomness)
      .SetTarget(transform);


#endregion
#region Local position

    public static Tween TweenLocalPosition(this Transform transform, Vector3 to, float duration) =>
      Peachy
        .Tween(transform.localPosition, to, duration, v => transform.localPosition = v)
        .SetTarget(transform);

    public static Tween TweenLocalPositionX(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.localPosition.x,
        to,
        duration,
        v => transform.localPosition = transform.localPosition.WithX(v)
      ).SetTarget(transform);

    public static Tween TweenLocalPositionY(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.localPosition.y,
        to,
        duration,
        v => transform.localPosition = transform.localPosition.WithY(v)
      ).SetTarget(transform);

    public static Tween TweenLocalPositionZ(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.localPosition.z,
        to,
        duration,
        v => transform.localPosition = transform.localPosition.WithZ(v)
      ).SetTarget(transform);

    /// <summary>
    /// Shake the Transform's local position with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition" path="param" />
    public static Tween ShakeLocalPosition(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(transform.localPosition, Random.onUnitSphere * magnitude, duration, v => transform.localPosition = v)
      .Shake(oscillationCount, decay, randomness)
      .SetTarget(transform);

    /// <summary>
    /// Shake the Transform's local position on the XY axis with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition" path="param" />
    public static Tween ShakeLocalPosition2D(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(
        (Vector2) transform.localPosition,
        VectorUtility.RandomOnUnitCircle() * magnitude,
        duration,
        v => transform.localPosition = v.WithZ(transform.localPosition.z)
      )
      .Shake2D(oscillationCount, decay, randomness)
      .SetTarget(transform);

#endregion
#region Scale

    public static Tween TweenScale(this Transform transform, Vector3 to, float duration) =>
      Peachy
        .Tween(transform.localScale, to, duration, v => transform.localScale = v)
        .SetTarget(transform);

    public static Tween TweenScaleX(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.localScale.x,
        to,
        duration,
        v => transform.localScale = transform.localScale.WithX(v)
      ).SetTarget(transform);

    public static Tween TweenScaleY(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.localScale.y,
        to,
        duration,
        v => transform.localScale = transform.localScale.WithY(v)
      ).SetTarget(transform);

    public static Tween TweenScaleZ(this Transform transform, float to, float duration) =>
      Peachy.Tween(
        transform.localScale.z,
        to,
        duration,
        v => transform.localScale = transform.localScale.WithZ(v)
      ).SetTarget(transform);

    /// <summary>
    /// Shake the Transform's local scale with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition" path="param" />
    public static Tween ShakeLocalScale(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(transform.localScale, Random.onUnitSphere * magnitude, duration, v => transform.localScale = v)
      .Shake(oscillationCount, decay, randomness)
      .SetTarget(transform);

    /// <summary>
    /// Shake the Transform's local scale on the XY axis with a random vector.
    /// </summary>
    /// <inheritdoc cref="ShakePosition" path="param" />
    public static Tween ShakeLocalScale2D(
      this Transform transform,
      float magnitude,
      float duration,
      int oscillationCount,
      float decay,
      float randomness
    ) => Peachy
      .Tween(
        (Vector2) transform.localScale,
        VectorUtility.RandomOnUnitCircle() * magnitude,
        duration,
        v => transform.localScale = v.WithZ(transform.localScale.z)
      )
      .Shake2D(oscillationCount, decay, randomness)
      .SetTarget(transform);

#endregion
  }
}