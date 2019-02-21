﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour {

    // Public properties
    public float speed;

    // Player Components
    private Rigidbody2D rb2d;
    private Collider2D collider2d;
    private new Transform transform;
    private Animator animator;

    public void SetSpeed(float speed) {
        this.speed = speed;
    }
	// Use this for initialization
	void Start () {
        // Gather components
        transform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    // called once per physics step
    private void FixedUpdate() {

        // Movement independent from jumping
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (moveVertical < 0) {
            animator.SetBool("Crouch", true);
        } else {
            animator.SetBool("Crouch", false);
        }
        
    }
}
