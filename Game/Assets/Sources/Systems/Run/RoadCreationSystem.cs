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

    string[] pattern = {
      "pavement", "pavement_side", 
      "road", "road_dash",
      "road", "road_solid",
      "road_solid_f", "road", 
      "road_dash_f", "road",
      "pavement_side_f", "pavement"
    };

    int lanes = pattern.Length;
    for (int distance = 0; distance < runDescription.distanceMeters + 30; distance++) {
      for (int lane = 0; lane < lanes; lane++) {
        var roadEntity = _contexts.game.CreateEntity();
        roadEntity.AddLane(lane, lane);
        _roadDictionary.SetupView(roadEntity, pattern[lane], lane, distance);
      }
    }

    var logicalPattern = new string[pattern.Length]; 
    for (var i = 0; i < pattern.Length; i++) {
      if (pattern[i].StartsWith("pavement")) {
        logicalPattern[i] = "pavement";
      }
      else if (pattern[i].StartsWith("road")) {
        logicalPattern[i] = "road";
      }
      else {
        Debug.Log("UNKNOWN pattern " + pattern[i]);
        logicalPattern[i] = "unknown";
      }
    }

    _contexts.game.ReplaceRoad(lanes, logicalPattern);
  }
}