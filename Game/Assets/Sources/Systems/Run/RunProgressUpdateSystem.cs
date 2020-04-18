using System;
using Entitas;

public class RunProgressUpdateSystem : IExecuteSystem {
  private readonly Action<float> _updateAction;
  private IGroup<GameEntity> _players;
  private Contexts _contexts;

  public RunProgressUpdateSystem(Contexts contexts, Action<float> updateAction) {
    _updateAction = updateAction;
    _contexts = contexts;
    _players = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.RoadPosition));
  }

  public void Execute() {
    var progress = _players.GetSingleEntity().roadPosition.distanceFromStart / _contexts.game.runDescription.distanceMeters;
    _updateAction(progress);
  }
}