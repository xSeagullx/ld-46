using System;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawningSystem : IExecuteSystem {
  private readonly Contexts _contexts;
  private readonly Action<GameEntity> _setupView;
  private float nextSpawnTime;
  private IGroup<GameEntity> _players;

  public CarSpawningSystem(Contexts contexts, Action<GameEntity> setupView) {
    _contexts = contexts;
    _setupView = setupView;
    nextSpawnTime = Time.time;
    _players = _contexts.game.GetGroup(GameMatcher.Player);
  }

  public void Execute() {
    if (Time.time < nextSpawnTime)
      return;

    nextSpawnTime = Time.time + Random.Range(2, 5);
    var lane = 2;
    var e = _contexts.game.CreateEntity();
    e.AddEnemy("car");
    e.AddLane(lane, lane + 1);
    e.AddRoadPosition(_players.GetSingleEntity().roadPosition.distanceFromStart + 30);
    e.AddVelocity(-15);
    _setupView(e);
  }
}