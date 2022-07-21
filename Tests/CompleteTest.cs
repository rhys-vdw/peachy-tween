using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class CompleteTest : BaseTweenTest {
    struct TestGroup { }

    [Test]
    public void Complete() {
      var onChangeCount = 0;
      var onChangeValue = null as float?;
      var onCompleteCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => {
          onChangeCount++;
          onChangeValue = v;
        })
        .OnComplete(() => onCompleteCount++)
        .SetGroup<TestGroup>();

      tween.Complete();
      Peachy.Run<TestGroup>(0);

      Assert.AreEqual(1, onChangeCount, "onChange was called");
      Assert.AreEqual(1f, onChangeValue, "Tween was completed");
      Assert.AreEqual(1, onCompleteCount, "onComplete was called");
      Assert.True(!tween.IsAlive(), "Tween is no longer alive");
    }

    [Test]
    public void CompletePreserve() {
      var onChangeCount = 0;
      var onChangeValue = null as float?;
      var onCompleteCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => {
          onChangeCount++;
          onChangeValue = v;
        })
        .OnComplete(() => onCompleteCount++)
        .SetGroup<TestGroup>()
        .Preserve();

      tween.Complete();
      Peachy.Run<TestGroup>(0);

      Assert.AreEqual(1, onChangeCount, "onChange was called");
      Assert.AreEqual(1f, onChangeValue, "Tween was completed");
      Assert.AreEqual(1, onCompleteCount, "onComplete was called");
      Assert.True(tween.IsAlive(), "Tween is still alive");
    }

    [Test]
    public void CompleteLoop() {
      var onChangeCount = 0;
      var onChangeValue = null as float?;
      var onCompleteCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => {
          onChangeCount++;
          onChangeValue = v;
        })
        .OnComplete(() => onCompleteCount++)
        .Loop(2)
        .SetGroup<TestGroup>();

      tween.Complete();
      Peachy.Run<TestGroup>(0);

      Assert.AreEqual(1, onChangeCount, "onChange was called");
      Assert.AreEqual(1f, onChangeValue, "Tween was completed");
      Assert.AreEqual(1, onCompleteCount, "onComplete was called");
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
    }

    static object[] CompletePingPongCases = new [] {
      new object[] { 1, 0f },
      new object[] { 2, 1f },
      new object[] { 3, 0f },
      new object[] { 4, 1f },
      new object[] { -1, 1f },
    };

    [TestCaseSource(nameof(CompletePingPongCases))]
    public void CompletePingPong(int loops, float end) {
      var onChangeCount = 0;
      var onChangeValue = null as float?;
      var onCompleteCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, v => {
          onChangeCount++;
          onChangeValue = v;
        })
        .OnComplete(() => onCompleteCount++)
        .SetLooping(loops)
        .PingPong()
        .SetGroup<TestGroup>();

      tween.Complete();
      Peachy.Run<TestGroup>(0);

      Assert.AreEqual(1, onChangeCount, "onChange was called");
      Assert.AreEqual(end, onChangeValue, "Tween completed back to initial value");
      Assert.AreEqual(1, onCompleteCount, "onComplete was called");
      Assert.False(tween.IsAlive(), "Tween is no longer alive");
    }
  }
}
