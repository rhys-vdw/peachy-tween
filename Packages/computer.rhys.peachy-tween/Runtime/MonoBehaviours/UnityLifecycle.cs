using UnityEngine;

namespace PeachyTween {
  internal class UnityLifecycle : MonoBehaviour {
#pragma warning disable IDE0051

    void Update() {
      Core.Run<Update>(Time.deltaTime);
      Core.Run<UnscaledUpdate>(Time.unscaledDeltaTime);
    }

    void LateUpdate() {
      Core.Run<LateUpdate>(Time.deltaTime);
      Core.Run<UnscaledLateUpdate>(Time.unscaledDeltaTime);
    }

    void FixedUpdate() {
      Core.Run<FixedUpdate>(Time.deltaTime);
      Core.Run<UnscaledFixedUpdate>(Time.unscaledDeltaTime);
    }

    void OnDestroy() => Core.Destroy();

#pragma warning restore IDE0051
  }
}