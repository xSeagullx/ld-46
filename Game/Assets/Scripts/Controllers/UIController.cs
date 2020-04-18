using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    void LateUpdate() {
        var cameraPos = Camera.main.transform.position;
        gameObject.transform.position = new Vector3(cameraPos.x, cameraPos.y, 0);
    }
}
