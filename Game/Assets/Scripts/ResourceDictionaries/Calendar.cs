using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu]
public class Calendar : ScriptableObject {
  public Sprite[] daySprites;
  public Sprite winSprite;

  public Sprite GetSprite(GameDay day) {
    var dayInt = (int) day.value;
    if (dayInt < daySprites.Length)
      return daySprites[dayInt];

    Debug.LogError("Unsupported day " + day.value);
    return null;
  }
}
}