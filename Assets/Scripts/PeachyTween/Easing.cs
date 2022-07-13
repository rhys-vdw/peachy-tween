// Adapted from https://github.com/ai/easings.net/blob/b303c19e52a01f8f3c1c799a1eae3bfec54e2a1c/src/easings/easingsFunctions.ts

using static UnityEngine.Mathf;

namespace PeachyTween {
  public delegate float EaseFunc(float t);

  public static class Easing {
    const float c1 = 1.70158f;
    const float c2 = c1 * 1.525f;
    const float c3 = c1 + 1;
    const float c4 = 2 * PI / 3;
    const float c5 = 2 * PI / 4.5f;

    public static float Linear(float t) =>
      t;

    public static float EaseInQuad(float t) =>
      t * t;

    public static float EaseOutQuad(float t) =>
      1 - (1 - t) * (1 - t);

    public static float EaseInOutQuad(float t) =>
      t < 0.5f ? 2 * t * t : 1 - Pow(-2 * t + 2, 2) / 2;

    public static float EaseInCubic(float t) =>
      t * t * t;

    public static float EaseOutCubic(float t) =>
      1 - Pow(1 - t, 3);

    public static float EaseInOutCubic(float t) =>
      t < 0.5f ? 4 * t * t * t : 1 - Pow(-2 * t + 2, 3) / 2;

    public static float EaseInQuart(float t) =>
      t * t * t * t;

    public static float EaseOutQuart(float t) =>
      1 - Pow(1 - t, 4);

    public static float EaseInOutQuart(float t) =>
      t < 0.5f ? 8 * t * t * t * t : 1 - Pow(-2 * t + 2, 4) / 2;

    public static float EaseInQuint(float t) =>
      t * t * t * t * t;

    public static float EaseOutQuint(float t) =>
      1 - Pow(1 - t, 5);

    public static float EaseInOutQuint(float t) =>
      t < 0.5f ? 16 * t * t * t * t * t : 1 - Pow(-2 * t + 2, 5) / 2;

    public static float EaseInSine(float t) =>
      1 - Cos(t * PI / 2);

    public static float EaseOutSine(float t) =>
      Sin(t * PI / 2);

    public static float EaseInOutSine(float t) =>
      -(Cos(PI * t) - 1) / 2;

    public static float EaseInExpo(float t) =>
      t == 0 ? 0 : Pow(2, 10 * t - 10);

    public static float EaseOutExpo(float t) =>
      t == 1 ? 1 : 1 - Pow(2, -10 * t);

    public static float EaseInOutExpo(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : t < 0.5f
        ? Pow(2, 20 * t - 10) / 2
        : (2 - Pow(2, -20 * t + 10)) / 2;

    public static float EaseInCirc(float t) =>
      1 - Sqrt(1 - Pow(t, 2));

    public static float EaseOutCirc(float t) =>
      Sqrt(1 - Pow(t - 1, 2));

    public static float EaseInOutCirc(float t) =>
      t < 0.5f
        ? (1 - Sqrt(1 - Pow(2 * t, 2))) / 2
        : (Sqrt(1 - Pow(-2 * t + 2, 2)) + 1) / 2;

    public static float EaseInBack(float t) =>
      c3 * t * t * t - c1 * t * t;

    public static float EaseOutBack(float t) =>
      1 + c3 * Pow(t - 1, 3) + c1 * Pow(t - 1, 2);

    public static float EaseInOutBack(float t) =>
      t < 0.5f
        ? Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2) / 2
        : (Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;

    public static float EaseInElastic(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : -Pow(2, 10 * t - 10) * Sin((t * 10 - 10.75f) * c4);

    public static float EaseOutElastic(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : Pow(2, -10 * t) * Sin((t * 10 - 0.75f) * c4) + 1;

    public static float EaseInOutElastic(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : t < 0.5f
        ? -(Pow(2, 20 * t - 10) * Sin((20 * t - 11.125f) * c5)) / 2
        : Pow(2, -20 * t + 10) * Sin((20 * t - 11.125f) * c5) / 2 + 1;

    public static float EaseInBounce(float t) =>
      1 - EaseOutBounce(1 - t);

    public static float EaseOutBounce(float t) {
      const float n1 = 7.5625f;
      const float d1 = 2.75f;

      if (t < 1 / d1) {
        return n1 * t * t;
      } else if (t < 2 / d1) {
        return n1 * (t -= 1.5f / d1) * t + 0.75f;
      } else if (t < 2.5f / d1) {
        return n1 * (t -= 2.25f / d1) * t + 0.9375f;
      } else {
        return n1 * (t -= 2.625f / d1) * t + 0.984375f;
      }
    }

    public static float EaseInOutBounce(float t) {
      return t < 0.5f
        ? (1 - EaseOutBounce(1 - 2 * t)) / 2
        : (1 + EaseOutBounce(2 * t - 1)) / 2;
    }
  }
}