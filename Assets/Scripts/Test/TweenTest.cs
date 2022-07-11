using UnityEngine;

namespace RhysTween.Test {
  public class TweenTest : MonoBehaviour {
    public Transform From;
    public Transform To;
    public float Duration = 5f;

    public void Start() {
      RhysTween
        .Tween(From.position, To.position, Duration, v => transform.position = v)
        .OnComplete(() => Debug.Log("Done!"));
    }
  }
}
