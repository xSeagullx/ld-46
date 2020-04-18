using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

public class View : IComponent {
  [EntityIndex]
  public GameObject gameObject;
}

