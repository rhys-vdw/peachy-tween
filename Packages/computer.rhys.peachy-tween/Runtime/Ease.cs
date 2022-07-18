// Adapted from https://github.com/ai/easings.net/blob/b303c19e52a01f8f3c1c799a1eae3bfec54e2a1c/src/easings/easingsFunctions.ts

using System;
using static UnityEngine.Mathf;

namespace PeachyTween {
  public delegate float EaseFunc(float t);

  public enum Ease {
    Linear,
    QuadIn,
    QuadOut,
    QuadInOut,
    CubicIn,
    CubicOut,
    CubicInOut,
    QuartIn,
    QuartOut,
    QuartInOut,
    QuintIn,
    QuintOut,
    QuintInOut,
    SineIn,
    SineOut,
    SineInOut,
    ExpoIn,
    ExpoOut,
    ExpoInOut,
    CircIn,
    CircOut,
    CircInOut,
    BackIn,
    BackOut,
    BackInOut,
    ElasticIn,
    ElasticOut,
    ElasticInOut,
    BounceIn,
    BounceOut,
    BounceInOut
  }

  public static class EaseUtility {
    public static EaseFunc ToFunc(this Ease ease) => ease switch {
      Ease.Linear => EaseFuncs.Linear,
      Ease.QuadIn => EaseFuncs.QuadIn,
      Ease.QuadOut => EaseFuncs.QuadOut,
      Ease.QuadInOut => EaseFuncs.QuadInOut,
      Ease.CubicIn => EaseFuncs.CubicIn,
      Ease.CubicOut => EaseFuncs.CubicOut,
      Ease.CubicInOut => EaseFuncs.CubicInOut,
      Ease.QuartIn => EaseFuncs.QuartIn,
      Ease.QuartOut => EaseFuncs.QuartOut,
      Ease.QuartInOut => EaseFuncs.QuartInOut,
      Ease.QuintIn => EaseFuncs.QuintIn,
      Ease.QuintOut => EaseFuncs.QuintOut,
      Ease.QuintInOut => EaseFuncs.QuintInOut,
      Ease.SineIn => EaseFuncs.SineIn,
      Ease.SineOut => EaseFuncs.SineOut,
      Ease.SineInOut => EaseFuncs.SineInOut,
      Ease.ExpoIn => EaseFuncs.ExpoIn,
      Ease.ExpoOut => EaseFuncs.ExpoOut,
      Ease.ExpoInOut => EaseFuncs.ExpoInOut,
      Ease.CircIn => EaseFuncs.CircIn,
      Ease.CircOut => EaseFuncs.CircOut,
      Ease.CircInOut => EaseFuncs.CircInOut,
      Ease.BackIn => EaseFuncs.BackIn,
      Ease.BackOut => EaseFuncs.BackOut,
      Ease.BackInOut => EaseFuncs.BackInOut,
      Ease.ElasticIn => EaseFuncs.ElasticIn,
      Ease.ElasticOut => EaseFuncs.ElasticOut,
      Ease.ElasticInOut => EaseFuncs.ElasticInOut,
      Ease.BounceIn => EaseFuncs.BounceIn,
      Ease.BounceOut => EaseFuncs.BounceOut,
      Ease.BounceInOut => EaseFuncs.BounceInOut,
      _ => throw new ArgumentException($"Unrecognized {nameof(Ease)}: {ease}"),
    };
  }

  public static class EaseFuncs {
    const float c1 = 1.70158f;
    const float c2 = c1 * 1.525f;
    const float c3 = c1 + 1;
    const float c4 = 2 * PI / 3;
    const float c5 = 2 * PI / 4.5f;

    public static float Linear(float t) =>
      t;

    public static float QuadIn(float t) =>
      t * t;

    public static float QuadOut(float t) =>
      1 - (1 - t) * (1 - t);

    public static float QuadInOut(float t) =>
      t < 0.5f ? 2 * t * t : 1 - Pow(-2 * t + 2, 2) / 2;

    public static float CubicIn(float t) =>
      t * t * t;

    public static float CubicOut(float t) =>
      1 - Pow(1 - t, 3);

    public static float CubicInOut(float t) =>
      t < 0.5f ? 4 * t * t * t : 1 - Pow(-2 * t + 2, 3) / 2;

    public static float QuartIn(float t) =>
      t * t * t * t;

    public static float QuartOut(float t) =>
      1 - Pow(1 - t, 4);

    public static float QuartInOut(float t) =>
      t < 0.5f ? 8 * t * t * t * t : 1 - Pow(-2 * t + 2, 4) / 2;

    public static float QuintIn(float t) =>
      t * t * t * t * t;

    public static float QuintOut(float t) =>
      1 - Pow(1 - t, 5);

    public static float QuintInOut(float t) =>
      t < 0.5f ? 16 * t * t * t * t * t : 1 - Pow(-2 * t + 2, 5) / 2;

    public static float SineIn(float t) =>
      1 - Cos(t * PI / 2);

    public static float SineOut(float t) =>
      Sin(t * PI / 2);

    public static float SineInOut(float t) =>
      -(Cos(PI * t) - 1) / 2;

    public static float ExpoIn(float t) =>
      t == 0 ? 0 : Pow(2, 10 * t - 10);

    public static float ExpoOut(float t) =>
      t == 1 ? 1 : 1 - Pow(2, -10 * t);

    public static float ExpoInOut(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : t < 0.5f
        ? Pow(2, 20 * t - 10) / 2
        : (2 - Pow(2, -20 * t + 10)) / 2;

    public static float CircIn(float t) =>
      1 - Sqrt(1 - Pow(t, 2));

    public static float CircOut(float t) =>
      Sqrt(1 - Pow(t - 1, 2));

    public static float CircInOut(float t) =>
      t < 0.5f
        ? (1 - Sqrt(1 - Pow(2 * t, 2))) / 2
        : (Sqrt(1 - Pow(-2 * t + 2, 2)) + 1) / 2;

    public static float BackIn(float t) =>
      c3 * t * t * t - c1 * t * t;

    public static float BackOut(float t) =>
      1 + c3 * Pow(t - 1, 3) + c1 * Pow(t - 1, 2);

    public static float BackInOut(float t) =>
      t < 0.5f
        ? Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2) / 2
        : (Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;

    public static float ElasticIn(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : -Pow(2, 10 * t - 10) * Sin((t * 10 - 10.75f) * c4);

    public static float ElasticOut(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : Pow(2, -10 * t) * Sin((t * 10 - 0.75f) * c4) + 1;

    public static float ElasticInOut(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : t < 0.5f
        ? -(Pow(2, 20 * t - 10) * Sin((20 * t - 11.125f) * c5)) / 2
        : Pow(2, -20 * t + 10) * Sin((20 * t - 11.125f) * c5) / 2 + 1;

    public static float BounceIn(float t) =>
      1 - BounceOut(1 - t);

    public static float BounceOut(float t) {
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

    public static float BounceInOut(float t) {
      return t < 0.5f
        ? (1 - BounceOut(1 - 2 * t)) / 2
        : (1 + BounceOut(2 * t - 1)) / 2;
    }

    public static EaseFunc Punch(int peakCount, EaseFunc scaleEase) {
      var r = Log(peakCount, 2) + 1;
      return t => (1 - scaleEase(t)) * -Sin(2 * PI * Pow(2, r - 1 - t * r));
    }
  }
}