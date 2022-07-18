using UnityEngine;
using NUnit.Framework;
using System.Reflection;

namespace PeachyTween.Tests {
  public static class TestUtility {
    public static void InitializeEcs() =>
      InvokePeachyStaticMethod(nameof(InitializeEcs));

    public static void Destroy() =>
      InvokePeachyStaticMethod(nameof(Destroy));

    static void InvokePeachyStaticMethod(string methodName) {
      var method = typeof(Peachy).GetMethod(
        methodName,
        BindingFlags.Static | BindingFlags.NonPublic
      );
      method.Invoke(null, null);
    }
  }
}


