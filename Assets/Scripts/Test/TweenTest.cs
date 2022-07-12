using System.Collections;
using UnityEngine;

namespace RhysTween.Test {
  public class TweenTest : MonoBehaviour {
    public Transform From;
    public Transform To;
    public float Duration = 5f;
    public bool IsManuallyUpdating = false;
    Tween _tween;

    public void Start() {
      _tween = transform
        .TPosition(To.position, Duration)
        .Loop(2)
        .Preserve()
        .LoopForever()
        .OnKill(() => Debug.Log("Kill!"))
        .OnComplete(() => Debug.Log("Done!"));
      // transform
      //   .TRotation(new Vector3(90, 90, 90), Duration)
      //   .Slerp()
      //   .LoopForever()
      //   .OnComplete(() => Debug.Log("Done!"));

      StartCoroutine(Coroutine());
    }

    // void Update() {
    //   if (IsManuallyUpdating) {
    //     _tween.ManualUpdate(Time.deltaTime);
    //   }
    // }

    IEnumerator Coroutine() {
      yield return new WaitForSeconds(Duration * 0.5f);
      _tween.Complete();
      yield return new WaitForSeconds(Duration * 1.4f);
      _tween.Pause();
      yield return new WaitForSeconds(Duration * 1.4f);
      _tween.GoTo(0.8f * Duration).Resume();
        // yield return new WaitForSeconds(1f);
        // Debug.Log("Goto 2.5");
        // _tween.GoTo(2.5f);
        // yield return new WaitForSeconds(1f);
        // Debug.Log("Rewind");
        // _tween.Rewind();
        // yield return new WaitForSeconds(1f);
        // Debug.Log("Complete");
        // _tween.Complete();
      // }
    }
  }
}
