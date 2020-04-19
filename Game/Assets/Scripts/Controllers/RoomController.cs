using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using ResourceDictionaries;
using Sources.Systems.Room;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controllers {
class InternetMessages {
  public string message;
  public int panicValue;

  public InternetMessages(string message, int panicValue) {
    this.message = message;
    this.panicValue = panicValue;
  }
}

public class RoomController : MonoBehaviour {
  public ContextHolder contextHolder;
  public UIRoomResourcesDisplay uiResources;
  public Text eventLog;

  private Systems _systems;
  private GlobalContext _globalContext;
  
  List<InternetMessages> _internetMessages = new List<InternetMessages>();

  public RoomController() {
    _internetMessages.Add(new InternetMessages("Cat pics, cat pics...", -10));
    _internetMessages.Add(new InternetMessages("Browsing the instagram...", 0));
    _internetMessages.Add(new InternetMessages("Browsing the news...", 10));
    _internetMessages.Add(new InternetMessages("So many people ill!", 50));
    _internetMessages.Add(new InternetMessages("People are crazy!", 0));
    _internetMessages.Add(new InternetMessages("Wow!", 0));
    _internetMessages.Add(new InternetMessages("Posting my breakfast...", 0));
    _internetMessages.Add(new InternetMessages("Pew pew pew", 0));
    _internetMessages.Add(new InternetMessages("Wow, Ludum Dare is on", -10));
    _internetMessages.Add(new InternetMessages("How they do it?!", 0));
    _internetMessages.Add(new InternetMessages("Watching series", 0));
  }

  private void Start() {
    contextHolder.roomController = this;
    var contexts = contextHolder.contexts;
    _globalContext = contextHolder.contexts.global;
    _systems = contextHolder.roomSystems;

#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
    new Entitas.VisualDebugging.Unity.ContextObserver(contexts.global);
#endif

    if (_systems == null) {
      _systems = new Systems();
      contextHolder.roomSystems = _systems;
      _systems
        .Add(new ResourceTrackingSystem(contexts))
        .Add(new ResourceLoggingSystem(contexts))
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
      contexts.global.SetLastSlept(1);
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
    Debug.Log("Loading scene Run");
    SceneManager.LoadScene("Run", LoadSceneMode.Single);
    
    yield return null;
    Debug.Log("Setting active scene Run");
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Run"));
  }

  public void Sleep() {
    var log = _globalContext.CreateEntity();
    if (_globalContext.gameDay.value - _globalContext.lastSlept.time < 14f / 24) {
      log.AddMessageLog("Not tired");
    }
    
    log.AddMessageLog("You've slept for 8 hours");
    var timeSkip = _globalContext.CreateEntity();
    timeSkip.AddPassingTime(0.3f);
    _globalContext.ReplaceLastSlept(_globalContext.gameDay.value + 0.3f);
  }

  public void Browse() {
    var internet = _internetMessages[Random.Range(0, _internetMessages.Count)];
    var msg = internet.message;
    if (internet.panicValue != 0) {
      var e = _globalContext.CreateEntity();
      e.AddResourceModification("panic", internet.panicValue);
      msg = "<color=" + (internet.panicValue > 0 ? "red" : "green") + ">" + msg + "</color>";
    }
    var log = _globalContext.CreateEntity();
    log.AddMessageLog(msg);
    var timeSkip = _globalContext.CreateEntity();
    timeSkip.AddPassingTime(0.05f);
  }

  public void Eat() {
    var log = _globalContext.CreateEntity();
    if (_globalContext.resources.foodCount <= 0) {
      log.AddMessageLog("<color=red>No food left!</color>");
      return;
    }

    log.AddMessageLog("So tasty!");
    var e = _globalContext.CreateEntity();
    e.AddResourceModification("food", -1);
    var timeSkip = _globalContext.CreateEntity();
    timeSkip.AddPassingTime(0.05f);
  }

  public void GoOut() {
    Debug.Log("Go out");
    contextHolder.contexts.game.SetRunDescription("food", 100);
    StartCoroutine(LoadRunScene());
  }
}
}