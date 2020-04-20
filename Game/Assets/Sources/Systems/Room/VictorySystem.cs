using System.Collections.Generic;
using Entitas;

public class VictorySystem : ReactiveSystem<GlobalEntity> {
  private readonly Contexts _contexts;

  public VictorySystem(Contexts contexts) : base(contexts.global) {
    _contexts = contexts;
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.GameDay);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasGameDay;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    var day = entities.SingleEntity();
    if (day.gameDay.value > 7) {
      _contexts.global.ReplaceGameState("win");
    }
  }
}