using NUnit.Framework;
using System;
using UnityEngine;

namespace PeachyTween.Tests {
  public class OnUpdateTest : BaseTweenTest {
    struct TestGroup { }

    [Test]
    public void OnUpdateSingleHandler() {
      var callCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnUpdate(() => callCount++)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);
      Peachy.Run<TestGroup>(0.5f);
      Assert.AreEqual(2, callCount, $"{nameof(OnUpdate)} callback called twice");
    }

    [Test]
    public void OnUpdateMultipleHandlers() {
      var aCount = 0;
      var bCount = 0;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnUpdate(() => aCount++)
        .OnUpdate(() => bCount++)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);

      Assert.AreEqual(1, aCount, $"first {nameof(OnUpdate)} callback called");
      Assert.AreEqual(1, bCount, $"second {nameof(OnUpdate)} callback called");
    }

    [Test]
    public void RemoveOnUpdate() {
      var aCount = 0;
      var bCount = 0;

      void a() => aCount++;
      void b() => bCount++;

      var tween = Peachy
        .Tween(0f, 1f, 1f, _ => {})
        .OnUpdate(a)
        .OnUpdate(b)
        .RemoveOnUpdate(b)
        .SetGroup<TestGroup>();

      Peachy.Run<TestGroup>(0.5f);

      Assert.AreEqual(1, aCount, $"first {nameof(OnUpdate)} callback called");
      Assert.AreEqual(0, bCount, $"second {nameof(OnUpdate)} callback was not called");
    }

    [Test]
    public void SyncOnUpdate() {
      var caught = null as Exception;
      var onUpdate = 0;
      var tween = Peachy.Tween(0f, 1f, 1f, _ => {});

      tween.OnUpdate(() => {
        onUpdate++;
        try {
          tween.Sync();
        } catch (Exception e) {
          caught = e;
        }
      });

      tween.ManualUpdate(0.5f);

      Assert.AreEqual(1, onUpdate, $"{nameof(OnUpdate)} callback called");
      Assert.True(caught is Exception, $"{nameof(Exception)} caught");
    }
  }
}
