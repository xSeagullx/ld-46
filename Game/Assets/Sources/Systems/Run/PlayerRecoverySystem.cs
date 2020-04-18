using Entitas;
using UnityEngine;

public class PlayerRecoverySystem : IExecuteSystem {
  private readonly Contexts _contexts;
  private IGroup<GameEntity> _toRecovery;

  public PlayerRecoverySystem(Contexts contexts) {
    _contexts = contexts;
    _toRecovery = _contexts.game.GetGroup(GameMatcher.HitRecoveryState);
  }

  public void Execute() {
    foreach (var entity in _toRecovery.GetEntities()) {
      var gameObject = entity.view.gameObject;
      var timeSinceCollision = Time.time - entity.hitRecoveryState.collisionTime;
      var recoveryTime = 0.3f;
      if (timeSinceCollision < recoveryTime) {
        gameObject.GetComponent<Collider2D>().enabled = false;
        var elapsed = timeSinceCollision / recoveryTime;
        var oldPos = new Vector3(entity.lane.lane, entity.roadPosition.distanceFromStart, 0);
        var newPos = new Vector3(entity.hitRecoveryState.targetLane, entity.roadPosition.distanceFromStart, 0);
        gameObject.transform.position = Vector3.Lerp(oldPos, newPos, elapsed);
        var oldScale = new Vector3(1, 1, 1);
        var newScale = new Vector3(2, 2, 1);
        if (timeSinceCollision < recoveryTime / 2) {
          gameObject.transform.localScale = Vector3.Lerp(oldScale, newScale, elapsed);
        }
        else {
          gameObject.transform.localScale = Vector3.Lerp(newScale, oldScale, elapsed);
        }
        
      }
      else {
        entity.ReplaceLane(entity.hitRecoveryState.targetLane, entity.hitRecoveryState.targetLane);
        gameObject.GetComponent<Collider2D>().enabled = true;
        entity.RemoveHitRecoveryState();
      }
    }
  }
}