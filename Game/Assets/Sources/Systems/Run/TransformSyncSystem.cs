using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TransformSyncSystem : ReactiveSystem<GameEntity> {
  public TransformSyncSystem(Contexts contexts) : base(contexts.game) {
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Lane, GameMatcher.View));
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasLane && entity.hasView;
  }

  protected override void Execute(List<GameEntity> entities) {
    foreach (var entity in entities) {
      entity.view.gameObject.transform.position = new Vector3(entity.lane.lane, 0, 0);
    }
  }
}