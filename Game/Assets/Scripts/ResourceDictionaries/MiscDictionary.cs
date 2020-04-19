using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu(fileName = "Dictionaries", menuName = "Misc", order = 0)]
public class MiscDictionary : ScriptableObject {
  public Sprite warningIcon;
  public Sprite dangerIcon;
  public GameObject signPrefab;

  public void SetupView(GameEntity e) {
    var signGO = Instantiate(signPrefab);
    var sr = signGO.GetComponent<SpriteRenderer>();
    sr.sprite = warningIcon; 
    e.ReplaceDangerSource(signGO);
  }
}
}