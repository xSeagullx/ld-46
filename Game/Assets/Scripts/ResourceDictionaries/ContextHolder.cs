using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu]
public class ContextHolder : ScriptableObject {
  public Contexts contexts = new Contexts();

  private void OnEnable() {
    Contexts.sharedInstance = contexts;
  }
}
}