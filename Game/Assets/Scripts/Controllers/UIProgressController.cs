using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressController : MonoBehaviour {
    private const int PROGRESS_WIDTH_PX = 320; // Pixels
    public Sprite[] progressAnimationSteps;

    void Update() {
        var time = (int) (Time.time * 5);

        var sr = gameObject.GetComponent<Image>();
        sr.sprite = progressAnimationSteps[time % progressAnimationSteps.Length];
    }

    public void updateProgress(float progress) {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        var position = rectTransform.anchoredPosition;
        position = new Vector2(progress * PROGRESS_WIDTH_PX, position.y);
        rectTransform.anchoredPosition = position;
    }
}
