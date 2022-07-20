using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class LoopTest : BaseTweenTest {
    [Test]
    public void LoopRewind() {
      var value = 0f;
      var tween = Peachy
        .Tween(0f, 1f, 1f, v => value = v)
        .LoopForever();

      tween.ManualUpdate(1.5f);

      Assert.True(tween.IsValid(), "Tween is still running");
      Assert.AreEqual(0.5f, value, Mathf.Epsilon, "Next loop is progressed");
    }
  }
}


