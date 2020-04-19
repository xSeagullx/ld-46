using Entitas;
using UnityEngine;

public class RoadCullingSystem : IExecuteSystem {
  private readonly Camera _camera;
  private IGroup<GameEntity> _views;
  private IGroup<GameEntity> _players;

  public RoadCullingSystem(Contexts contexts, Camera camera) {
    _camera = camera;
    _views = contexts.game.GetGroup(GameMatcher.View);
    _players = contexts.game.GetGroup(GameMatcher.Player);
  }

  public void Execute() {
    var distanceFromStart = _players.GetSingleEntity().roadPosition.distanceFromStart;
    // visibleTilesCount is 22
    var limitBottom = distanceFromStart - 2 - 2;
    var limitTop = distanceFromStart + 20 + 2;

    foreach (var view in _views) {
      if (view.view.gameObject.transform.position.y < limitBottom || view.view.gameObject.transform.position.y > limitTop)
        view.view.gameObject.SetActive(false);
      else
        view.view.gameObject.SetActive(true);
    }
  }
}