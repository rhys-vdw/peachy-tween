using System.Reflection;
using System;

namespace PeachyTween.Tests {
  public static class TestUtility {
    public static void InitializeEcs() =>
      Core.InitializeEcs();

    public static void Destroy() =>
      Core.Destroy();

    static void InvokePeachyStaticMethod(string methodName) {
      var method = Type.GetType("PeachyTween.Core").GetMethod(
        methodName,
        BindingFlags.Static | BindingFlags.NonPublic
      );
      method.Invoke(null, null);
    }
  }
}


