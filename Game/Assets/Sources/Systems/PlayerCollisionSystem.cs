using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Sources.Systems {
public class PlayerCollisionSystem : ReactiveSystem<GameEntity>, ICleanupSystem {
  private readonly Contexts _contexts;
  private IGroup<GameEntity> _collisions;
  private IGroup<GameEntity> _players;

  public PlayerCollisionSystem(Contexts contexts) : base(contexts.game) {
    _contexts = contexts;
    _collisions = contexts.game.GetGroup(GameMatcher.PlayerCollision);
    _players = contexts.game.GetGroup(GameMatcher.Player);
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.PlayerCollision);
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasPlayerCollision;
  }

  protected override void Execute(List<GameEntity> entities) {
    var player = _players.GetSingleEntity();
    
    foreach (var gameEntity in entities) {
      if (gameEntity.playerCollision.target.hasEnemy) {
        CollidedWithTheEnemy(player, gameEntity.playerCollision.target);
      }
    }
  }

  public void Cleanup() {
    foreach (var gameEntity in _collisions.GetEntities()) {
      gameEntity.Destroy();
    }
  }
  
  private void CollidedWithTheEnemy(GameEntity player, GameEntity enemy) {
    var pattern = _contexts.game.road.pattern;
    var playerLane = player.lane.lane;
    var closestPavement = player.lane.lane;
    for (int i = 0; i < Math.Min(closestPavement, pattern.Length - 1 - closestPavement); i++) {
      if (pattern[playerLane + i] == "pavement") {
        closestPavement = playerLane + i;
        break;
      }
      if (pattern[playerLane - i] == "pavement") {
        closestPavement = playerLane - i;
        break;
      }
    }

    player.AddHitRecoveryState(closestPavement, Time.time);
  }
}
}