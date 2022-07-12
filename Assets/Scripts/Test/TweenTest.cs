using System.Collections;
using UnityEngine;

namespace RhysTween.Test {
  public class TweenTest : MonoBehaviour {
    public Transform From;
    public Transform To;
    public float Duration = 5f;
    Tween _tween;

    public void Start() {
      transform
        .TPosition(To.position, Duration)
        // .LoopForever()
        .SetFixedUpdate()
        .OnComplete(() => Debug.Log("Done!"));
      _tween = transform
        .TRotation(new Vector3(90, 90, 90), Duration)
        .Slerp()
        .LoopForever()
        .OnComplete(() => Debug.Log("Done!"));

      // StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine() {
      while (true) {
        _tween.Pause();
        yield return new WaitForSeconds(0.4f);
        _tween.Resume();
        yield return new WaitForSeconds(0.4f);
      }
    }
  }
}
