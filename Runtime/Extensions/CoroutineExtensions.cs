using System.Collections;

namespace PeachyTween {
  /// <summary>
  /// Convenience functions for converting tweens to Unity coroutines.
  /// </summary>
  public static class CoroutineExtensions {
    public static IEnumerator ToCoroutine(this Tween tween) {
      while (tween.IsActive()) {
        yield return null;
      }
    }
  }
}