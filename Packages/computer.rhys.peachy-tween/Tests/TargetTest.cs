using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class TargetTest {
    [SetUp]
    public void SetUp() => TestUtility.InitializeEcs();

    [TearDown]
    public void TearDown() => TestUtility.Destroy();

    [Test]
    public void SetTargetTest() {
      var expected = new Object();
      var tween = Peachy.Tween(0f, 1f, 1f, v => {}).SetTarget(expected);

      Assert.True(tween.TryGetTarget(out var actual), "Target was set");
      Assert.AreEqual(actual, expected, "Target was set correctly");
    }

    [Test]
    public void TryGetTargetWithoutTarget() {
      var tween = Peachy.Tween(0f, 1f, 1f, v => {});

      Assert.False(tween.TryGetTarget(out var target), "No target was set");
    }
  }
}

