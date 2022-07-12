using UnityEngine;

namespace RhysTween {
  internal class UnityLifecycle : MonoBehaviour {
#pragma warning disable IDE0051

    void Update() => RhysTween.Run<Update>(Time.deltaTime);

    void LateUpdate() => RhysTween.Run<LateUpdate>(Time.deltaTime);

    void FixedUpdate() => RhysTween.Run<FixedUpdate>(Time.deltaTime);

    void OnDestroy() => RhysTween.Destroy();

#pragma warning restore IDE0051
  }
}