using System;
using Leopotam.EcsLite;

namespace RhysTween {
  internal static class EcsUtility {
    public static bool HasComponent<T>(this EcsWorld world, int entity) where T : struct =>
      world.GetPool<T>().Has(entity);

    public static void DelComponent<T>(this EcsWorld world, int entity) where T : struct =>
      world.GetPool<T>().Del(entity);

    public static void DelComponent(this EcsWorld world, Type type, int entity) =>
      world.GetPoolByType(type).Del(entity);

    public static ref T GetComponent<T>(this EcsWorld world, int entity) where T : struct =>
      ref world.GetPool<T>().Get(entity);

    public static bool TryGetComponent<T>(this EcsWorld world, int entity, out T component) where T : struct {
      var pool = world.GetPool<T>();
      if (pool.Has(entity)) {
        component = pool.Get(entity);
        return true;
      }
      component = default;
      return false;
    }

    public static ref T AddComponent<T>(this EcsWorld world, int entity) where T : struct =>
      ref world.GetPool<T>().Add(entity);

    public static ref T AddComponent<T>(this EcsWorld world, int entity, in T component) where T : struct {
      ref var c = ref AddComponent<T>(world, entity);
      c = component;
      return ref c;
    }

    public static ref T EnsureComponent<T>(this EcsWorld world, int entity) where T : struct {
      var pool = world.GetPool<T>();
      return ref pool.Has(entity)
        ? ref pool.Get(entity)
        : ref pool.Add(entity);
    }

    public static int First(this EcsFilter filter) {
      if (TryGetFirst(filter, out var entity)) {
        return entity;
      }
      throw new System.ArgumentException($"Filter is empty");
    }

    public static bool TryGetFirst(this EcsFilter filter, out int entity) {
      foreach (var e in filter) {
        entity = e;
        return true;
      }
      entity = -1;
      return false;
    }

    public static void KillTween(this EcsWorld world, int entity) {
      if (world.TryGetComponent<OnKill>(entity, out var onKill)) {
        onKill.Callback();
      }
      world.DelEntity(entity);
    }
  }
}