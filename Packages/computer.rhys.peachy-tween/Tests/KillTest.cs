using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class KillTest {
    [SetUp]
    public void SetUp() => TestUtility.InitializeEcs();

    [TearDown]
    public void TearDown() => TestUtility.Destroy();

    [Test]
    public void Kill() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true)
        .SetManualUpdate()
        .Kill();

      Assert.False(onKill, $"Called OnKill prematurely");
      Assert.True(tween.IsValid(), $"Tween is still valid");

      tween.ManualUpdate(0.5f);

      Assert.False(onComplete, $"Called OnComplete");
      Assert.False(onChange, $"Called OnChange");
      Assert.True(onKill, $"Called OnKill");
      Assert.False(tween.IsValid(), $"Tween is no longer valid");
    }

    [Test]
    public void KillThenSync() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true)
        .Kill()
        .Sync();

      Assert.False(onComplete, $"Called OnComplete");
      Assert.False(onChange, $"Called OnChange");
      Assert.True(onKill, $"Called OnKill");
      Assert.False(tween.IsValid(), $"Tween is no longer valid");
    }

    [Test]
    public void KillComplete() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true)
        .Kill(complete: true)
        .Sync();

      Assert.True(onComplete, $"Called OnComplete");
      Assert.True(onChange, $"Called OnChange");
      Assert.True(onKill, $"Called OnKill");
      Assert.False(tween.IsValid(), $"Tween is no longer valid");
    }
  }
}


