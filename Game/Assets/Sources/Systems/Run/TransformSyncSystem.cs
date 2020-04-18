using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TransformSyncSystem : ReactiveSystem<GameEntity> {
  public TransformSyncSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Lane,  GameMatcher.RoadPosition, GameMatcher.View).NoneOf(GameMatcher.HitRecoveryState));
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasLane && entity.hasView && entity.hasRoadPosition;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      entity.view.gameObject.transform.position = new Vector3((entity.lane.lane + entity.lane.lane2) / 2f, entity.roadPosition.distanceFromStart, 0);
    }
  }
}