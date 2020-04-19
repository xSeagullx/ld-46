using System;
using System.Collections;
using Entitas;
using ResourceDictionaries;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
public class RoomController : MonoBehaviour {
  public ContextHolder contextHolder;
  public SceneAsset runScene;
  public UIRoomResourcesDisplay uiResources;

  private Systems _systems;

  private void Start() {
    contextHolder.roomController = this;
    var contexts = contextHolder.contexts;
    _systems = contextHolder.roomSystems;

#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
    new Entitas.VisualDebugging.Unity.ContextObserver(contexts.global);
#endif

    if (_systems == null) {
      _systems = new Systems();
      contextHolder.roomSystems = _systems;
      _systems
        .Add(new ResourceTrackingSystem(contexts))
        .Add(new ResourceRenderSystem(contexts, resources => contextHolder.roomController.uiResources.UpdateResources(resources)))
        .Add(new CalendarUpdatingSystem(contexts, day => contextHolder.roomController.uiResources.UpdateDay(day)))
        .Add(new VictorySystem(contexts))
        .Add(new GameStateTransitionSystem(contexts, state => contextHolder.roomController.TransitionStates(state)))
        ;
    }

    if (contexts.global.count == 0) {
      contexts.global.SetGameState("start");
      contexts.global.SetResources(6, 4, 2, 100, 30, 300);
      contexts.global.SetGameDay(1);
    }

    _systems.Initialize();
  }

  private void Update() {
    _systems.Execute();
    _systems.Cleanup();
  }

  private void TransitionStates(string state) {
    switch (state) {
      case "win": break; // Load win scene 
      case "panic": break; // Load panic death scene
      case "run": break; // load run scene
    }
  }

  private void OnGUI() {
    if (GUI.Button(new Rect(100, 100, 32, 32 * 5), "Go for food")) {
      contextHolder.contexts.game.SetRunDescription("food", 100);
      StartCoroutine(LoadRunScene());
    }
    if (GUI.Button(new Rect(100, 150, 32, 32 * 5), "Add food")) {
      var e = contextHolder.contexts.global.CreateEntity();
      e.AddResourceModification("food", 1);
    }
  }

  private IEnumerator LoadRunScene() {
    Debug.Log("Loading scene " + runScene.name + " " + runScene.name);
    SceneManager.LoadScene(runScene.name, LoadSceneMode.Single);
    
    yield return null;
    Debug.Log("Setting active scene " + runScene.name + " " + runScene.name);
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(runScene.name));
  }
}
}