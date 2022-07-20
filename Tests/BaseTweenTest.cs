using System.Reflection;
using System;
using NUnit.Framework;

namespace PeachyTween.Tests {
  public class BaseTweenTest {
    [SetUp]
    public void SetUp() => Core.InitializeEcs();

    [TearDown]
    public void TearDown() => Core.Destroy();
  }
}