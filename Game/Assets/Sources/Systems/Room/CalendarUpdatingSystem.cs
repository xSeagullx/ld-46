using System;
using System.Collections.Generic;
using Entitas;

public class CalendarUpdatingSystem : ReactiveSystem<GlobalEntity>, IInitializeSystem, ICleanupSystem {
  private readonly Contexts _contexts;
  private readonly Action<GameDay> _uiDayUpdate;

  public CalendarUpdatingSystem(Contexts contexts, Action<GameDay> uiDayUpdate) : base(contexts.global) {
    _contexts = contexts;
    _uiDayUpdate = uiDayUpdate;
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.PassingTime);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasPassingTime;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    foreach (var entity in entities) {
      float increment = entity.passingTime.value;
      var oldDayVal = _contexts.global.gameDay.value;
      var currentDayVal = oldDayVal + increment;
      _contexts.global.ReplaceGameDay(currentDayVal);

      if ((int)oldDayVal != (int)currentDayVal) {
        var e = _contexts.global.CreateEntity();
        e.AddMessageLog("<b>It's a new day!</b>");
        _uiDayUpdate(_contexts.global.gameDay);
      }
    }
    
  }

  public void Cleanup() {
    foreach (var timeEvent in _contexts.global.GetGroup(GlobalMatcher.PassingTime).GetEntities()) {
      timeEvent.Destroy();
    }
  }

  public void Initialize() {
    _uiDayUpdate(_contexts.global.gameDay);
  }
}