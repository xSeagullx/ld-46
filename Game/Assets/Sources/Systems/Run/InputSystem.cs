using Entitas;
using Sources.Interfaces;

namespace Sources.Systems.Run {
public class InputSystem : IExecuteSystem {
  private readonly Contexts _contexts;
  private readonly InputAccessor _inputAccessor;
  private IGroup<GameEntity> _players;

  public InputSystem(Contexts contexts, InputAccessor inputAccessor) {
    _contexts = contexts;
    _inputAccessor = inputAccessor;
    _players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Lane));
  }

  public void Execute() {
    var directionalInput = _inputAccessor.GetDirectionalInput();
    if (directionalInput == DirectionalInput.NONE)
      return;

    var road = _contexts.game.road;
    
    foreach (var player in _players) {
      if (directionalInput == DirectionalInput.LEFT) {
        var newLane = player.lane.lane - 1;
        player.ReplaceLane(newLane > 0 ? newLane : 0);
      }
      if (directionalInput == DirectionalInput.RIGHT) {
        var newLane = player.lane.lane + 1;
        player.ReplaceLane(newLane <= road.numLanes ? newLane : road.numLanes);
      }
    }
  }
}
}