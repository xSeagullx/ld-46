using System;
using System.Collections;
using Entitas;
using ResourceDictionaries;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers {
public class RoomController : MonoBehaviour {
  public ContextHolder contextHolder;
  public SceneAsset runScene;
  public UIRoomResourcesDisplay uiResources;
  public Text eventLog;

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
        .Add(new GameEventSystem(contexts, (messages) => contextHolder.roomController.RenderEventLog(messages)))
        ;
    }

    if (contexts.global.count == 0) {
      contexts.global.SetGameState("start");
      contexts.global.SetResources(6, 4, 2, 100, 30, 300);
      contexts.global.SetGameDay(1);
      var startMessage = contexts.global.CreateEntity();
      startMessage.AddMessageLog("The government requests you to stay at home.");
    }

    _systems.Initialize();
  }

  private void RenderEventLog(string[] messages) {
    string log = "";
    foreach (var message in messages) {
      log += message + "\n";
    }

    eventLog.text = log;
  }

  private void Update() {
    if (Input.GetMouseButtonDown(0)) {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
         
      if( Physics.Raycast(ray, out hit, 100 )) {
        Debug.Log(hit.transform.gameObject.name);
      }
    }

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

  private IEnumerator LoadRunScene() {
    Debug.Log("Loading scene " + runScene.name + " " + runScene.name);
    SceneManager.LoadScene(runScene.name, LoadSceneMode.Single);
    
    yield return null;
    Debug.Log("Setting active scene " + runScene.name + " " + runScene.name);
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(runScene.name));
  }

  public void Sleep() {
    var log = contextHolder.contexts.global.CreateEntity();
    log.AddMessageLog("You've slept for 8 hours");
    var timeSkip = contextHolder.contexts.global.CreateEntity();
    timeSkip.AddPassingTime(0.3f);
  }
  
  public void Browse() {
    var log = contextHolder.contexts.global.CreateEntity();
    log.AddMessageLog("Cat pics, cat pics...");
    var timeSkip = contextHolder.contexts.global.CreateEntity();
    timeSkip.AddPassingTime(0.05f);
  }

  public void Eat() {
    var log = contextHolder.contexts.global.CreateEntity();
    log.AddMessageLog("So tasty!");
    var e = contextHolder.contexts.global.CreateEntity();
    e.AddResourceModification("food", -1);
    var timeSkip = contextHolder.contexts.global.CreateEntity();
    timeSkip.AddPassingTime(0.05f);
  }

  public void GoOut() {
    Debug.Log("Go out");
    contextHolder.contexts.game.SetRunDescription("food", 100);
    StartCoroutine(LoadRunScene());
  }
}
}