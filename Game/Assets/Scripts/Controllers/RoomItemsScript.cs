using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomItemsScript : MonoBehaviour {
  public UnityEvent onClick;

  private Vector3 originalScale;

  private void OnMouseEnter() {
    gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    originalScale = gameObject.transform.localScale;
    gameObject.transform.localScale = originalScale * 1.1f;
  }

  private void OnMouseExit() {
    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    gameObject.transform.localScale = originalScale;
  }

  private void OnMouseUp() {
    onClick.Invoke();
  }
}
