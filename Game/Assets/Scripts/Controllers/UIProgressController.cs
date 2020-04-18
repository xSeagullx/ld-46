using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgressController : MonoBehaviour {
    private const int PROGRESS_WIDTH = 10; // Same as width of Progress GO, refactor later
    public GameObject uiProgressGO;
    public Sprite[] progressAnimationSteps;

    // Start is called before the first frame update
    void Start() {
        
    }

    void Update() {
        var time = (int) (Time.time * 5);

        var sr = uiProgressGO.GetComponent<SpriteRenderer>();
        sr.sprite = progressAnimationSteps[time % progressAnimationSteps.Length];
    }

    public void updateProgress(float progress) {
        var position = uiProgressGO.transform.position;
        position = new Vector3(progress * PROGRESS_WIDTH, position.y, position.z);
        uiProgressGO.transform.position = position;
    }
}
