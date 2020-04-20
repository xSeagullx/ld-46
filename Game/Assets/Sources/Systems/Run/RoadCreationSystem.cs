using Entitas;
using ResourceDictionaries;
using UnityEngine;

public class RoadCreationSystem : IInitializeSystem {
  private readonly Contexts _contexts;
  private readonly RoadDictionary _roadDictionary;

  public RoadCreationSystem(Contexts contexts, RoadDictionary roadDictionary) {
    _contexts = contexts;
    _roadDictionary = roadDictionary;
  }

  public void Initialize() {
    var runDescription = _contexts.game.runDescription;

    string[] pattern = _contexts.game.road.visualPattern;

    int lanes = pattern.Length;
    for (int distance = 0; distance < runDescription.distanceMeters + 30; distance++) {
      for (int lane = 0; lane < lanes; lane++) {
        var roadEntity = _contexts.game.CreateEntity();
        roadEntity.AddLane(lane, lane);
        _roadDictionary.SetupView(roadEntity, pattern[lane], lane, distance);
      }
    }
  }
}