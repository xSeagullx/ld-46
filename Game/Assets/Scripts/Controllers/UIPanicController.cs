using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanicController : MonoBehaviour {
    public List<GameObject> panicObjects;
    public Sprite noPanic;
    public Sprite panic;

    public void UpdatePlayerPanic(int heartsRemaining) {
        for (int i = 0; i < panicObjects.Count; i++) {
            var sr = panicObjects[i].GetComponent<Image>();
            if (heartsRemaining > i) {
                sr.sprite = noPanic; 
            }
            else {
                sr.sprite = panic;
            }
        }
    }
}
