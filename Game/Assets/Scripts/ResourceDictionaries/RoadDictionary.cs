using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu]
public class RoadDictionary : ScriptableObject {
  public GameObject roadPrefab;

  public void SetupView(GameEntity entity, int type,  int lane, int distance) {
    var roadTile = Instantiate(roadPrefab);
    roadTile.transform.position = new Vector3(lane, distance, 10);
    entity.AddView(roadTile);
  }
}
}