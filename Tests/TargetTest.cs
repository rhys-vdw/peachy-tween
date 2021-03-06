using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class TargetTest : BaseTweenTest {
    [Test]
    public void SetTarget() {
      var expected = new Object();
      var tween = Peachy.Tween(0f, 1f, 1f, v => {}).SetTarget(expected);

      Assert.True(tween.TryGetTarget(out var actual), "Target was set");
      Assert.AreEqual(expected, actual, "Target was set correctly");
    }

    [Test]
    public void TryGetTargetWithoutTarget() {
      var tween = Peachy.Tween(0f, 1f, 1f, v => {});

      Assert.False(tween.TryGetTarget(out var target), "No target was set");
    }

    [Test]
    public void KillAllWithTarget() {
      var target = new Object();
      var onKill = false;

      var targeting = Peachy
        .Tween(0f, 1f, 1f, v => {})
        .OnKill(() => onKill = true)
        .SetTarget(target);
      var nonTargeting = Peachy.Tween(0f, 1f, 1f, v => {});

      Peachy.KillAllWithTarget(target);

      targeting.Sync();
      nonTargeting.Sync();

      Assert.True(nonTargeting.IsAlive(), "Did not kill untargeting tween");
      Assert.True(onKill, "Called OnKill");
      Assert.False(targeting.IsAlive(), "Targeting tween is invalid");
    }
  }
}
