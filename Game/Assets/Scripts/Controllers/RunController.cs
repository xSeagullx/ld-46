using System.Collections;
using Entitas;
using ResourceDictionaries;
using Sources.Interfaces;
using Sources.Systems.Run;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
public class RunController : MonoBehaviour, InputAccessor {
  public ContextHolder contextHolder;
  public RoadDictionary roadDictionary;
  public EnemyDictionary enemyDictionary;
  public MiscDictionary miscDictionary; 
  public SceneAsset roomScene;
  public GameObject playerObjectPrefab;

  private Systems _systems = new Systems();
  public UIProgressController uiProgressControler;
  public UIHeartController uiHealthController;

  private string runOverReason;

  private void Start() {
    runOverReason = null;
    var contexts = contextHolder.contexts;
    #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        new Entitas.VisualDebugging.Unity.ContextObserver(contexts.game);
    #endif

    // TODO Cleanup this
    if (!contexts.game.hasRunDescription) {
      contexts.game.SetRunDescription("food", 100);
    }

    _systems
      // Init
      .Add(new RoadCreationSystem(contexts, roadDictionary))
      .Add(new EnemySpawnSystem(contexts, entity => enemyDictionary.SetupView(entity)))

      // Update
      .Add(new CarSpawningSystem(contexts, entity => enemyDictionary.SetupView(entity)))
      .Add(new InputSystem(contexts, this))
      .Add(new RunSystem(contexts))
      .Add(new PlayerCollisionSystem(contexts))

      .Add(new TransformSyncSystem(contexts))
      .Add(new PlayerRecoverySystem(contexts))
      .Add(new RoadCullingSystem(contexts, Camera.main))
      .Add(new WarningSystem(contexts, entity => miscDictionary.SetupView(entity)))
      .Add(new RunEndSystem(contexts, reason => runOverReason = reason))

      // Render
      .Add(new FollowingCameraSystem(contexts, Camera.main))
      .Add(new RoadRenderSystem())
      .Add(new RunProgressUpdateSystem(contexts, (progress) => uiProgressControler.updateProgress(progress)))
      .Add(new PlayerHealthSystem(contexts, health => uiHealthController.UpdatePlayerHeart(health.current), reason => runOverReason = reason))

      ;

    var player = contexts.game.CreateEntity();
    player.isPlayer = true;
    player.AddLane(0, 0);
    player.AddRoadPosition(0);
    player.AddView(Instantiate(playerObjectPrefab));
    player.AddVelocity(5);
    player.ReplaceHealth(1, 3);

    _systems.ActivateReactiveSystems();
    _systems.Initialize();
  }

  private void Update() {
    if (runOverReason != null) {
      EndRun(runOverReason);
    }
    else {
      _systems.Execute();  
      _systems.Cleanup();
    }
  }

  public Vector2 GetMousePosition() {
    return Input.mousePosition;
  }

  public DirectionalInput GetDirectionalInput() {
    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
      return DirectionalInput.RIGHT;
    }
    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
      return DirectionalInput.LEFT;
    }

    return DirectionalInput.NONE;
  }

  public void EndRun(string reason) {
    Debug.Log("Run over for the reason " + reason);
    CalculateRunResults(reason);
    _systems.DeactivateReactiveSystems();
    contextHolder.contexts.game.DestroyAllEntities();

    StartCoroutine(LoadRoomScene());
  }

  private void CalculateRunResults(string reason) {
    void Add(string resource, int amount) {
      var resourceModification = contextHolder.contexts.global.CreateEntity();
      resourceModification.AddResourceModification(resource, amount);
    }
    if (reason.StartsWith("fail")) {
      Add("panic", 30);
      if (reason.EndsWith("injury")) {
        Add("meds", -2);  
      }
    }
    else {
      Add("panic", -10);
      Add("money", 100);
      Add("food", 4);
      Add("meds", 2);
      Add("toilet", 1);
    }
  }
  
  private IEnumerator LoadRoomScene() {
    Debug.Log("Loading scene " + roomScene.name + " " + roomScene.name);
    SceneManager.LoadScene(roomScene.name, LoadSceneMode.Single);
    
    yield return null;
    Debug.Log("Setting active scene " + roomScene.name + " " + roomScene.name);
    SceneManager.SetActiveScene(SceneManager.GetSceneByName(roomScene.name));
  }
}
}