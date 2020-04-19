using System;
using System.Collections.Generic;
using Entitas;

public class ResourceRenderSystem : ReactiveSystem<GlobalEntity> {
  private readonly Action<Resources> _updateUi;

  public ResourceRenderSystem(Contexts contexts, Action<Resources> updateUI) : base(contexts.global) {
    _updateUi = updateUI;
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.Resources);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasResources;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    _updateUi(entities.SingleEntity().resources);
  }
}