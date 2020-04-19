using System;
using Entitas;

public class RunEndSystem : IExecuteSystem {
  private readonly Contexts _contexts;
  private readonly Action<string> _runOverAction;
  private IGroup<GameEntity> _player;

  public RunEndSystem(Contexts contexts, Action<string> runOverAction) {
    _contexts = contexts;
    _runOverAction = runOverAction;
    _player = contexts.game.GetGroup(GameMatcher.Player);
  }

  public void Execute() {
    if (_player.GetSingleEntity().roadPosition.distanceFromStart >= _contexts.game.runDescription.distanceMeters) {
      _runOverAction("success");
    }
  }
}