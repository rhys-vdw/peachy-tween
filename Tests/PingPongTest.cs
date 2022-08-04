using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class PingPongTest : BaseTweenTest {
    const float Imprecision = 0.00001f;

    static object[] Cases = new [] {
      new object[] { 2, 0f },
      new object[] { 3, 1f },
      new object[] { 4, 0f },
      new object[] { 5, 1f },
      new object[] { 6, 0f }
    };

    [TestCaseSource(nameof(Cases))]
    public void PingPong(int loopCount, float finalValue) {
      var onComplete = 0;
      var onLoop = 0;
      var value = 0f;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => value = v)
        .OnComplete(() => onComplete++)
        .OnLoop(() => onLoop++)
        .PingPong()
        .SetLooping(loopCount);

      tween.ManualUpdate(0.3f);

      for (var i = 0; i < loopCount; i++) {
        Assert.AreEqual(i, onLoop, "OnLoop not called");
        Assert.AreEqual(0, onComplete, "OnComplete not called");
        Assert.AreEqual(
          expected: i % 2 == 0 ? 0.3f : 0.7f,
          actual: value,
          Imprecision,
          "Value is progressed"
        );
        Assert.True(tween.IsAlive(), "Tween is still running");

        tween.ManualUpdate(1f);
      }

      Assert.AreEqual(loopCount, onLoop, "OnLoop called");
      Assert.AreEqual(1, onComplete, "OnComplete called");
      Assert.AreEqual(finalValue, value, "Value returned to start");
      Assert.False(tween.IsAlive(), "Tween is not running");
    }
  }
}
