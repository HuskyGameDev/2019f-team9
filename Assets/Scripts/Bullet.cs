﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage;

	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
             PlayerInfo player = other.GetComponent<PlayerInfo>();
			 player.ReduceHealth(damage);
             GameObject.Destroy(this);
		}
    }
	
}