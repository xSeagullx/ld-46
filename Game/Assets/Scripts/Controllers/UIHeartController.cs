using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeartController : MonoBehaviour {
    public List<GameObject> hearts;
    public Sprite heartFull;
    public Sprite heartDepleted;

    public void UpdatePlayerHeart(int heartsRemaining) {
        for (int i = 0; i < hearts.Count; i++) {
            var sr = hearts[i].GetComponent<Image>();
            if (heartsRemaining > i) {
                sr.sprite = heartFull; 
            }
            else {
                sr.sprite = heartDepleted;
            }
        }
    }
}
