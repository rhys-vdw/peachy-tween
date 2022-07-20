using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class SlerpTest : BaseTweenTest {
    [Test]
    public void SlerpVector3() {
      var actual = null as Vector3?;

      var expected = Vector3.up;
      var neg = Quaternion.Euler(0, 0, -20) * expected;
      var pos = Quaternion.Euler(0, 0, 20) * expected;

      var tween = Peachy
        .Tween(neg, pos, 1f, v => actual = v)
        .Slerp();

      tween.ManualUpdate(0.5f);

      // NOTE: == is approximate comparison.
      Assert.True(expected == actual, $"Vector3 was slerped");
    }

    [Test]
    public void SlerpVector2() {
      var actual = null as Vector2?;

      var expected = Vector2.up;
      var neg = expected.Rotate(-20);
      var pos = expected.Rotate(20);

      var tween = Peachy
        .Tween(neg, pos, 1f, v => actual = v)
        .Slerp();

      tween.ManualUpdate(0.5f);

      // NOTE: == is approximate comparison.
      Assert.True(expected == actual, $"Vector2 was slerped");
    }
  }
}



