// Adapted from https://github.com/ai/easings.net/blob/b303c19e52a01f8f3c1c799a1eae3bfec54e2a1c/src/easings/easingsFunctions.ts

using System;
using static UnityEngine.Mathf;

namespace PeachyTween {
  /// <summary>
  /// An easing function that controls the timing of a tween.
  /// </summary>
  /// <seealso cref="Tween.Ease"/>
  /// <param name="t">Normalized time in range [0, 1].</param>
  /// <returns>The interpolated value.</returns>
  public delegate float EaseFunc(float t);

  /// <summary>
  /// Standard easing functions.
  /// </summary>
  public enum Ease {
    /// <summary>
    /// No ease.
    /// </summary>
    /// <remarks>
    /// This is the same as <c cref="Linear">Linear</c>. It exists as a default
    /// to mirror DOTween's ease enum for easier migration.
    /// </remarks>
    Unset,
    /// <summary>
    /// No ease.
    /// </summary>
    Linear,
    /// <summary>
    /// Easing function with a sine ease in.
    /// </summary>
    SineIn,
    /// <summary>
    /// Easing function with a sine ease out.
    /// </summary>
    SineOut,
    /// <summary>
    /// Easing function with a sine ease in and out.
    /// </summary>
    SineInOut,
    /// <summary>
    /// Easing function with a quadratic ease in.
    /// </summary>
    QuadIn,
    /// <summary>
    /// Easing function with a quadratic ease out.
    /// </summary>
    QuadOut,
    /// <summary>
    /// Easing function with a quadratic ease in and out.
    /// </summary>
    QuadInOut,
    /// <summary>
    /// Easing function with a cubic ease in.
    /// </summary>
    CubicIn,
    /// <summary>
    /// Easing function with a cubic ease out.
    /// </summary>
    CubicOut,
    /// <summary>
    /// Easing function with a cubic ease in and out.
    /// </summary>
    CubicInOut,
    /// <summary>
    /// Easing function with a quartic ease in.
    /// </summary>
    QuartIn,
    /// <summary>
    /// Easing function with a quartic ease out.
    /// </summary>
    QuartOut,
    /// <summary>
    /// Easing function with a quartic ease in and out.
    /// </summary>
    QuartInOut,
    /// <summary>
    /// Easing function with a quintic ease in.
    /// </summary>
    QuintIn,
    /// <summary>
    /// Easing function with a quintic ease out.
    /// </summary>
    QuintOut,
    /// <summary>
    /// Easing function with a quintic ease in and out.
    /// </summary>
    QuintInOut,
    /// <summary>
    /// Easing function with a exponential ease in.
    /// </summary>
    ExpoIn,
    /// <summary>
    /// Easing function with a exponential ease out.
    /// </summary>
    ExpoOut,
    /// <summary>
    /// Easing function with a exponential ease in and out.
    /// </summary>
    ExpoInOut,
    /// <summary>
    /// Easing function with a circular ease in.
    /// </summary>
    CircIn,
    /// <summary>
    /// Easing function with a circular out.
    /// </summary>
    CircOut,
    /// <summary>
    /// Easing function with a circular ease in and out.
    /// </summary>
    CircInOut,
    /// <summary>
    /// Easing function with an elastic ease in.
    /// </summary>
    ElasticIn,
    /// <summary>
    /// Easing function with an elastic ease out.
    /// </summary>
    ElasticOut,
    /// <summary>
    /// Easing function with an elastic ease in and out.
    /// </summary>
    ElasticInOut,
    /// <summary>
    /// Easing function that backs off before continuing.
    /// </summary>
    BackIn,
    /// <summary>
    /// Easing function that overshoots the end.
    /// </summary>
    BackOut,
    /// <summary>
    /// Easing function that backs off before continuing and overshoots the end.
    /// </summary>
    BackInOut,
    /// <summary>
    /// Easing function with a bounce in and out.
    /// </summary>
    BounceIn,
    /// <summary>
    /// Easing function with a bounce in and out.
    /// </summary>
    BounceOut,
    /// <summary>
    /// Easing function with a bounce in and out.
    /// </summary>
    BounceInOut
  }

  /// <summary>
  /// A collection of standard easing functions.
  /// </summary>
  /// <remarks>
  /// These functions were all taken from <see
  /// href="https://easings.net">easings.net</see>, you can use their animations
  /// for reference.
  /// </remarks>
  public static class EaseFuncs {
    const float c1 = 1.70158f;
    const float c2 = c1 * 1.525f;
    const float c3 = c1 + 1;
    const float c4 = 2 * PI / 3;
    const float c5 = 2 * PI / 4.5f;

    /// <summary>
    /// Linear easing function.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float Linear(float t) =>
      t;

    /// <summary>
    /// Easing function with a quadratic ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuadIn(float t) =>
      t * t;

    /// <summary>
    /// Easing function with a quadratic ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuadOut(float t) =>
      1 - (1 - t) * (1 - t);

    /// <summary>
    /// Easing function with a quadratic ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuadInOut(float t) =>
      t < 0.5f ? 2 * t * t : 1 - Pow(-2 * t + 2, 2) / 2;

    /// <summary>
    /// Easing function with a cubic ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float CubicIn(float t) =>
      t * t * t;

    /// <summary>
    /// Easing function with a cubic ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float CubicOut(float t) =>
      1 - Pow(1 - t, 3);

    /// <summary>
    /// Easing function with a cubic ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float CubicInOut(float t) =>
      t < 0.5f ? 4 * t * t * t : 1 - Pow(-2 * t + 2, 3) / 2;

    /// <summary>
    /// Easing function with a quartic ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuartIn(float t) =>
      t * t * t * t;

    /// <summary>
    /// Easing function with a quartic ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuartOut(float t) =>
      1 - Pow(1 - t, 4);

    /// <summary>
    /// Easing function with a quartic ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuartInOut(float t) =>
      t < 0.5f ? 8 * t * t * t * t : 1 - Pow(-2 * t + 2, 4) / 2;

    /// <summary>
    /// Easing function with a quintic ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuintIn(float t) =>
      t * t * t * t * t;

    /// <summary>
    /// Easing function with a quintic ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuintOut(float t) =>
      1 - Pow(1 - t, 5);

    /// <summary>
    /// Easing function with a quintic ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float QuintInOut(float t) =>
      t < 0.5f ? 16 * t * t * t * t * t : 1 - Pow(-2 * t + 2, 5) / 2;

    /// <summary>
    /// Easing function with a sine ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float SineIn(float t) =>
      1 - Cos(t * PI / 2);

    /// <summary>
    /// Easing function with a sine ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float SineOut(float t) =>
      Sin(t * PI / 2);

    /// <summary>
    /// Easing function with a sine ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float SineInOut(float t) =>
      -(Cos(PI * t) - 1) / 2;

    /// <summary>
    /// Easing function with a exponential ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float ExpoIn(float t) =>
      t == 0 ? 0 : Pow(2, 10 * t - 10);

    /// <summary>
    /// Easing function with a exponential ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float ExpoOut(float t) =>
      t == 1 ? 1 : 1 - Pow(2, -10 * t);

    /// <summary>
    /// Easing function with a exponential ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float ExpoInOut(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : t < 0.5f
        ? Pow(2, 20 * t - 10) / 2
        : (2 - Pow(2, -20 * t + 10)) / 2;

    /// <summary>
    /// Easing function with a circular ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float CircIn(float t) =>
      1 - Sqrt(1 - Pow(t, 2));

    /// <summary>
    /// Easing function with a circular out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float CircOut(float t) =>
      Sqrt(1 - Pow(t - 1, 2));

    /// <summary>
    /// Easing function with a circular ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float CircInOut(float t) =>
      t < 0.5f
        ? (1 - Sqrt(1 - Pow(2 * t, 2))) / 2
        : (Sqrt(1 - Pow(-2 * t + 2, 2)) + 1) / 2;

    /// <summary>
    /// Easing function that backs off before continuing.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float BackIn(float t) =>
      c3 * t * t * t - c1 * t * t;

    /// <summary>
    /// Easing function that overshoots the end.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float BackOut(float t) =>
      1 + c3 * Pow(t - 1, 3) + c1 * Pow(t - 1, 2);

    /// <summary>
    /// Easing function that backs off before continuing and overshoots the end.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float BackInOut(float t) =>
      t < 0.5f
        ? Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2) / 2
        : (Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;

    /// <summary>
    /// Easing function with an elastic ease in.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float ElasticIn(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : -Pow(2, 10 * t - 10) * Sin((t * 10 - 10.75f) * c4);

    /// <summary>
    /// Easing function with an elastic ease out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float ElasticOut(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : Pow(2, -10 * t) * Sin((t * 10 - 0.75f) * c4) + 1;

    /// <summary>
    /// Easing function with an elastic ease in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float ElasticInOut(float t) =>
      t == 0
        ? 0
        : t == 1
        ? 1
        : t < 0.5f
        ? -(Pow(2, 20 * t - 10) * Sin((20 * t - 11.125f) * c5)) / 2
        : Pow(2, -20 * t + 10) * Sin((20 * t - 11.125f) * c5) / 2 + 1;

    /// <summary>
    /// Easing function with a bounce in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float BounceIn(float t) =>
      1 - BounceOut(1 - t);

    /// <summary>
    /// Easing function with a bounce in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
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

    /// <summary>
    /// Easing function with a bounce in and out.
    /// </summary>
    /// <inheritdoc cref="EaseFunc"/>
    public static float BounceInOut(float t) {
      return t < 0.5f
        ? (1 - BounceOut(1 - 2 * t)) / 2
        : (1 + BounceOut(2 * t - 1)) / 2;
    }

    /// <summary>
    /// Get an easing function.
    /// </summary>
    /// <param name="ease">The desired easing function.</param>
    /// <returns>The easing function.</returns>
    public static EaseFunc ToFunc(this Ease ease) => ease switch {
      Ease.Unset => Linear,
      Ease.Linear => Linear,
      Ease.QuadIn => QuadIn,
      Ease.QuadOut => QuadOut,
      Ease.QuadInOut => QuadInOut,
      Ease.CubicIn => CubicIn,
      Ease.CubicOut => CubicOut,
      Ease.CubicInOut => CubicInOut,
      Ease.QuartIn => QuartIn,
      Ease.QuartOut => QuartOut,
      Ease.QuartInOut => QuartInOut,
      Ease.QuintIn => QuintIn,
      Ease.QuintOut => QuintOut,
      Ease.QuintInOut => QuintInOut,
      Ease.SineIn => SineIn,
      Ease.SineOut => SineOut,
      Ease.SineInOut => SineInOut,
      Ease.ExpoIn => ExpoIn,
      Ease.ExpoOut => ExpoOut,
      Ease.ExpoInOut => ExpoInOut,
      Ease.CircIn => CircIn,
      Ease.CircOut => CircOut,
      Ease.CircInOut => CircInOut,
      Ease.BackIn => BackIn,
      Ease.BackOut => BackOut,
      Ease.BackInOut => BackInOut,
      Ease.ElasticIn => ElasticIn,
      Ease.ElasticOut => ElasticOut,
      Ease.ElasticInOut => ElasticInOut,
      Ease.BounceIn => BounceIn,
      Ease.BounceOut => BounceOut,
      Ease.BounceInOut => BounceInOut,
      _ => throw new ArgumentException($"Unrecognized {nameof(Ease)}: {ease}"),
    };
  }

  /// <summary>
  /// Helpers for creating configurable easing functions.
  /// </summary>
  public static class EaseFuncFactory {
    /// <summary>
    /// Create an easing function that oscillates and fade out.
    /// </summary>
    /// <param name="oscillationCount">
    /// The number of times the value will oscillate (half the period).<para/>
    ///
    /// A negative value will move the value away from the target on its first
    /// oscillation.
    /// </param>
    /// <param name="amplitudeDecay">
    /// Rate at which the amplitude of the wave decreases.<para/>
    ///
    /// - Higher values cause a more vigorous initial shake.<br/>
    /// - A value of zero will cause amplitude to stay constant.<br/>
    /// - Values below zero cause the amplitude to increase over time, tending towards infinity.<br/>
    /// </param>
    /// <param name="frequencyDecay">
    /// Rate at which the frequency of the wave decreases.<para/>
    ///
    /// Higher values cause a more vigorous initial shake. Values below zero
    /// cause the shake to increase in speed over time.
    /// </param>
    /// <param name="alwaysStartPositive">
    /// If the curve would start with a negative gradient, invert it.
    /// </param>
    /// <returns>The easing function.</returns>
    public static EaseFunc CreatePunch(
      int oscillationCount,
      float amplitudeDecay,
      float frequencyDecay,
      bool alwaysStartPositive = true
    ) {
      // Cache constant.
      var b = PI * oscillationCount;

      // If the curve would be going in the negative direction, flip it.
      if (alwaysStartPositive && oscillationCount % 2 == 0) {
        b = -b;
      }

      return t => Pow(1 - t, amplitudeDecay) * Sin(Pow(1 - t, frequencyDecay) * b);
    }
  }
}