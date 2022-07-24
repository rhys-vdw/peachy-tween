using System;
using UnityEngine;

namespace PeachyTween {
  /// <summary>
  /// Methods for creating new tweens and operating on tweens in groups.
  /// </summary>
  public static class Peachy {
#region Tween factory

    /// <summary>
    /// Create a new <c>float</c> tween.
    /// </summary>
    /// <param name="from">The starting value.</param>
    /// <param name="to">The end value.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="onChange">A callback that will be invoked every time the tween value changes.</param>
    /// <returns>The newly created tween.</returns>
    public static Tween Tween(float from, float to, float duration, Action<float> onChange) =>
      CreateTween(from, to, duration, onChange);

    /// <summary>
    /// Create a new <c>Vector2</c> tween.
    /// </summary>
    /// <param name="from">The starting value.</param>
    /// <param name="to">The end value.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="onChange">A callback that will be invoked every time the tween value changes.</param>
    /// <returns>The newly created tween.</returns>
    public static Tween Tween(Vector2 from, Vector2 to, float duration, Action<Vector2> onChange) =>
      CreateTween(from, to, duration, onChange);

    /// <summary>
    /// Create a new <c>Vector3</c> tween.
    /// </summary>
    /// <param name="from">The starting value.</param>
    /// <param name="to">The end value.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="onChange">A callback that will be invoked every time the tween value changes.</param>
    /// <returns>The newly created tween.</returns>
    public static Tween Tween(Vector3 from, Vector3 to, float duration, Action<Vector3> onChange) =>
      CreateTween(from, to, duration, onChange);

    /// <summary>
    /// Create a new <c>Vector4</c> tween.
    /// </summary>
    /// <param name="from">The starting value.</param>
    /// <param name="to">The end value.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="onChange">A callback that will be invoked every time the tween value changes.</param>
    /// <returns>The newly created tween.</returns>
    public static Tween Tween(Vector4 from, Vector4 to, float duration, Action<Vector4> onChange) =>
      CreateTween(from, to, duration, onChange);

    /// <summary>
    /// Create a new <c>Quaternion</c> tween.
    /// </summary>
    /// <param name="from">The starting value.</param>
    /// <param name="to">The end value.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="onChange">A callback that will be invoked every time the tween value changes.</param>
    /// <returns>The newly created tween.</returns>
    public static Tween Tween(Quaternion from, Quaternion to, float duration, Action<Quaternion> onChange) =>
      CreateTween(from, to, duration, onChange);

    /// <summary>
    /// Create a new <c>Color</c> tween.
    /// </summary>
    /// <param name="from">The starting value.</param>
    /// <param name="to">The end value.</param>
    /// <param name="duration">Total tween duration in seconds.</param>
    /// <param name="onChange">A callback that will be invoked every time the tween value changes.</param>
    /// <returns>The newly created tween.</returns>
    public static Tween Tween(Color from, Color to, float duration, Action<Color> onChange) =>
      CreateTween(from, to, duration, onChange);

    static Tween CreateTween<T>(T from, T to, float duration, Action<T> onChange) =>
      new (Core.CreateTween(from, to, duration, onChange));

#endregion
#region Sequence factory

    public static Sequence Sequence() =>
      new (Core.CreateSequence());

#endregion
#region Run

    /// <summary>
    /// Update a custom group of tweens.
    /// </summary>
    /// <remarks>
    /// Tweens default to the <c>Update</c> group, but custom groups can be
    /// assigned using <see cref="Tween.SetGroup{TGroup}"><c>SetGroup</c></see>.
    /// </remarks>
    /// <param name="t">The time step to progress your group by.</param>
    /// <typeparam name="TGroup">An empty struct that is used as an identifier for your custom group.</typeparam>
    public static void Run<TGroup>(float deltaTime) where TGroup : struct =>
      Core.Run<TGroup>(deltaTime);

#endregion
#region Control

    /// <summary>
    /// Kill all <see cref="Tween">tweens</see> targeting an object.
    /// </summary>
    /// <seealso cref="Tween.SetTarget"/>
    /// <param name="target">The target object.</param>
    /// <param name="complete">
    /// Progress tween to end value and trigger <see cref="Tween.OnComplete"><c>OnComplete</c></see> handlers.
    /// </param>
    public static void KillAllWithTarget(object target, bool complete = false) =>
      Core.KillAllWithTarget(target, complete);

#endregion
  }
}