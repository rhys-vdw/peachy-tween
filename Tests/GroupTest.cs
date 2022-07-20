using UnityEngine;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class GroupTest {
    struct TestGroupA { }
    struct TestGroupB { }

    [SetUp]
    public void SetUp() => TestUtility.InitializeEcs();

    [TearDown]
    public void TearDown() => TestUtility.Destroy();

    [Test]
    public void CustomGroupTest() {
      var aRan = false;
      var bRan = false;

      var tweenA = Peachy.Tween(0f, 1f, 1f, v => aRan = true).SetGroup<TestGroupA>();
      var tweenB = Peachy.Tween(0f, 1f, 1f, v => bRan = true).SetGroup<TestGroupB>();

      Peachy.Run<TestGroupA>(1f);

      Assert.True(aRan, "Selected group ran");
      Assert.False(bRan, "Unselected group did not run");
    }
  }
}

