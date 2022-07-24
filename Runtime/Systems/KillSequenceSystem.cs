using Leopotam.EcsLite;

namespace PeachyTween {
  internal class KillSequenceSystem : IEcsSystem, IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    EcsFilter _memberFilter;

    public void PreInit(EcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Sequencer>().Inc<Active>().Inc<Kill>().End();
      _memberFilter = _world.Filter<SequenceMember>().End();
    }

    public void Run(EcsSystems systems) {
      foreach (var sequencerEntity in _filter) {
        foreach (var memberEntity in _memberFilter) {
          ref var member = ref _world.GetComponent<SequenceMember>(memberEntity);
          _world.EnsureComponent<Active>(memberEntity);
          _world.AddComponent<Kill>(memberEntity);
        }
      }
    }
  }
}
