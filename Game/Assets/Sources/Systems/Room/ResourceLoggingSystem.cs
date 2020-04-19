using System.Collections.Generic;
using System.Globalization;
using Entitas;
using Sources.Utils;

namespace Sources.Systems.Room {
public class ResourceLoggingSystem : ReactiveSystem<GlobalEntity> {
  private GlobalContext _globalContext;

  public ResourceLoggingSystem(Contexts contexts) : base(contexts.global) {
    _globalContext = contexts.global;
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.ResourceModification);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasResourceModification;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    foreach (var entity in entities) {
      var e = _globalContext.CreateEntity();
      var modification = entity.resourceModification;
      var isGood = entity.resourceModification.change > 0;
      isGood = modification.resourceType == "panic" ? !isGood : isGood;
      if (isGood) {
        e.AddMessageLog(StringUtils.Capitalise(modification.resourceType) + " (<color=green>" + modification.change + "</color>)");
      }
      else {
        e.AddMessageLog(StringUtils.Capitalise(modification.resourceType) + " (<color=red>" + modification.change + "</color>)");
      }
    }
  }
}
}