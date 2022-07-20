using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class KillTest : BaseTweenTest {
    [Test]
    public void Kill() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true);

      tween.Kill();

      Assert.False(onKill, "Called OnKill prematurely");
      Assert.True(tween.IsValid(), "Tween is still valid");

      tween.ManualUpdate(0.5f);

      Assert.False(onComplete, "Called OnComplete");
      Assert.False(onChange, "Called OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsValid(), "Tween is no longer valid");
    }

    [Test]
    public void KillSync() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true);

      tween.Kill();
      tween.Sync();

      Assert.False(onComplete, "Called OnComplete");
      Assert.False(onChange, "Called OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsValid(), "Tween is no longer valid");
    }

    [Test]
    public void KillComplete() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true);

      tween.Kill(complete: true);
      tween.Sync();

      Assert.True(onComplete, "Called OnComplete");
      Assert.True(onChange, "Called OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsValid(), "Tween is no longer valid");
    }

    [Test]
    public void KillWarnings() {
      (string message, LogType type) lastLog = default;
      int logCount = 0;

      void HandleLog(string message, string stackTrace, LogType logType) {
        lastLog = (message, logType);
        logCount++;
      }

      Application.logMessageReceived += HandleLog;

      var tween = Peachy.Tween(0f, 1f, 1f, v => {});
      tween.Kill();
      tween.Sync();

      Assert.False(tween.IsValid(), "Tween is no longer valid");
      Assert.AreEqual(0, logCount, "No logs yet");

      tween.Kill();

      Assert.AreEqual(0, logCount, "Does not log on kill");

      const string expectedMessage = "Tween is invalid";

      Debug.Log($"'{expectedMessage}' warning logged after this is expected:");
      logCount--; // Subtract one for log above.

      tween.Sync();

      Assert.AreEqual(1, logCount, "Logs on sync after kill");
      Assert.AreEqual(expectedMessage, lastLog.message, "Logs on invalid tween");
      Assert.AreEqual(LogType.Warning, lastLog.type, "Log is warning");

      tween.KillSync();

      Assert.AreEqual(1, logCount, $"Does not log on {nameof(Tween.KillSync)}");
    }
  }
}


