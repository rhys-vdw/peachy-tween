using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class KillTest : BaseTweenTest {
    [Test]
    public void KillManualUpdate() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true);

      tween.Kill();

      Assert.False(onKill, "Did not call OnKill prematurely");
      Assert.True(tween.IsAlive(), "Tween is still alive");

      tween.ManualUpdate(0.5f);

      Assert.False(onComplete, "Did not call OnComplete");
      Assert.False(onChange, "Did not call OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
    }

    struct TestGroup { }

    [Test]
    public void KillGroupUpdate() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true)
        .SetGroup<TestGroup>();

      tween.Kill();

      Assert.False(onKill, "Did not call OnKill prematurely");
      Assert.True(tween.IsAlive(), "Tween is still alive");

      Core.Run<TestGroup>(0.5f);

      Assert.False(onComplete, "Did not call OnComplete");
      Assert.False(onChange, "Did not call OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
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

      Assert.False(onComplete, "Did not call OnComplete");
      Assert.False(onChange, "Did not call OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
    }

    [Test]
    public void KillCompleteSync() {
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
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
    }

    [Test]
    public void KillCompleteGroupUpdate() {
      var onChange = false;
      var onKill = false;
      var onComplete = false;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => onChange = true)
        .OnComplete(() => onComplete = true)
        .OnKill(() => onKill = true)
        .SetGroup<TestGroup>();

      tween.Kill(complete: true);
      Core.Run<TestGroup>(0.5f);

      Assert.True(onComplete, "Called OnComplete");
      Assert.True(onChange, "Called OnChange");
      Assert.True(onKill, "Called OnKill");
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
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

      Assert.False(tween.IsAlive(), "Tween is no longer alive");
      Assert.AreEqual(0, logCount, "No logs yet");

      tween.Kill();

      Assert.AreEqual(0, logCount, "Does not log on kill");

      const string expectedMessage = "Tween is not alive";

      Debug.Log($"'{expectedMessage}' warning logged after this is expected:");
      logCount--; // Subtract one for log above.

      tween.Sync();

      Assert.AreEqual(1, logCount, "Logs on sync after kill");
      Assert.AreEqual(expectedMessage, lastLog.message, "Logs correct message");
      Assert.AreEqual(LogType.Warning, lastLog.type, "Log is warning");

      tween.KillSync();

      Assert.AreEqual(1, logCount, $"Does not log on {nameof(Tween.KillSync)}");
    }
  }
}
