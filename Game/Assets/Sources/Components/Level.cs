using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class Level : IComponent {
  public int[] tiles;
}

