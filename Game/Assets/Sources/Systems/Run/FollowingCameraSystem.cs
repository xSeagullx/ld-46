using Entitas;
using UnityEngine;

public class FollowingCameraSystem : IExecuteSystem {
  private readonly Camera _camera;
  private IGroup<GameEntity> _players;

  public FollowingCameraSystem(Contexts contexts, Camera camera) {
    _camera = camera;
    _players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.RoadPosition));
  }

  public void Execute() {
    var roadPosition = _players.GetSingleEntity().roadPosition;
    _camera.transform.position = new Vector3(0, roadPosition.distanceFromStart, -10);
  }
}