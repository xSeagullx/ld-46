using Entitas;
using Entitas.CodeGeneration.Attributes;

[Global, Unique]
public class LastSlept : IComponent {
  public float time;
}

