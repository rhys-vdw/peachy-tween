// Adapted from https://github.com/Cysharp/UniTask/blob/78db78c7bd5c1c75902dabb1496c85bd2316fca5/src/UniTask/Assets/Plugins/UniTask/Runtime/External/DOTween/DOTweenAsyncExtensions.cs

#if PEACHY_UNITASK_SUPPORT

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Internal;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PeachyTween {
  public enum TweenCancelBehavior {
    Kill,
    Complete,
    CompleteAndKill,
    CancelAsync
  }

  public static class UniTaskExtensions {
    enum CallbackType {
      Kill,
      Complete,
    }

    public static TweenAwaiter GetAwaiter(this Tween tween) {
      return new TweenAwaiter(tween);
    }

    public static UniTask WithCancellation(this Tween tween, CancellationToken cancellationToken) {
      if (!tween.IsActive()) return UniTask.CompletedTask;
      return new UniTask(
        TweenConfiguredSource.Create(
          tween,
          TweenCancelBehavior.Kill,
          cancellationToken,
          CallbackType.Kill,
          out var token
        ),
        token
      );
    }

    public static UniTask ToUniTask(this Tween tween, TweenCancelBehavior tweenCancelBehavior = TweenCancelBehavior.Kill, CancellationToken cancellationToken = default) {
      if (!tween.IsActive()) return UniTask.CompletedTask;
      return new UniTask(
        TweenConfiguredSource.Create(
          tween,
          tweenCancelBehavior,
          cancellationToken,
          CallbackType.Kill,
          out var token
        ),
        token
      );
    }

    public static UniTask AwaitComplete(this Tween tween, TweenCancelBehavior tweenCancelBehavior = TweenCancelBehavior.Kill, CancellationToken cancellationToken = default) {
      if (!tween.IsActive()) return UniTask.CompletedTask;
      return new UniTask(
        TweenConfiguredSource.Create(
          tween,
          tweenCancelBehavior,
          cancellationToken,
          CallbackType.Complete,
          out var token
        ),
        token
      );
    }

    public struct TweenAwaiter : INotifyCompletion  {
      readonly Tween _tween;

      public bool IsCompleted => !_tween.IsActive();

      public TweenAwaiter(Tween tween) {
        _tween = tween;
      }

      public TweenAwaiter GetAwaiter() => this;

      public void GetResult() { }

      public void OnCompleted(System.Action continuation) =>
        _tween.OnKill(PooledTweenCallback.Create(continuation));
    }

    sealed class TweenConfiguredSource : IUniTaskSource, ITaskPoolNode<TweenConfiguredSource> {
      static TaskPool<TweenConfiguredSource> _pool;
      TweenConfiguredSource _nextNode;
      public ref TweenConfiguredSource NextNode => ref _nextNode;

      static TweenConfiguredSource() {
        TaskPool.RegisterSizeGetter(typeof(TweenConfiguredSource), () => _pool.Size);
      }

      readonly Action _handleCompleteTween;
      readonly Action _handleUpdateTween;

      Tween _tween;
      TweenCancelBehavior _cancelBehavior;
      CancellationToken _cancellationToken;
      CallbackType _callbackType;

      UniTaskCompletionSourceCore<AsyncUnit> _core;

      TweenConfiguredSource() {
        _handleCompleteTween = HandleTweenComplete;
        _handleUpdateTween = HandleTweenUpdate;
      }

      public static IUniTaskSource Create(Tween tween, TweenCancelBehavior cancelBehavior, CancellationToken cancellationToken, CallbackType callbackType, out short token) {
        if (cancellationToken.IsCancellationRequested) {
          CancelTween(tween, cancelBehavior);
          return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
        }

        if (!_pool.TryPop(out var result)) {
          result = new TweenConfiguredSource();
        }

        result._tween = tween;
        result._cancelBehavior = cancelBehavior;
        result._cancellationToken = cancellationToken;
        result._callbackType = callbackType;

        tween.OnUpdate(result._handleUpdateTween);

        switch (callbackType) {
          case CallbackType.Complete:
            tween.OnComplete(result._handleCompleteTween);
            break;
          case CallbackType.Kill:
          default:
            tween.OnKill(result._handleCompleteTween);
            break;
        }

        TaskTracker.TrackActiveTask(result, 3);

        token = result._core.Version;
        return result;
      }

      void HandleTweenComplete() {
        if (_cancellationToken.IsCancellationRequested) {
          _core.TrySetCanceled(_cancellationToken);
          // Should be safe to call `Complete` or `Kill` on a completed tween.
          CancelTween(_tween, _cancelBehavior);
        } else {
          _core.TrySetResult(AsyncUnit.Default);
        }
      }

      void HandleTweenUpdate() {
        if (_cancellationToken.IsCancellationRequested) {
          CancelTween(_tween, _cancelBehavior);
          _core.TrySetCanceled(_cancellationToken);
        }
      }

      static void CancelTween(Tween tween, TweenCancelBehavior cancelBehavior) {
        switch (cancelBehavior) {
          case TweenCancelBehavior.Kill:
            tween.Kill();
            break;
          case TweenCancelBehavior.Complete:
            tween.Complete();
            break;
          case TweenCancelBehavior.CompleteAndKill:
            tween.Kill(complete: true);
            break;
          case TweenCancelBehavior.CancelAsync:
            // Do nothing.
            break;
        }
      }

      public void GetResult(short token) {
        try {
          _core.GetResult(token);
        } finally {
          TryReturn();
        }
      }

      public UniTaskStatus GetStatus(short token) =>
        _core.GetStatus(token);

      public UniTaskStatus UnsafeGetStatus() =>
        _core.UnsafeGetStatus();

      public void OnCompleted(Action<object> continuation, object state, short token) =>
        _core.OnCompleted(continuation, state, token);

      bool TryReturn() {
        TaskTracker.RemoveTracking(this);
        _core.Reset();
        _tween.RemoveOnUpdate(_handleUpdateTween);
        switch (_callbackType) {
          case CallbackType.Complete:
            _tween.RemoveOnComplete(_handleCompleteTween);
            break;
          default:
          case CallbackType.Kill:
            _tween.RemoveOnKill(_handleCompleteTween);
            break;
        }
        _tween = default;
        _cancellationToken = default;
        return _pool.TryPush(this);
      }
    }
  }

  sealed class PooledTweenCallback {
    static readonly ConcurrentQueue<PooledTweenCallback> _pool = new ();

    readonly Action _run;

    Action _continuation;


    PooledTweenCallback() {
      _run = Run;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Action Create(Action continuation) {
      if (!_pool.TryDequeue(out var item)) {
        item = new PooledTweenCallback();
      }

      item._continuation = continuation;
      return item._run;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Run() {
      var call = _continuation;
      _continuation = null;
      if (call != null) {
        _pool.Enqueue(this);
        call.Invoke();
      }
    }
  }
}

#endif