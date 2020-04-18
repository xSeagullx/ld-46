using Entitas;
using UnityEngine;

public class RunSystem : IExecuteSystem {
  private IGroup<GameEntity> _players;

  public RunSystem(Contexts contexts) {
    _players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.RoadPosition));
  }

  public void Execute() {
    foreach (var player in _players) {
      player.ReplaceRoadPosition(player.roadPosition.distanceFromStart + 1 * Time.deltaTime);
    }
  }
}