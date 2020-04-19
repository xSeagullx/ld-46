using System;
using System.Collections.Generic;
using Entitas;

public class PlayerHealthSystem : ReactiveSystem<GameEntity>, IInitializeSystem {
  private readonly Contexts _contexts;
  private readonly Action<Health> _updateHealthDisplay;
  private readonly Action<string> _endRun;

  public PlayerHealthSystem(Contexts contexts, Action<Health> updateHealthDisplay, Action<string> endRun) : base(contexts.game) {
    _contexts = contexts;
    _updateHealthDisplay = updateHealthDisplay;
    _endRun = endRun;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Player, GameMatcher.Health));
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasHealth && entity.isPlayer;
  }

  protected override void Execute(List<GameEntity> entities) {
    var player = entities.SingleEntity();
    _updateHealthDisplay(player.health);
    if (player.health.current <= 0) {
      _endRun("fail:injured");
    }
  }

  public void Initialize() {
    _contexts.game.GetGroup(GameMatcher.Player).GetSingleEntity().AddHealth(3, 3);
  }
}