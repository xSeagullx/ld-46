using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

public class ResourceChange {
  public readonly string type;
  public readonly int amountMin;
  public readonly int amountMax;

  public ResourceChange(string type, int amountMin, int amountMax) {
    this.type = type;
    this.amountMin = amountMin;
    this.amountMax = amountMax;
  }

  public ResourceChange(string type, int amount) {
    this.type = type;
    this.amountMin = amount;
    this.amountMax = amount;
  }
}
public class Reward {
  public readonly  string message;
  public readonly float probabilityPercent; //0-1
  public readonly ResourceChange[] _changes;

  public Reward(string message, float probabilityPercent, params ResourceChange[] changes) {
    this.probabilityPercent = probabilityPercent;
    this.message = message;
    this._changes = changes;
  }
}

[Unique]
public class RunReward : IComponent {
  public Reward[] rewards;
}

