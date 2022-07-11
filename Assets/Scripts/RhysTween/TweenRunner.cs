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
    EcsSystems _updateSystems;
    EcsSystems _fixedUpdateSystems;
    EcsSystems _manualUpdateSystems;
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

  public void SetUpdate<TUpdate>(Tween tween) where TUpdate : struct {
    if (Entity(tween, out var entity)) {
      _world.DelComponent<Update>(entity);
      _world.DelComponent<FixedUpdate>(entity);
      _world.DelComponent<ManualUpdate>(entity);
      _world.AddComponent<TUpdate>(entity);
    }
  }

#endregion
#region Control

    public void ManualUpdate(float deltaTime) {
      ref var time = ref GetTime();
      time.DeltaTime = deltaTime;
      _manualUpdateSystems.Run();
    }

#endregion
#region Internal

    internal ref Time GetTime() =>
      ref _world.GetPool<Time>().Get(_timeFilter.First());

#endregion
#region Private

    static ChangeSystem<TValue> CreateChangeSystem<TValue, TUpdate>(
      EcsWorld world,
      Lerp<TValue> lerp
    ) where TUpdate : struct {
      var filter = world.Filter<TweenConfig<TValue>>().Inc<TUpdate>().End();
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

    EcsSystems CreateSystems<TUpdate>(EcsWorld world) where TUpdate : struct{
      var systems = new EcsSystems(world, this);
      systems
        .Add(new ProgressSystem<TUpdate>())
        .Add(CreateChangeSystem<float, TUpdate>(world, Mathf.LerpUnclamped))
        .Add(CreateChangeSystem<Vector2, TUpdate>(world, Vector2.LerpUnclamped))
        .Add(CreateChangeSystem<Vector3, TUpdate>(world, Vector3.LerpUnclamped))
        .Add(CreateChangeSystem<Quaternion, TUpdate>(world, Quaternion.LerpUnclamped))
        .Add(new LoopSystem())
        .Add(new CompleteSystem())
        .Init();
      return systems;
    }

    void Awake() {
      _world = new ();
      _updateSystems = CreateSystems<Update>(_world);
      _fixedUpdateSystems = CreateSystems<FixedUpdate>(_world);
      _manualUpdateSystems = CreateSystems<ManualUpdate>(_world);

      _timeFilter = _world.Filter<Time>().End();

      var timeEntity = _world.NewEntity();
      _world.AddComponent<Time>(timeEntity);
    }

    void Update() {
      ref var time = ref GetTime();
      time.DeltaTime = UnityEngine.Time.deltaTime;
      _updateSystems.Run();
    }

    void FixedUpdate() {
      ref var time = ref GetTime();
      time.DeltaTime = UnityEngine.Time.deltaTime;
      _fixedUpdateSystems.Run();
    }

    void OnDestroy() {
      _world.Destroy();
    }

#pragma warning restore IDE0051
#endregion
  }
}