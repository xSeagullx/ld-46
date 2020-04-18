using Entitas;
using Sources;

public class EnemySpawnSystem : IInitializeSystem {
  private readonly Contexts _contexts;

  public EnemySpawnSystem(Contexts contexts) {
    _contexts = contexts;
  }

  public void Initialize() {
    //var numEnemies = _contexts.game.runDescription.distanceMeters / 50;
    
    
  }
}