﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
   private Animator anim;

	// Use this for initialization
	void Start () {
        // Gather components
        // Collider2D[] coll = GetComponents<Collider2D>();
        // standing2D = coll[0];
        // crouching2D = coll[1];
        anim = GetComponent<Animator>();
	}
    
    // public Collider2D getStanding2D() {
    //     return standing2D;
    // }
    // public Collider2D getCrouching2D() {
    //     return crouching2D;
    // }
    // called once per physics step
    private void FixedUpdate() {
        // Movement independent from jumping
        if (Sinput.GetAxisRaw("Vertical") < 0 || Sinput.GetButton("Crouch"))
            anim.SetTrigger("Slide");
    }
}