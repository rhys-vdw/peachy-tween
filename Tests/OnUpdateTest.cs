using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class OnUpdateTest : BaseTweenTest {
    struct TestGroup { }

    [Test]
    public void OnUpdateSingleHandler() {
      var callCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnUpdate(() => callCount++)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);
      Peachy.Run<TestGroup>(0.5f);
      Assert.AreEqual(2, callCount, $"{nameof(OnUpdate)} callback called twice");
    }

    [Test]
    public void OnUpdateMultipleHandlers() {
      var aCount = 0;
      var bCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnUpdate(() => aCount++)
        .OnUpdate(() => bCount++)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);

      Assert.AreEqual(1, aCount, $"first {nameof(OnUpdate)} callback called");
      Assert.AreEqual(1, bCount, $"second {nameof(OnUpdate)} callback called");
    }
  }
}
