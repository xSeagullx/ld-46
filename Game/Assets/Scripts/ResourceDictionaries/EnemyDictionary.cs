using UnityEngine;

namespace ResourceDictionaries {
[CreateAssetMenu]
public class EnemyDictionary : ScriptableObject {
  public GameObject carPrefab;
  public GameObject humanPrefab;

  public void SetupView(GameEntity e) {
    GameObject prefabToInstantiate = null;
    switch (e.enemy.type) {
      case "human": prefabToInstantiate = humanPrefab; break;
      case "car": prefabToInstantiate = carPrefab; break;
      default: 
        Debug.Log("Error instantiating enemy" + e.enemy.type);
        prefabToInstantiate = humanPrefab;
        break;
    };

    var gameObject = Instantiate(prefabToInstantiate);
    gameObject.transform.position = new Vector3(e.lane.lane, e.roadPosition.distanceFromStart, 0);
    e.AddView(gameObject);
  }
}
}