using UnityEngine;
using static UnityEngine.Mathf;

namespace PeachyTween {
  internal static class VectorUtility {
    public static Vector3 WithX(this in Vector3 v, float x) =>
      new (x, v.y, v.z);

    public static Vector3 WithY(this in Vector3 v, float y) =>
      new (v.x, y, v.z);

    public static Vector3 WithZ(this in Vector3 v, float z) =>
      new (v.x, v.y, z);

    public static Vector2 RadiansLength(float radians, float length) =>
      new Vector2(Cos(radians), Sin(radians)) * length;

    public static Vector2 FromRadians(float radians) =>
      new (Cos(radians), Sin(radians));

    public static float ToRadians(this in Vector2 v) =>
      Atan2(v.y, v.x);

    // From: http://answers.unity.com/comments/834881/view.html
    public static Vector2 RotateRadians(this in Vector2 v, float radians) {
      var sin = Sin(radians);
      var cos = Cos(radians);

      var tx = v.x;
      var ty = v.y;

      return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }

    public static Vector2 Rotate(this in Vector2 v, float degrees) =>
      RotateRadians(v, degrees * Deg2Rad);


    public static Vector2 RandomOnUnitCircle() =>
      FromRadians(Random.Range(0f, PI * 2f));
  }
}
