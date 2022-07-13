using System.Collections;
using UnityEngine;

namespace PeachyTween.Test {
  public class TweenTest : MonoBehaviour {
    public Transform From;
    public Transform To;
    public float Duration = 5f;
    public bool IsManuallyUpdating = false;
    Tween _tween;

    public void Start() {
      _tween = transform
        .TPosition(To.position, Duration)
        .LoopForever()
        .OnLoop(() => Debug.Log("Loop!"))
        // .Preserve()
        .OnKill(() => Debug.Log("Kill!"))
        .OnKill(() => Debug.Log("KILL KILL!"))
        .OnComplete(() => {
          Debug.Log("Done!");
          StartCoroutine(Coroutine());
        });
      // transform
      //   .TRotation(new Vector3(90, 90, 90), Duration)
      //   .Slerp()
      //   .LoopForever()
      //   .OnComplete(() => Debug.Log("Done!"));
    }

    // void Update() {
    //   if (IsManuallyUpdating) {
    //     _tween.ManualUpdate(Time.deltaTime);
    //   }
    // }

    IEnumerator Coroutine() {
      yield return new WaitForSeconds(0.5f);
      _tween.GoTo(0.5f);
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
