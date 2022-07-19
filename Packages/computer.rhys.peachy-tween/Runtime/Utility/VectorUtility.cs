using UnityEngine;
using static UnityEngine.Mathf;

namespace PeachyTween {
  public static class VectorUtility {
    public static Vector3 WithX(this in Vector3 v, float x) =>
      new (x, v.y, v.z);

    public static Vector3 WithY(this in Vector3 v, float y) =>
      new (v.x, y, v.z);

    public static Vector3 WithZ(this in Vector3 v, float z) =>
      new (v.x, v.y, z);

    public static Vector2 RadiansLength(float radians, float length) =>
      new Vector2(Cos(radians), Sin(radians)) * length;

    public static float ToRadians(this in Vector2 v) =>
      Atan2(v.y, v.x);
  }
}
