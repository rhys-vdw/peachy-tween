using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class PingPongTest : BaseTweenTest {
    const float Imprecision = 0.00001f;

    [Test]
    public void SimplePingPong() {
      var onComplete = 0;
      var onLoop = 0;
      var value = 0f;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => value = v)
        .OnComplete(() => onComplete++)
        .OnLoop(() => onLoop++)
        .PingPong()
        .Loop(2);

      tween.ManualUpdate(0.3f);

      Assert.AreEqual(0, onLoop, "OnLoop not called");
      Assert.AreEqual(0, onComplete, "OnComplete not called");
      Assert.AreEqual(0.3f, value, Imprecision, "Value is progressed");
      Assert.True(tween.IsAlive(), "Tween is still running");

      tween.ManualUpdate(1f);

      Assert.AreEqual(1, onLoop, "OnLoop called");
      Assert.AreEqual(0, onComplete, "OnComplete not called");
      Assert.AreEqual(0.7f, value, Imprecision, "Value is ping-ponged");
      Assert.True(tween.IsAlive(), "Tween is still running");

      tween.ManualUpdate(1f);

      Assert.AreEqual(2, onLoop, "OnLoop called");
      Assert.AreEqual(1, onComplete, "OnComplete called");
      Assert.AreEqual(0f, value, "Value returned to start");
      Assert.False(tween.IsAlive(), "Tween is not running");
    }
  }
}
