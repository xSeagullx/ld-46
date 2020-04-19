using Entitas;
using Entitas.CodeGeneration.Attributes;

[Global, Unique]
public class GameState : IComponent {
  public string state;
}

