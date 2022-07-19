using System.Collections;
using UnityEngine;

namespace PeachyTween.Test {
  public class TweenExample : MonoBehaviour {
    public int OscillationCount = 5;
    public float Duration = 1f;
    public float AmplitudeDecay = 0f;
    public float FrequencyDecay = 0f;
    public Transform To;
    Tween _tween;

    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        _tween.Kill(complete: true).Sync();
        _tween = transform
          .TweenPosition(To.position, Duration)
          .Punch(OscillationCount, AmplitudeDecay, FrequencyDecay);
          // .Punch(5)
          // .OnComplete(() => Debug.Log("Done!"));
      }
      if (Input.GetKeyDown(KeyCode.R)) {
        _tween.Reverse();
      }
    }
  }
}
