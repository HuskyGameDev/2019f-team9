﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {

    // Private Properties
    private int counter = 0;
    private int interval = 13;
    
    // Public properties
    public float speed;
    // Player Components
    private Animator animator;

    public void SetSpeed(float speed) {
        this.speed = speed;
    }
	// Use this for initialization
	void Start () {
        // Gather components
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        // If we are moving right, call the function to play the sound
        if (Sinput.GetAxisRaw("Horizontal") < 0) {
            Footstep();
        }
	}
  
  // called once per physics step
  private void FixedUpdate() {

        // Movement independent from jumping
        float moveHorizontal = Sinput.GetAxisRaw("Horizontal");
        if (moveHorizontal < 0) {
            animator.SetBool("WalkingLeft", true);
            Vector3 movement = new Vector3(moveHorizontal, 0, 0);
            transform.position += (10 * movement * speed * Time.deltaTime);
        } else {
            animator.SetBool("WalkingLeft", false);
        }
    }

    void Footstep() {
        counter++;
        if ((counter % interval) == 0) {
            //Do footstep sound here
        }
    }
}
