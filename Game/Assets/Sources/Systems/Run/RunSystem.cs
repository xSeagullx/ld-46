using Entitas;
using UnityEngine;

public class RunSystem : IExecuteSystem {
  private IGroup<GameEntity> _movables;

  public RunSystem(Contexts contexts) {
    _movables = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Velocity, GameMatcher.RoadPosition));
  }

  public void Execute() {
    foreach (var movable in _movables) {
      movable.ReplaceRoadPosition(movable.roadPosition.distanceFromStart + movable.velocity.value * Time.deltaTime);
    }
  }
}