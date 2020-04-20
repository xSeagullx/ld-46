using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class Road : IComponent {
  public int numLanes;
  public string[] pattern;
  public string[] visualPattern;
}

