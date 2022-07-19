using UnityEngine;

namespace PeachyTween {
  internal class UnityLifecycle : MonoBehaviour {
#pragma warning disable IDE0051

    void Update() => Core.Run<Update>(Time.deltaTime);

    void LateUpdate() => Core.Run<LateUpdate>(Time.deltaTime);

    void FixedUpdate() => Core.Run<FixedUpdate>(Time.deltaTime);

    void OnDestroy() => Core.Destroy();

#pragma warning restore IDE0051
  }
}