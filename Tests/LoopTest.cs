using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class LoopTest : BaseTweenTest {
    [Test]
    public void LoopThreeTimes() {
      var onComplete = 0;
      var onLoop = 0;
      var value = 0f;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => value = v)
        .OnComplete(() => onComplete++)
        .OnLoop(() => onLoop++)
        .Loop(3);

      // Prime first loop.
      tween.ManualUpdate(0.5f);
      Assert.AreEqual(0, onLoop, "OnLoop not called");

      tween.ManualUpdate(1f);

      Assert.AreEqual(1, onLoop, "OnLoop called once");
      Assert.AreEqual(0, onComplete, "OnComplete not called");
      Assert.AreEqual(0.5f, value, Mathf.Epsilon, "Next loop is progressed");
      Assert.True(tween.IsAlive(), "Tween is still running");

      tween.ManualUpdate(1f);

      Assert.AreEqual(2, onLoop, "OnLoop called twice");
      Assert.AreEqual(0, onComplete, "OnComplete not called");
      Assert.AreEqual(0.5f, value, Mathf.Epsilon, "Next loop is progressed");
      Assert.True(tween.IsAlive(), "Tween is still running");

      tween.ManualUpdate(1f);

      Assert.AreEqual(3, onLoop, "OnLoop called three times");
      Assert.AreEqual(1, onComplete, "OnComplete was called");
      Assert.AreEqual(1f, value, Mathf.Epsilon, "Final loop completed");
      Assert.False(tween.IsAlive(), "Tween is no longer running");
    }

    [Test]
    public void LoopForever() {
      var value = 0f;
      var tween = Peachy
        .Tween(0f, 1f, 1f, v => value = v)
        .LoopForever();

      tween.ManualUpdate(1.5f);

      Assert.True(tween.IsAlive(), "Tween is still running");
      Assert.AreEqual(0.5f, value, Mathf.Epsilon, "Next loop is progressed");
    }
  }
}
