using Controllers;
using Entitas;
using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu]
public class ContextHolder : ScriptableObject {
  public Contexts contexts = new Contexts();
  public Systems roomSystems; // TODO this is a hack. Remove next two fields
  public RoomController roomController; // TODO this is a hack

  private void OnEnable() {
    Contexts.sharedInstance = contexts;
  }
}
}