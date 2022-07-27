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

    [Test]
    public void AppendDelay() {
      var aChangeCount = 0;
      var bChangeCount = 0;

      var sequence = Peachy.Sequence()
        .Append(Peachy.Tween(0f, 1f, 1f, _ => aChangeCount++))
        .AppendDelay(1f)
        .Append(Peachy.Tween(0f, 1f, 1f, _ => bChangeCount++));

      var t = sequence.ToTween();

      t.ManualUpdate(0.5f);
      Assert.AreEqual(1, aChangeCount);
      Assert.AreEqual(0, bChangeCount);

      t.ManualUpdate(1f);
      Assert.AreEqual(2, aChangeCount);
      Assert.AreEqual(0, bChangeCount);

      t.ManualUpdate(0.1f);
      Assert.AreEqual(2, aChangeCount);
      Assert.AreEqual(0, bChangeCount);

      t.ManualUpdate(1f);
      Assert.AreEqual(2, aChangeCount);
      Assert.AreEqual(1, bChangeCount);
    }
  }
}