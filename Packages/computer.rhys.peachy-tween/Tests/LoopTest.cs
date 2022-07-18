using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class LoopTest {
    [SetUp]
    public void SetUp() => TestUtility.InitializeEcs();

    [TearDown]
    public void TearDown() => TestUtility.Destroy();

    [Test]
    public void LoopRewindTest() {
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


