using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;


public class CarSpawningSystem : IExecuteSystem, IInitializeSystem {
  private enum Direction {
    UP, DOWN
  }

  private class LaneDefinition {
    public int lineNo;
    public Direction direction;
    public float nextSpawnTime;
  }

  private readonly Contexts _contexts;
  private readonly Action<GameEntity> _setupView;
  private List<LaneDefinition> lanesToSpawn;
  private IGroup<GameEntity> _players;

  public CarSpawningSystem(Contexts contexts, Action<GameEntity> setupView) {
    _contexts = contexts;
    _setupView = setupView;
    _players = _contexts.game.GetGroup(GameMatcher.Player);
  }

  public void Execute() {
    foreach (var laneDefinition in lanesToSpawn) {
      if (Time.time < laneDefinition.nextSpawnTime)
        continue;

      laneDefinition.nextSpawnTime = Time.time + Random.Range(2f, 5f);
      var lane = laneDefinition.lineNo;
      int signum = laneDefinition.direction == Direction.DOWN ? 1 : -1;
      var e = _contexts.game.CreateEntity();
      e.AddEnemy("car");
      e.AddLane(lane, lane + 1);
      e.AddRoadPosition(_players.GetSingleEntity().roadPosition.distanceFromStart + 40 * signum);
      e.AddVelocity(-15 * signum);
      e.AddTTL(10);
      e.AddDangerSource(null);
      _setupView(e);
    }
  }

  public void Initialize() {
    lanesToSpawn = new List<LaneDefinition>();
    var roadPattern = _contexts.game.road.pattern;
    for (var i = 0; i < roadPattern.Length; i++) {
      if (roadPattern[i] == "road" && roadPattern[i + 1] == "road") {
        var laneDefinition = new LaneDefinition();
        laneDefinition.direction = i < roadPattern.Length / 2 ? Direction.UP : Direction.DOWN;
        laneDefinition.lineNo = i;
        laneDefinition.nextSpawnTime = Time.time + Random.Range(2f, 5f);
        lanesToSpawn.Add(laneDefinition);
        i++;
      }
    }
  }
}