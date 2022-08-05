using System.Collections;

namespace PeachyTween {
  /// <summary>
  /// Convenience functions for converting tweens to Unity coroutines.
  /// </summary>
  public static class CoroutineExtensions {
    /// <summary>
    /// Convert the tween to a coroutine.
    /// </summary>
    /// <example>
    /// <code>
    /// void Start() {
    ///     StartCoroutine(MoveAndLog());
    /// }
    ///
    /// IEnumerator MoveAndLog() {
    ///     var tween = transform.TweenPosition(
    ///         transform.position + Vector3.up,
    ///         0.5f
    ///     );
    ///     yield return tween.ToCoroutine();
    ///     Debug.Log("Done moving!");
    /// }
    /// </code>
    /// </example>
    /// <param name="tween">The tween.</param>
    /// <returns>An enumerator that can be used in a coroutine.</returns>
    public static IEnumerator ToCoroutine(this Tween tween) {
      while (tween.IsActive()) {
        yield return null;
      }
    }
  }
}