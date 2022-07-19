using System.Collections;
using UnityEngine;

namespace PeachyTween.Test {
  public class TweenExample : MonoBehaviour {
    public Vector3 Range = Vector3.one;
    public int OscillationCount = 5;
    public float Duration = 1f;
    public float AmplitudeDecay = 1f;
    public float FrequencyDecay = 1f;
    public float AmplitudeRandomness = 0f;
    public float FrequencyRandomness = 0f;
    public Transform To;
    Tween _tween;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        _tween.Kill(complete: true).Sync();
        _tween = transform
          .TweenScale(Range, Duration)
          .Shake(
            OscillationCount,
            AmplitudeDecay,
            FrequencyDecay,
            AmplitudeRandomness,
            FrequencyRandomness
          )
          .PingPong()
          .LoopForever();
          // .Punch(5)
          // .OnComplete(() => Debug.Log("Done!"));
      }
      if (Input.GetKeyDown(KeyCode.R)) {
        _tween.Reverse();
      }
    }
  }
}
