using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Global]
public class Resources : IComponent {
  public int foodCount;
  public int medsCount;
  public int toiletCount;
  public int money;
  public int panicCount;
  public int maxPanic;
}

