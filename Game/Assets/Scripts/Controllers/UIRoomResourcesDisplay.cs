using ResourceDictionaries;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers {
public class UIRoomResourcesDisplay : MonoBehaviour {
  public GameObject panicMeter;
  public GameObject dayGameObject;
  public Text foodCount;
  public Text toiletCount;
  public Text moneyCount;
  public Text medsCount;
  public Calendar calendar;

  public void UpdateResources(Resources resources) {
    foodCount.text = resources.mealCount.ToString();
    medsCount.text = resources.medsCount.ToString();
    moneyCount.text = resources.money.ToString();
    toiletCount.text = resources.toiletCount.ToString();
    var panicPercent = ((float) resources.panicCount) / resources.maxPanic;
    panicMeter.transform.localScale = new Vector3(panicPercent, 1, 1);
  }

  public void UpdateDay(GameDay day) {
    dayGameObject.GetComponent<Image>().sprite = calendar.GetSprite(day);
  }
}
}