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
    int lanes = 4;
    var tileTypes = new int[runDescription.distanceMeters * lanes];

    for (int distance = 0; distance < runDescription.distanceMeters; distance++) {
      for (int lane = 0; lane < lanes; lane++) {
        var tileType = Random.Range(0, 5);
        tileTypes[distance * lane] = tileType;
        var roadEntity = _contexts.game.CreateEntity();
        roadEntity.AddLane(lane);
        _roadDictionary.SetupView(roadEntity, tileType, lane, distance);
      }
    }

    _contexts.game.ReplaceRoad(lanes);
    _contexts.game.ReplaceLevel(tileTypes);
  }
}