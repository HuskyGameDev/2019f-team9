﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    // Public properties
    public float jumpForce;
    public int jumpFrameBuffer;
    public LayerMask groundLayer;

    // Internal variables
    private int currentJumpFrameBuffer;
    bool wasGrounded;

    // Player Components
    private Rigidbody2D rb2d;
    private Collider2D collider2d;
    private new Transform transform;

    public void SetValues(float jumpForce, int jumpFrameBuffer, LayerMask groundLayer) {
        this.jumpForce = jumpForce;
        this.jumpFrameBuffer = jumpFrameBuffer;
        this.groundLayer = groundLayer;
    }
	// Use this for initialization
	void Start () {

        // Gather components
        transform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        wasGrounded = false;

    }
	
	// Update is called once per frame
	void Update () {
	}

    private bool jumping;
    // called once per physics step
    private void FixedUpdate() {
        if (jumping && isGrounded()) {
            GetComponent<Animator>().SetTrigger("Land");
            jumping = false;
        }

        // Check if eligible for jumping (grounded and pressing button)
        if ((Sinput.GetAxisRaw("Vertical") > 0 || Sinput.GetAxisRaw("Jump") > 0) && canJump()) {
            // Jump!
            if (rb2d.velocity.y <= 0.001f) {
                rb2d.AddForce(300 * transform.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
                AkSoundEngine.PostEvent("Jump", gameObject);
                GetComponent<Animator>().SetTrigger("Jump");
                jumping = true;
            }
        }

        if (currentJumpFrameBuffer > 0) {
            currentJumpFrameBuffer--;
        }
        
    }

    private bool canJump() {
        
        // if (wasGrounded || isGrounded()) {
        //     if(wasGrounded && !isGrounded()){
        //         wasGrounded = false;
        //     }
        //     if (currentJumpFrameBuffer == 0) {
        //         return true;
        //     }
        // } else {
        //     if (currentJumpFrameBuffer == 0) {
        //         return true;
        //     }
        // } else {
        //     currentJumpFrameBuffer = jumpFrameBuffer;
        // }
        // return false;
        if(wasGrounded || isGrounded()){
            return true;
        }
        return false;

    }

    // Raycasting method to check if on the ground (or close enough that the difference is negligible)
    private bool isGrounded() {
        // RaycastHit2D hitLeft = Physics2D.Raycast(collider2d.bounds.center - new Vector3(collider2d.bounds.extents.x * 0.5f, collider2d.bounds.extents.y, 0), -transform.up, 0.3f, groundLayer.value);
        // RaycastHit2D hitRight = Physics2D.Raycast(collider2d.bounds.center - new Vector3(-collider2d.bounds.extents.x * 0.5f, collider2d.bounds.extents.y, 0), -transform.up, 0.3f, groundLayer.value);
		RaycastHit2D hitLeft = Physics2D.Raycast(collider2d.bounds.center - new Vector3(collider2d.bounds.extents.x * 0.9f, collider2d.bounds.extents.y * 0.9f, 0), -transform.up, 0.15f, groundLayer.value);
        RaycastHit2D hitRight = Physics2D.Raycast(collider2d.bounds.center - new Vector3(-collider2d.bounds.extents.x * 0.9f, collider2d.bounds.extents.y * 0.9f, 0), -transform.up, 0.15f, groundLayer.value);

        Vector3 normalizedUp = Vector3.Normalize(transform.up);

        Vector3 leftNormal = Vector3.Normalize(hitLeft.normal);
        Vector3 rightNormal = Vector3.Normalize(hitRight.normal);

        if (hitLeft && hitLeft.collider.OverlapPoint(hitLeft.point + new Vector2(0.0f, 0.01f))) {
            leftNormal *= -1;
        }
        if (hitRight && hitRight.collider.OverlapPoint(hitRight.point + new Vector2(0.0f, 0.01f))) {
            rightNormal *= -1;
        }

        bool leftGrounded = Vector3.Dot(leftNormal, normalizedUp) > 0;
        bool rightGrounded = Vector3.Dot(rightNormal, normalizedUp) > 0;
        // bool leftGrounded = leftNormal.Equals(normalizedUp);
        // bool rightGrounded = rightNormal.Equals(normalizedUp);

        if (leftGrounded || rightGrounded) {
            wasGrounded=true;
            return true;
        }
        wasGrounded=false;
        return false;
    }
}
