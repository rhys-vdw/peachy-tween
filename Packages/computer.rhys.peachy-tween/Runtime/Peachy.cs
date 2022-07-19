using System;
using UnityEngine;

namespace PeachyTween {
  public static class Peachy {
#region Tween factory

    public static Tween Tween(float from, float to, float duration, Action<float> onChange) =>
      Core.CreateTween(from, to, duration, onChange);

    public static Tween Tween(Vector2 from, Vector2 to, float duration, Action<Vector2> onChange) =>
      Core.CreateTween(from, to, duration, onChange);

    public static Tween Tween(Vector3 from, Vector3 to, float duration, Action<Vector3> onChange) =>
      Core.CreateTween(from, to, duration, onChange);

    public static Tween Tween(Vector4 from, Vector4 to, float duration, Action<Vector4> onChange) =>
      Core.CreateTween(from, to, duration, onChange);

    public static Tween Tween(Quaternion from, Quaternion to, float duration, Action<Quaternion> onChange) =>
      Core.CreateTween(from, to, duration, onChange);

    public static Tween Tween(Color from, Color to, float duration, Action<Color> onChange) =>
      Core.CreateTween(from, to, duration, onChange);

#endregion
#region Run

    public static void Run<TGroup>(float deltaTime) where TGroup : struct =>
      Core.Run<TGroup>(deltaTime);

#endregion
#region Control

    /// <summary>
    /// Kill all <c cref="Tween">Tween</c>s targeting an object.
    /// </summary>
    /// <seealso cref="SetTarget"/>
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