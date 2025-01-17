﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage;
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject); 
    }
	void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
             Health player = other.GetComponent<Health>();
			 player.ReduceHealth(damage);
             Destroy(this.gameObject);
		}
    }
}
