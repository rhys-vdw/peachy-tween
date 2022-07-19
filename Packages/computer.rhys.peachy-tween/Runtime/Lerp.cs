using UnityEngine;
using static UnityEngine.Mathf;

namespace PeachyTween {
  public delegate T LerpFunc<T>(T from, T to, float t);

  public static class LerpFuncs {
    public static LerpFunc<Vector3> CreateShake(
      int oscillationCount,
      float amplitudeDecay = 1f,
      float frequencyDecay = 1f,
      float amplitudeRandomness = 0f,
      float frequencyRandomness = 0f
    ) {
      EaseFunc Create() {
        return EaseFuncs.CreatePunch(
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
  }
}