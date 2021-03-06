using UnityEngine;
using static UnityEngine.Mathf;

namespace PeachyTween {
  /// <summary>
  /// An function that controls how a tweened value is interpolated.
  /// </summary>
  /// <seealso cref="Tween.Lerp"/>
  /// <param name="from">The starting value.</param>
  /// <param name="to">The value to tween to.</param>
  /// <param name="t">Normalized time in range [0, 1].</param>
  /// <typeparam name="T">The type being interpolated.</typeparam>
  /// <returns>The interpolated value.</returns>
  public delegate T LerpFunc<T>(T from, T to, float t);

  /// <summary>
  /// Helpers for creating configurable interpolation functions.
  /// </summary>
  public static class LerpFuncFactory {
#region Shake

    /// <summary>
    /// Create a interpolation function that shakes a <c>Vector3</c> on all axes.
    /// </summary>
    /// <returns>The shake interpolation function.</returns>
    /// <inheritdoc cref="CreateShake"/>
    public static LerpFunc<Vector2> CreateShake2D(
      int oscillationCount,
      float amplitudeDecay = 1f,
      float frequencyDecay = 1f,
      float amplitudeRandomness = 0f,
      float frequencyRandomness = 0f
    ) {
      EaseFunc Create() {
        return EaseFuncFactory.CreatePunch(
          oscillationCount,
          RandomScale(amplitudeDecay, amplitudeRandomness),
          RandomScale(frequencyDecay, frequencyRandomness),
          alwaysStartPositive: false
        );
      }
      var punchX = Create();
      var punchY = Create();
      return (origin, range, t) => new Vector2(
        LerpUnclamped(origin.x, range.x, punchX(t)),
        LerpUnclamped(origin.y, range.y, punchY(t))
      );
    }

    /// <summary>
    /// Create a interpolation function that shakes a <c>Vector2</c> on both axes.
    /// </summary>
    /// <param name="oscillationCount">Number of oscillations per axis.</param>
    /// <param name="frequencyDecay">Rate at which frequency decreases over time.</param>
    /// <param name="amplitudeDecay">Rate at which amplitude decreases over time.</param>
    /// <param name="frequencyRandomness">Maximum percentage change randomly applied to frequency per axis.</param>
    /// <param name="amplitudeRandomness">Maximum percentage change randomly applied to amplitude per axis.</param>
    /// <returns>The shake interpolation function.</returns>
    public static LerpFunc<Vector3> CreateShake(
      int oscillationCount,
      float amplitudeDecay = 1f,
      float frequencyDecay = 1f,
      float amplitudeRandomness = 0f,
      float frequencyRandomness = 0f
    ) {
      EaseFunc Create() {
        return EaseFuncFactory.CreatePunch(
          oscillationCount,
          RandomScale(amplitudeDecay, amplitudeRandomness),
          RandomScale(frequencyDecay, frequencyRandomness),
          alwaysStartPositive: false
        );
      }
      var punchX = Create();
      var punchY = Create();
      var punchZ = Create();
      return (origin, range, t) => new Vector3(
        LerpUnclamped(origin.x, range.x, punchX(t)),
        LerpUnclamped(origin.y, range.y, punchY(t)),
        LerpUnclamped(origin.z, range.z, punchZ(t))
      );
    }

    static float RandomScale(float value, float maxScale) =>
      value * Random.Range(1 - maxScale, 1 + maxScale);

#endregion
  }

  internal static class LerpFuncs {
#region Float

    public static float InverseLerpUnclamped(float a, float b, float value) =>
      a == b ? 0 : (value - a) / (b - a);

#endregion
#region Angle

    // Based on Unity's Mathf.LerpAngle
    // https://github.com/Unity-Technologies/UnityCsReference/blob/c84064be69f20dcf21ebe4a7bbc176d48e2f289c/Runtime/Export/Math/Mathf.cs#L232-L238
    static float LerpRadiansUnclamped(float a, float b, float t) {
      var delta = Repeat(b - a, PI * 2);
      if (delta > PI) {
        delta -= PI * 2;
      }
      return a + delta * t;
    }

#endregion
#region Vector

    public static Vector2 SlerpUnclamped(Vector2 a, Vector2 b, float t) =>
      VectorUtility.RadiansLength(
        LerpRadiansUnclamped(a.ToRadians(), b.ToRadians(), t),
        LerpUnclamped(a.magnitude, b.magnitude, t)
      );

#endregion
  }
}