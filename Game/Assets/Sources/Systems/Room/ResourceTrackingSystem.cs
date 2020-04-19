using System.Collections.Generic;
using Entitas;
using TMPro.EditorUtilities;
using UnityEngine;

public class ResourceTrackingSystem : ReactiveSystem<GlobalEntity>, ICleanupSystem {
  private IGroup<GlobalEntity> _modifications;
  private GlobalContext _globalContext;

  public ResourceTrackingSystem(Contexts contexts) : base(contexts.global) {
    _globalContext = contexts.global;
    _modifications = _globalContext.GetGroup(GlobalMatcher.ResourceModification);
  }

  protected override ICollector<GlobalEntity> GetTrigger(IContext<GlobalEntity> context) {
    return context.CreateCollector(GlobalMatcher.ResourceModification);
  }

  protected override bool Filter(GlobalEntity entity) {
    return entity.hasResourceModification;
  }

  protected override void Execute(List<GlobalEntity> entities) {
    var resources = _globalContext.resources;
    var panicCount = resources.panicCount;
    var money = resources.money;
    var mealCount = resources.mealCount;
    var medsCount = resources.medsCount;
    var toiletCount = resources.toiletCount;

    foreach (var entity in entities) {
      var modification = entity.resourceModification;
      switch (modification.resourceType) {
        case "panic":
          panicCount += modification.change;
          break;
        case "money":
          money += modification.change;
          break;
        case "food":
          mealCount += modification.change;
          break;
        case "meds":
          medsCount += modification.change;
          break;
        case "toilet":
          toiletCount += modification.change;
          break;
        default:
          Debug.LogError("Unsupported modification type " + modification.resourceType);
          break;
      }
    }

    _globalContext.ReplaceResources(newPanicCount: panicCount, newMealCount: mealCount, newMedsCount: medsCount,
      newToiletCount: toiletCount, newMoney: money, newMaxPanic: resources.maxPanic);
  }

  public void Cleanup() {
    foreach (var entity in _modifications.GetEntities()) {
      entity.Destroy();
    }
  }
}