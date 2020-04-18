using System;
using Entitas;
using Sources;
using Random = UnityEngine.Random;

public class RoadCreationSystem : IInitializeSystem {
  private readonly Contexts _contexts;

  public RoadCreationSystem(Contexts contexts) {
    _contexts = contexts;
  }

  public void Initialize() {
    var runDescription = _contexts.game.runDescription;
    int lines = 4;
    var tileType = new int[runDescription.distanceMeters * lines];

    for (int i = 0; i < runDescription.distanceMeters; i++) {
      for (int line = 0; line < lines; line++) {
        tileType[i * line] = Random.Range(0, 5);
      }
    }
  }
}