using System.Reflection;
using System;

namespace PeachyTween.Tests {
  public static class TestUtility {
    public static void InitializeEcs() =>
      Core.InitializeEcs();

    public static void Destroy() =>
      Core.Destroy();
  }
}


