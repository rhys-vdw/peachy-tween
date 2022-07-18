using System.Collections;
using UnityEngine;

namespace PeachyTween.Test {
  public class TweenExample : MonoBehaviour {
    public float Duration = 1f;
    public float VibrationCount = 4;
    public float Rotation = 1080f;
    Tween _tween;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        _tween.Kill(complete: true).Sync();
        _tween = transform
          .TweenRotation(new Vector3(0, Rotation, 0), Duration)
          .LoopForever()
          .PingPong()
          .OnLoop(() => Debug.Log("Loop!"));
          // .Punch(5)
          // .OnComplete(() => Debug.Log("Done!"));
      }
      if (Input.GetKeyDown(KeyCode.R)) {
        _tween.Reverse();
      }
    }
  }
}
