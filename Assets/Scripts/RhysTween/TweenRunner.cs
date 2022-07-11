using UnityEngine;
using Leopotam.EcsLite;
using System;

namespace RhysTween {
  public struct Tween {
    readonly internal EcsPackedEntity _entity;

    public Tween(EcsPackedEntity entity) {
      _entity = entity;
    }
  }

  internal class TweenRunner : MonoBehaviour {
    EcsWorld _world;
    EcsSystems _systems;
    EcsFilter _timeFilter;

#region Singleton

    static TweenRunner _instance;

    public static TweenRunner Instance {
      get {
        if (_instance == null) {
          var go = new GameObject("TweenRunner");
          _instance = go.AddComponent<TweenRunner>();
          DontDestroyOnLoad(_instance);
        }
        return _instance;
      }
    }

#endregion
#region Tweens

  public Tween CreateTween<T>(T from, Action<T> onChange, T to, float duration) {
    var entity = _world.NewEntity();
    _world.AddComponent(entity, new TweenConfig<T>(from, to, onChange));
    _world.AddComponent(entity, new TweenState(duration));
    return new Tween(_world.PackEntity(entity));
  }

  public void SetOnComplete(Tween tween, Action onComplete) {
    if (Entity(tween, out var entity)) {
      ref var c = ref _world.EnsureComponent<OnComplete>(entity);
      c.Callback = onComplete;
    }
  }

  public void SetLooping(Tween tween, int remaining = -1) {
    if (Entity(tween, out var entity)) {
      ref var loop = ref _world.EnsureComponent<Loop>(entity);
      loop.Remaining = remaining;
    }
  }

#endregion
#region Internal

    internal ref Time GetTime() =>
      ref _world.GetPool<Time>().Get(_timeFilter.First());

#endregion
#region Private

    ChangeSystem<T> CreateChangeSystem<T>(Lerp<T> lerp) {
      var filter = _world.Filter<TweenConfig<T>>().End();
      return CreateChangeSystem(filter, lerp);
    }

    ChangeSystem<T> CreateChangeSystem<T>(EcsFilter filter, Lerp<T> lerp) =>
      new (filter, lerp);

    bool Entity(Tween tween, out int entity) {
      if (!tween._entity.Unpack(_world, out entity)) {
        Debug.LogWarning($"Tween is no longer active");
        return false;
      }
      return true;
    }

#endregion
#region Unity lifecycle
#pragma warning disable IDE0051

    void Awake() {
      _world = new ();
      _systems = new (_world, this);
      _systems
        .Add(new ProgressSystem())
        .Add(CreateChangeSystem<float>(Mathf.Lerp))
        .Add(CreateChangeSystem<Vector2>(Vector2.Lerp))
        .Add(CreateChangeSystem<Vector3>(Vector3.Lerp))
        .Add(CreateChangeSystem<Quaternion>(Quaternion.Lerp))
        .Add(new LoopSystem())
        .Add(new CompleteSystem())
        .Init();

      _timeFilter = _world.Filter<Time>().End();

      var timeEntity = _world.NewEntity();
      _world.AddComponent<Time>(timeEntity);
    }

    void Update() {
      ref var time = ref GetTime();
      time.DeltaTime = UnityEngine.Time.deltaTime;
      _systems.Run();
    }

    void OnDestroy() {
      _world.Destroy();
    }

#pragma warning restore IDE0051
#endregion
  }
}