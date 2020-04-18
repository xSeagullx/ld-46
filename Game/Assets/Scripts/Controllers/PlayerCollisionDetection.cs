using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using ResourceDictionaries;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {
    public ContextHolder contextHolder;
    void OnTriggerEnter2D(Collider2D col) {
        var contexts = contextHolder.contexts;
        var collision = contexts.game.CreateEntity();
        var e = contexts.game.GetEntitiesWithView(col.gameObject).SingleEntity();
        collision.AddPlayerCollision(e);
    }
}
