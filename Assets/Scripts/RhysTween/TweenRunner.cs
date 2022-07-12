using UnityEngine;
using Leopotam.EcsLite;
using System;
using System.Collections.Generic;

namespace RhysTween {
  internal class RunState {
    public float DeltaTime;
    public EcsFilter GroupFilter;
  }

  public struct Tween {
    readonly internal EcsPackedEntity _entity;

    public Tween(EcsPackedEntity entity) {
      _entity = entity;
    }
  }

  internal class TweenRunner : MonoBehaviour {
    EcsWorld _world;
    EcsSystems _systems;
    readonly RunState _runState = new ();

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
    _world.AddComponent<Update>(entity);
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

  public void SetGroup<TUpdate>(Tween tween) where TUpdate : struct {
    if (Entity(tween, out var entity)) {
      _world.DelComponent<Update>(entity);
      _world.DelComponent<FixedUpdate>(entity);
      _world.DelComponent<ManualUpdate>(entity);
      _world.AddComponent<TUpdate>(entity);
    }
  }

  public bool IsPaused(Tween tween) =>
    Entity(tween, out var entity) && IsPaused(entity);

  public void Pause(Tween tween) {
    if (
      Entity(tween, out var entity) &&
      !IsPaused(entity)
    ) {
      _world.AddComponent<Paused>(entity);
    }
  }

  public void Resume(Tween tween) {
    if (Entity(tween, out var entity)) {
      _world.DelComponent<Paused>(entity);
    }
  }

#endregion
#region Run

    readonly Dictionary<Type, EcsFilter> _groupFilters = new();

    EcsFilter GroupFilter<TGroup>() where TGroup : struct {
      if (!_groupFilters.TryGetValue(typeof(TGroup), out var filter)) {
        filter = _world.Filter<TGroup>().End();
        _groupFilters.Add(typeof(TGroup), filter);
      }
      return filter;
    }

    public void ManualUpdate(float deltaTime) => Run<ManualUpdate>(deltaTime);

    public void Run<TGroup>(float deltaTime) where TGroup : struct{
      _runState.DeltaTime = deltaTime;
      _runState.GroupFilter = GroupFilter<TGroup>();
      _systems.Run();
    }

#endregion
#region Private

    bool IsPaused(int entity) => _world.HasComponent<Paused>(entity);

    ChangeSystem<TValue> CreateChangeSystem<TValue>(Lerp<TValue> lerp) {
      var filter = _world.Filter<TweenConfig<TValue>>().Inc<Active>().Exc<Paused>().End();
      return CreateChangeSystem(filter, lerp);
    }

    static ChangeSystem<T> CreateChangeSystem<T>(EcsFilter filter, Lerp<T> lerp) =>
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
      _systems = new EcsSystems(_world, _runState)
        .Add(new ActivateGroupSystem())
        .Add(new ProgressSystem())
        .Add(CreateChangeSystem<float>(Mathf.LerpUnclamped))
        .Add(CreateChangeSystem<Vector2>(Vector2.LerpUnclamped))
        .Add(CreateChangeSystem<Vector3>(Vector3.LerpUnclamped))
        .Add(CreateChangeSystem<Quaternion>(Quaternion.LerpUnclamped))
        .Add(new DeactivateGroupSystem())
        .Add(new LoopSystem())
        .Add(new CompleteSystem());
      _systems.Init();
    }

    void Update() => Run<Update>(Time.deltaTime);

    void LateUpdate() => Run<LateUpdate>(Time.deltaTime);

    void FixedUpdate() => Run<FixedUpdate>(Time.deltaTime);

    void OnDestroy() => _world.Destroy();

#pragma warning restore IDE0051
#endregion
  }
}