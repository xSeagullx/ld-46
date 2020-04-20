using ResourceDictionaries;
using UnityEngine;

namespace Controllers {
[CreateAssetMenu]
public class LocationController : ScriptableObject {
  private const float DEBUG_MODIFIER = 1f;
  public ContextHolder contextHolder;

  public void GoToWork() {
    contextHolder.contexts.game.ReplaceRunDescription("work", (int) (500 * DEBUG_MODIFIER));
    string[] pattern = {
      "pavement", "pavement_side",
      "road", "road_solid",
      "road_solid_f", "road",
      "pavement_side_f", "pavement"
    };
    contextHolder.contexts.game.ReplaceRoad(pattern.Length, toLogicalPattern(pattern), pattern);
    contextHolder.contexts.global.ReplaceGameState("run");
    contextHolder.contexts.game.ReplaceRunReward(new [] {
      new Reward("Hard work pays off", 0.9f, new ResourceChange("money", 100)),
      new Reward("Got a nice bonus!", 0.1f, new ResourceChange("money", 100, 300)),
      new Reward("My colleague was off. Worrying.", 0.4f, new ResourceChange("money", 100), new ResourceChange("panic", 10)),
      new Reward("My colleague is sick. It's soo bad!", 0.1f, new ResourceChange("money", 100), new ResourceChange("panic", 10)),
    });
  }

  public void GoToPark() {
    contextHolder.contexts.game.ReplaceRunDescription("walk", (int)(300 * DEBUG_MODIFIER));
    string[] pattern = {
      "pavement", "pavement", "pavement"
    };
    contextHolder.contexts.game.ReplaceRoad(pattern.Length, toLogicalPattern(pattern), pattern);
    contextHolder.contexts.global.ReplaceGameState("run");
    contextHolder.contexts.game.ReplaceRunReward(new [] {
      new Reward("So nice to be out", 0.9f, new ResourceChange("panic", -10)),
      new Reward("Found someone's purse", 0.9f, new ResourceChange("panic", -10), new ResourceChange("money", 200, 250)),
      new Reward("Weather is so nice today!", 0.4f, new ResourceChange("panic", -40)),
      new Reward("Someone sneazed at me!", 0.1f, new ResourceChange("panic", 10)),
    });
  }

  public void GoToShop() {
    contextHolder.contexts.game.ReplaceRunDescription("shop", (int)(300 * DEBUG_MODIFIER));
    string[] pattern = {
      "pavement", "pavement_side", 
      "road", "road_dash",
      "road", "road_solid",
      "road_solid_f", "road", 
      "road_dash_f", "road",
    };
    contextHolder.contexts.game.ReplaceRoad(pattern.Length, toLogicalPattern(pattern), pattern);
    contextHolder.contexts.global.ReplaceGameState("run");
    contextHolder.contexts.game.ReplaceRunReward(new [] {
      new Reward("Got stocked up!", 0.1f, new ResourceChange("food", 2, 4), new ResourceChange("meds", 1, 2), new ResourceChange("toilet", 1)),
      new Reward("No food left!", 0.3f, new ResourceChange("panic", 30), new ResourceChange("meds", 1, 2), new ResourceChange("toilet", 1)),
      new Reward("No toilet paper left!", 0.5f, new ResourceChange("panic", 10), new ResourceChange("food", 2, 4), new ResourceChange("meds", 1, 2)),
      new Reward("Got no meds", 0.2f, new ResourceChange("panic", 30),  new ResourceChange("food", 2, 4), new ResourceChange("toilet", 1)),
    });
  }

  public void GoToMall() {
    contextHolder.contexts.game.ReplaceRunDescription("mall", (int)(300 * DEBUG_MODIFIER));
    string[] pattern = {
      "pavement", "pavement_side", 
      "road", "road_dash",
      "road", "road_solid",
      "road_solid_f", "road", 
      "road_dash_f", "road",
      "pavement_side_f", "pavement"
    };
    contextHolder.contexts.game.ReplaceRoad(pattern.Length, toLogicalPattern(pattern), pattern);
    contextHolder.contexts.global.ReplaceGameState("run");
    contextHolder.contexts.game.ReplaceRunReward(new [] {
      new Reward("Got stocked up!", 0.4f, new ResourceChange("panic", -30), new ResourceChange("food", 4, 8), new ResourceChange("meds", 3, 4), new ResourceChange("toilet", 1, 2)),
      new Reward("Bought some stuff", 0.4f, new ResourceChange("food", 3), new ResourceChange("meds", 2, 3), new ResourceChange("toilet", 2)),
      new Reward("There was so little food left!", 0.2f, new ResourceChange("panic", 30), new ResourceChange("food", 1, 2), new ResourceChange("meds", 1, 2), new ResourceChange("toilet", 1)),
    });
  }

  private string[] toLogicalPattern(string[] visualPattern) {
    var logicalPattern = new string[visualPattern.Length]; 
    for (var i = 0; i < visualPattern.Length; i++) {
      if (visualPattern[i].StartsWith("pavement")) {
        logicalPattern[i] = "pavement";
      }
      else if (visualPattern[i].StartsWith("road")) {
        logicalPattern[i] = "road";
      }
      else {
        Debug.Log("UNKNOWN visualPattern " + visualPattern[i]);
        logicalPattern[i] = "unknown";
      }
    }

    return logicalPattern;
  }
}
}