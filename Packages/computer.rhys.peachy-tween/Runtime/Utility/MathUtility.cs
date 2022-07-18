using UnityEngine;

namespace PeachyTween {
  static class MathUtility {
    public static Vector3 WithX(this in Vector3 v, float x) =>
      new (x, v.y, v.z);

    public static Vector3 WithY(this in Vector3 v, float y) =>
      new (v.x, y, v.z);

    public static Vector3 WithZ(this in Vector3 v, float z) =>
      new (v.x, v.y, z);

    public static Vector2 SlerpUnclamped(Vector2 a, Vector2 b, float t) {
      var ar = ToRadians(a);
      var br = ToRadians(b);
      return RadiansLength(
        LerpRadiansUnclamped(ar, br, t),
        Mathf.LerpUnclamped(a.magnitude, b.magnitude, t)
      );
    }

    static Vector2 RadiansLength(float radians, float length) =>
      new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * length;

    static float ToRadians(this in Vector2 v) =>
      Mathf.Atan2(v.y, v.x);

    // Based on Unity's Mathf.LerpAngle
    // https://github.com/Unity-Technologies/UnityCsReference/blob/c84064be69f20dcf21ebe4a7bbc176d48e2f289c/Runtime/Export/Math/Mathf.cs#L232-L238
    static float LerpRadiansUnclamped(float a, float b, float t) {
      var delta = Mathf.Repeat(b - a, Mathf.PI * 2);
      if (delta > Mathf.PI) {
        delta -= Mathf.PI * 2;
      }
      return a + delta * t;
    }
  }
}
