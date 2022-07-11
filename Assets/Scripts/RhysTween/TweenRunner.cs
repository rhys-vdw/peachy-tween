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

  public Tween CreateTween<T>(TweenState<T> state) {
    var entity = _world.NewEntity();
    _world.AddComponent(entity, state);
    return new Tween(_world.PackEntity(entity));
  }

  public void SetOnChange<T>(Tween tween, Action<T> onChange) {
    if (Entity(tween, out var entity)) {
      ref var c = ref _world.EnsureComponent<OnChange<T>>(entity);
      c.Callback = onChange;
    }
  }

  public void SetOnComplete(Tween tween, Action onComplete) {
    if (Entity(tween, out var entity)) {
      ref var c = ref _world.EnsureComponent<OnComplete>(entity);
      c.Callback = onComplete;
    }
  }

#endregion
#region Private

    ProgressSystem<T> CreateProgressSystem<T>(Lerp<T> lerp) {
      var filter = _world.Filter<TweenState<T>>().End();
      return CreateProgressSystem(filter, lerp);
    }

    ProgressSystem<T> CreateProgressSystem<T>(EcsFilter filter, Lerp<T> lerp) =>
      new (filter, lerp);

  bool Entity(Tween tween, out int entity) {
    if (!tween._entity.Unpack(_world, out entity)) {
      Debug.LogWarning($"Tween is no longer active");
      return false;
    }
    return true;
  }

#endregion
#pragma warning disable IDE0051

    void Awake() {
      _world = new ();
      _systems = new (_world, this);
      _systems
        .Add(CreateProgressSystem<float>(Mathf.Lerp))
        .Add(CreateProgressSystem<Vector2>(Vector2.Lerp))
        .Add(CreateProgressSystem<Vector3>(Vector3.Lerp))
        .Add(CreateProgressSystem<Quaternion>(Quaternion.Lerp))
        .Add(new CompleteSystem())
        .Init();

      _timeFilter = _world.Filter<Time>().End();

      var timeEntity = _world.NewEntity();
      _world.AddComponent<Time>(timeEntity);
    }

    void Update() {
      ref var time = ref GetTime();
      time.Current = UnityEngine.Time.timeAsDouble;
      _systems.Run();
    }

    void OnDestroy() {
      _world.Destroy();
    }

#pragma warning restore IDE0051

    internal ref Time GetTime() =>
      ref _world.GetPool<Time>().Get(_timeFilter.First());
  }
}