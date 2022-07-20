using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class ShortestAngleTest {
    [SetUp]
    public void SetUp() => TestUtility.InitializeEcs();

    [TearDown]
    public void TearDown() => TestUtility.Destroy();

    [Test]
    public void ShortestAngle() {
      var actual = null as float?;

      var tween = Peachy
        .Tween(350f, 10f, 1f, v => actual = v)
        .ShortestAngle();

      tween.ManualUpdate(0.5f);

      Assert.AreEqual(360f, actual, $"Rotated around shortest angle");
    }
  }
}



