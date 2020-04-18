using System;
using Entitas;
using ResourceDictionaries;
using Sources;
using Sources.Interfaces;
using Sources.Systems.Run;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
public class RunController : MonoBehaviour, InputAccessor {
  public ContextHolder contextHolder;
  public RoadDictionary roadDictionary;
  private Scene roomScene;
  public GameObject playerObjectPrefab;

  private Systems _systems = new Systems();
  public UIProgressController uiProgressControler;

  private void Start() {
    roomScene = SceneManager.GetSceneByName("Room");

    var contexts = contextHolder.contexts;
    #if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        var contextObserver = new Entitas.VisualDebugging.Unity.ContextObserver(contexts.game);
        DontDestroyOnLoad(contextObserver.gameObject);
    #endif

    // TODO Cleanup this
    if (!contexts.game.hasRunDescription) {
      contexts.game.SetRunDescription("food", 1000);
    }

    _systems
      // Init
      .Add(new EnemySpawnSystem(contexts))
      .Add(new RoadCreationSystem(contexts, roadDictionary))

      // Update
      .Add(new InputSystem(contexts, this))
      .Add(new RunSystem(contexts))

      .Add(new TransformSyncSystem(contexts))
      .Add(new RoadCullingSystem(contexts, Camera.main))

      // Render
      .Add(new FollowingCameraSystem(contexts, Camera.main))
      .Add(new RoadRenderSystem())
      .Add(new RunProgressUpdateSystem(contexts, (progress) => uiProgressControler.updateProgress(progress)))

      ;

    var player = contexts.game.CreateEntity();
    player.isPlayer = true;
    player.AddLane(0);
    player.AddRoadPosition(0);
    player.AddView(Instantiate(playerObjectPrefab));

    _systems.Initialize();
  }

  private void Update() {
    _systems.Execute();
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
}
}