using System;
using System.Collections.Generic;
using Entitas;

public class PlayerPanicSystem : ReactiveSystem<GameEntity>, IInitializeSystem {
  private readonly Contexts _contexts;
  private readonly Action<Panic> _updateDisplay;
  private readonly Action<string> _endRun;

  public PlayerPanicSystem(Contexts contexts, Action<Panic> updateDisplay, Action<string> endRun) : base(contexts.game) {
    _contexts = contexts;
    _updateDisplay = updateDisplay;
    _endRun = endRun;
  }

  protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
    return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Player, GameMatcher.Panic));
  }

  protected override bool Filter(GameEntity entity) {
    return entity.hasPanic && entity.isPlayer;
  }

  protected override void Execute(List<GameEntity> entities) {
    var player = entities.SingleEntity();
    _updateDisplay(player.panic);
    if (player.panic.current <= 0) {
      _endRun("fail:panic");
    }
  }

  public void Initialize() {
    var player = _contexts.game.GetGroup(GameMatcher.Player).GetSingleEntity();
    player.AddPanic(5, 5);
  }
}