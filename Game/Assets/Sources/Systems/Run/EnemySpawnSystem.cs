using System;
using System.Collections.Generic;
using Entitas;
using Random = UnityEngine.Random;

public class EnemySpawnSystem : IInitializeSystem {
  private readonly Contexts _contexts;
  private readonly Action<GameEntity> _configureView;

  public EnemySpawnSystem(Contexts contexts, Action<GameEntity> configureView) {
    _contexts = contexts;
    _configureView = configureView;
  }

  public void Initialize() {
    var numLanes = _contexts.game.road.numLanes;
    var pattern = _contexts.game.road.pattern;
    var numEnemies = (int) (_contexts.game.runDescription.distanceMeters / 20f * numLanes * 4);

    var lanes = new List<int>();
    for (var i = 0; i < pattern.Length; i++) {
      if (pattern[i] == "pavement") {
        lanes.Add(i); 
      }
    }

    int distance = 10;
    for (var i = 0; i < numEnemies; i++) {
      Shuffle(lanes);
      int laneI = 0; // Index in shuffled lane
      for (var i1 = 0; i1 < Random.Range(0, 4); i1++) {
        var e = _contexts.game.CreateEntity();
        var type = "human";
        e.AddEnemy(type);
        e.AddRoadPosition(distance);
        e.AddLane(lanes[laneI], lanes[laneI]);
        _configureView(e);
        distance += Random.Range(3, 5);
        laneI++;
      }
    }
  }
  
  private static void Shuffle(List<int> lanes) {
    int n = lanes.Count;
    while (n > 1) {
      int k = Random.Range(0, n--);
      int temp = lanes[n];
      lanes[n] = lanes[k];
      lanes[k] = temp;
    }
  }
}