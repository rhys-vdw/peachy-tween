using UnityEngine;

namespace PeachyTween {
  internal class UnityLifecycle : MonoBehaviour {
#pragma warning disable IDE0051

    void Update() => Peachy.Run<Update>(Time.deltaTime);

    void LateUpdate() => Peachy.Run<LateUpdate>(Time.deltaTime);

    void FixedUpdate() => Peachy.Run<FixedUpdate>(Time.deltaTime);

    void OnDestroy() => Peachy.Destroy();

#pragma warning restore IDE0051
  }
}