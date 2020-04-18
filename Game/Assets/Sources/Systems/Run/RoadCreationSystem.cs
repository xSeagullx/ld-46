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

    string[] pattern = new[] {
      "pavement", "pavement_side", "road", "road_dash", "road", "road_solid", "road_solid_f", "road", "road_dash_f",
      "road", "pavement_side_f", "pavement"
    };

    int lanes = pattern.Length;
    for (int distance = 0; distance < runDescription.distanceMeters; distance++) {
      for (int lane = 0; lane < lanes; lane++) {
        var roadEntity = _contexts.game.CreateEntity();
        roadEntity.AddLane(lane);
        _roadDictionary.SetupView(roadEntity, pattern[lane], lane, distance);
      }
    }

    _contexts.game.ReplaceRoad(lanes);
  }
}