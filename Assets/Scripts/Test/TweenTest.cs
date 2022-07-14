using System.Collections;
using UnityEngine;

namespace PeachyTween.Test {
  public class TweenTest : MonoBehaviour {
    public Transform From;
    public Transform To;
    public float Duration = 5f;
    public bool IsManuallyUpdating = false;
    public Ease Ease = Ease.Linear;
    public AnimationCurve Curve = AnimationCurve.Linear(0, 1, 0, 1);
    Tween _tween;

    public void Start() {
      _tween = transform
        .TweenPosition(To.position, Duration)
        .From()
        .LoopForever()
        .PingPong()
        .Ease(Ease)
        // .Preserve()
        .OnKill(() => Debug.Log("Kill!"));
        // .OnComplete(() => {
        //   Debug.Log("Done!");
        //   // StartCoroutine(Coroutine());
        // });
      transform
        .TweenRotation(new Vector3(90, 90, 90), Duration)
        .Slerp()
        .LoopForever()
        .OnComplete(() => Debug.Log("Done!"));
      Peachy.Tween(
        Vector2.left,
        Vector2.right,
        1f,
        v => {
          Debug.DrawRay(
            Vector3.zero,
            v,
            Color.red
          );
        }
      ).LoopForever();
      Peachy.Tween(
        Vector2.left,
        Vector2.right,
        1f,
        v => {
          Debug.DrawRay(
            Vector3.zero,
            v,
            Color.green
          );
        }
      ).Rotate().LoopForever();
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        _tween.Reverse();
      }
    }

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
