using System.Collections;
using Entitas;
using ResourceDictionaries;
using Sources.Interfaces;
using Sources.Systems.Run;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Controllers {
public class RunController : MonoBehaviour, InputAccessor {
  public ContextHolder contextHolder;
  public RoadDictionary roadDictionary;
  public EnemyDictionary enemyDictionary;
  public MiscDictionary miscDictionary;
  public GameObject playerObjectPrefab;

  private Systems _systems = new Systems();
  public UIProgressController uiProgressControler;
  public UIHeartController uiHealthController;
  public UIPanicController uiPanicController;

  private string runOverReason;

  private void Start() {
    runOverReason = null;
    var contexts = contextHolder.contexts;
    #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        new Entitas.VisualDebugging.Unity.ContextObserver(contexts.game);
    #endif

    // TODO Cleanup this
    if (!contexts.game.hasRunDescription) {
      contextHolder.contexts.game.ReplaceRunDescription("mall", 300);
      string[] pattern = {
        "pavement", "pavement_side", 
        "road", "road_dash",
        "road", "road_solid",
        "road_solid_f", "road", 
        "road_dash_f", "road",
        "pavement_side_f", "pavement"
      };
      contextHolder.contexts.game.ReplaceRoad(pattern.Length, new [] {
        "pavement", "pavement", 
        "road", "road",
        "road", "road",
        "road", "road", 
        "road", "road",
        "pavement", "pavement"
      }, pattern);
      contextHolder.contexts.global.ReplaceGameState("run");
      contextHolder.contexts.game.ReplaceRunReward(new [] {
        new Reward("Got stocked up!", 0.4f, new ResourceChange("panic", -30), new ResourceChange("food", 4, 8), new ResourceChange("meds", 3, 4), new ResourceChange("toilet", 1, 2)),
        new Reward("Bought some stuff", 0.4f, new ResourceChange("food", 3), new ResourceChange("meds", 2, 3), new ResourceChange("toilet", 2)),
        new Reward("There was so little food left!", 0.2f, new ResourceChange("panic", 30), new ResourceChange("food", 1, 2), new ResourceChange("meds", 1, 2), new ResourceChange("toilet", 1)),
      });
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
      .Add(new PlayerPanicSystem(contexts, panic => uiPanicController.UpdatePlayerPanic(panic.current), reason => runOverReason = reason))

      ;

    var player = contexts.game.CreateEntity();
    player.isPlayer = true;
    player.AddLane(0, 0);
    player.AddRoadPosition(0);
    player.AddView(Instantiate(playerObjectPrefab));
    player.AddVelocity(10);

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
    var globalContext = contextHolder.contexts.global;

    void Add(string resource, int amount) {
      var resourceModification = globalContext.CreateEntity();
      resourceModification.AddResourceModification(resource, amount);
    }

    var time = globalContext.CreateEntity();
    var distanceFromStart = contextHolder.contexts.game.GetGroup(GameMatcher.Player).GetSingleEntity().roadPosition.distanceFromStart;
    time.AddPassingTime(distanceFromStart / 5f / 60);

    string messageText;
    if (reason.StartsWith("fail")) {
      if (reason.EndsWith("injured")) {
        if (globalContext.resources.medsCount >= 2) {
          Add("meds", -2);
          Add("panic", 30);
          messageText = "<color=red>Damn! I'm hit, have to go home!</color>";
        }
        else {
          Add("panic", 100);
          messageText = "<color=red>I had no meds and I need them!</color>";
        }
      }
      else {
        Add("panic", 30);
        messageText = "<color=red>It's too scary there!!! I better stay at home!</color>";
      }
    }
    else {
      var rewards = contextHolder.contexts.game.runReward.rewards;
      var runRewardReward = rewards[UnityEngine.Random.Range(0, rewards.Length)]; // TODO weighted random
      messageText = runRewardReward.message;
      foreach (var change in runRewardReward._changes) {
        var amount = change.amountMin == change.amountMax
          ? change.amountMin
          : UnityEngine.Random.Range(change.amountMin, change.amountMax);
        Add(change.type, amount);  
      }
    }
    
    var message = globalContext.CreateEntity();
    message.AddMessageLog(messageText);
  }
  
  private IEnumerator LoadRoomScene() {
    Debug.Log("Loading scene Room");
    SceneManager.LoadScene("Room", LoadSceneMode.Single);
    
    yield return null;
    Debug.Log("Setting active scene Room");
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Room"));
  }
}
}