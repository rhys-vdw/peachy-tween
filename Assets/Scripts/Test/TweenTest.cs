using UnityEngine;

namespace RhysTween.Test {
  public class TweenTest : MonoBehaviour {
    public Transform From;
    public Transform To;
    public float Duration = 5f;

    public void Start() {
      transform
        .TPosition(To.position, Duration)
        .OnComplete(() => Debug.Log("Done!"));
      transform
        .TRotation(new Vector3(90, 90, 90), Duration)
        .OnComplete(() => Debug.Log("Done!"));
    }
  }
}
