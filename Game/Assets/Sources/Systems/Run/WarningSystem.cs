using System;
using Entitas;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

public class WarningSystem : IExecuteSystem {
  private readonly Contexts _contexts;
  private readonly Action<GameEntity> _createWarningEntitiy;
  private IGroup<GameEntity> _dangers;
  private IGroup<GameEntity> _player;

  public WarningSystem(Contexts contexts, Action<GameEntity> createWarningEntitiy) {
    _contexts = contexts;
    _createWarningEntitiy = createWarningEntitiy;
    _dangers = _contexts.game.GetGroup(GameMatcher.DangerSource);
    _player = _contexts.game.GetGroup(GameMatcher.Player);
  }

  public void Execute() {
    var player = _player.GetSingleEntity();
    var playerPos = player.roadPosition.distanceFromStart;

    foreach (var danger in _dangers) {
      var velocity = danger.hasVelocity ? danger.velocity.value : 0;
      
      var signPosition = velocity < 0
        ? playerPos + 18
        : playerPos - 2;

      var distanceToDanger = (danger.roadPosition.distanceFromStart - signPosition);

      var mutualVelocity = velocity - player.velocity.value;
      var timeToCollision = distanceToDanger / -mutualVelocity;
      if (timeToCollision > 0) {
        // Closer than 50 and moving toward us from above. Change to time based logic
        // Distance positive, velocity negative
        if (timeToCollision < 2) { 
          if (danger.dangerSource.icon == null) {
            _createWarningEntitiy(danger);
          }
          
          var positionOffScreen = new Vector3((danger.lane.lane + danger.lane.lane2) / 2f, signPosition, 0);
          danger.dangerSource.icon.gameObject.transform.position = positionOffScreen;
        }
      }
      else {
        if (danger.dangerSource.icon != null) {
          danger.dangerSource.icon.DestroyGameObject();
          danger.ReplaceDangerSource(null);
        }
      }
    }
  }
}