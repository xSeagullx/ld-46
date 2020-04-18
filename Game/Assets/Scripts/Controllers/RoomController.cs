using System;
using Entitas;
using ResourceDictionaries;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
public class RoomController : MonoBehaviour {
  public ContextHolder contextHolder;
  private Scene runScene;

  Systems _systems = new Systems();

  private void Start() {
    runScene = SceneManager.GetSceneByName("Run");
    var roomSupply = contextHolder.contexts.game.CreateEntity();
    
    roomSupply.AddFood(6);
    roomSupply.AddMoney(100);
    roomSupply.AddPanic(30);

    roomSupply.AddGameDay(0);
    //_systems.Add();
  }

  private void OnGUI() {
    if (GUI.Button(new Rect(), "Go for food")) {
      contextHolder.contexts.game.SetRunDescription("food", 1000);
      SceneManager.SetActiveScene(runScene);
    }
    
  }
}
}