using System;
using UnityEngine;

namespace PeachyTween {
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
#region Run

    public static void Run<TGroup>(float deltaTime) where TGroup : struct =>
      Core.Run<TGroup>(deltaTime);

#endregion
#region Control

    /// <summary>
    /// Kill all <c cref="Tween">Tween</c>s targeting an object.
    /// </summary>
    /// <seealso cref="Tween.SetTarget"/>
    /// <param name="tween">The object.</param>
    /// <param name="target">The target object.</param>
    /// <param name="complete">
    /// Progress tween to end value and trigger <c cref="Tween.OnComplete">OnComplete</c> handlers.
    /// </param>
    public static void KillAllWithTarget(object target, bool complete = false) =>
      Core.KillAllWithTarget(target, complete);

#endregion
  }
}