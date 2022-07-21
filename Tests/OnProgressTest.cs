using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class OnProgressTest : BaseTweenTest {
    struct TestGroup { }

    [Test]
    public void OnProgressSingleHandler() {
      var callCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnProgress(() => callCount++)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);
      Peachy.Run<TestGroup>(0.5f);
      Assert.AreEqual(2, callCount, $"{nameof(OnProgress)} callback called twice");
    }

    [Test]
    public void OnProgressMultipleHandlers() {
      var aCount = 0;
      var bCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnProgress(() => aCount++)
        .OnProgress(() => bCount++)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);

      Assert.AreEqual(1, aCount, $"first {nameof(OnProgress)} callback called");
      Assert.AreEqual(1, bCount, $"second {nameof(OnProgress)} callback called");
    }
  }
}
