using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PeachyTween.Tests {
  public class GroupTest {
    struct TestGroupA { }
    struct TestGroupB { }

    [SetUp]
    public void SetUp() {
      var method = typeof(Peachy).GetMethod(
        "InitializeEcs",
        BindingFlags.Static | BindingFlags.NonPublic
      );
      method.Invoke(null, null);
    }

    [TearDown]
    public void TearDown() => Peachy.Destroy();

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

