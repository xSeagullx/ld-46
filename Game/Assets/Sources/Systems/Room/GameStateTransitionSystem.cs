using System;
using System.Collections.Generic;
using Entitas;

public class GameStateTransitionSystem : ReactiveSystem<GlobalEntity> {
  private readonly Action<string> _transitionAction;

  public GameStateTransitionSystem(Contexts contexts, Action<string> transitionAction) : base(contexts.global) {
    _transitionAction = transitionAction;
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.GameState);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasGameState;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    var state = entities.SingleEntity().gameState;
    _transitionAction(state.state);
  }
}