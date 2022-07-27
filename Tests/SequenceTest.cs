using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class SequenceTest : BaseTweenTest {
    [Test]
    public void MembersComplete() {
      var completeCount = 0;
      var sequnceCompleteCount = 0;

      var sequence = Peachy.Sequence();
      for (var i = 0; i < 3; i++) {
        var sub = Peachy
          .Tween(0f, 1f, 1f, _ => {})
          .OnComplete(() => completeCount++);
        sequence.Append(sub);
      }

      var st = sequence
        .ToTween()
        .OnComplete(() => sequnceCompleteCount++);

      for (var i = 1; i < 4; i++) {
        st.ManualUpdate(1f);
        Assert.AreEqual(i, completeCount, "Tween complete called");
      }

      Assert.AreEqual(1, sequnceCompleteCount, "Sequence complete called");
    }
  }
}