using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class RunDescription: IComponent {
  public string target;
  public int distanceMeters;
}
