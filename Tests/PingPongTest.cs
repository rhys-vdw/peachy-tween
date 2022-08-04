using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class PingPongTest : BaseTweenTest {
    const float Imprecision = 0.00001f;

    static object[] Cases = new [] {
      new object[] { 2, 0f, 1.4f },
      new object[] { 3, 1f, 0.2f },
      new object[] { 4, 0f, 0.1f },
      new object[] { 5, 1f, 2f },
      new object[] { 6, 0f, 69f }
    };

    [TestCaseSource(nameof(Cases))]
    public void PingPong(int loopCount, float finalValue, float duration) {
      var onComplete = 0;
      var onLoop = 0;
      var value = 0f;

      var tween = Peachy
        .Tween(0f, 1f, duration, v => value = v)
        .OnComplete(() => onComplete++)
        .OnLoop(() => onLoop++)
        .PingPong()
        .SetLooping(loopCount);

      tween.ManualUpdate(duration * 0.3f);

      for (var i = 0; i < loopCount; i++) {
        Assert.AreEqual(i, onLoop, $"loop {i}: OnLoop not called");
        Assert.AreEqual(0, onComplete, $"loop {i}: OnComplete not called");
        Assert.AreEqual(
          expected: i % 2 == 0 ? 0.3f : 0.7f,
          actual: value,
          Imprecision,
          $"loop {i} Value is progressed"
        );
        Assert.True(tween.IsAlive(), $"loop {i}: Tween is still running");

        tween.ManualUpdate(duration);
      }

      Assert.AreEqual(loopCount, onLoop, "OnLoop called");
      Assert.AreEqual(1, onComplete, "OnComplete called");
      Assert.AreEqual(finalValue, value, "Value returned to start");
      Assert.False(tween.IsAlive(), "Tween is not running");
    }
  }
}
