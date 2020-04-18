using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu]
public class RoadDictionary : ScriptableObject {
  public GameObject roadPrefab;
  public Sprite road;
  public Sprite road_dash;
  public Sprite road_dash_f;
  public Sprite road_solid;
  public Sprite road_solid_f;
  public Sprite pavement;
  public Sprite pavement_side;
  public Sprite pavement_side_f;

  public void SetupView(GameEntity entity, string type,  int lane, int distance) {
    var roadTile = Instantiate(roadPrefab);
    roadTile.transform.position = new Vector3(lane, distance, 10);
    entity.AddView(roadTile);
    var sr = roadTile.GetComponent<SpriteRenderer>();
    sr.sprite = GetSprite(type);
  }

  private Sprite GetSprite(string type) {
    switch (type) {
      case "road": return road;
      case "road_dash": return road_dash;
      case "road_dash_f": return road_dash_f;
      case "road_solid": return road_solid;
      case "road_solid_f": return road_solid_f;
      case "pavement": return pavement;
      case "pavement_side": return pavement_side;
      case "pavement_side_f": return pavement_side_f;
      default: return road; // TODO return unknown
    }
  }
}
}