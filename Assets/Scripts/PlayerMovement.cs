using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the script responsible for player movement.
/// Comments will be left in for future studies.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator animator;
    private SpriteRenderer sr;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }
    //private MovementState state;


    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Hello World!");

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>(); 
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // To get any key from the keyboard use GetKey(KeyCode)
        // Reference: https://docs.unity3d.com/ScriptReference/KeyCode.html


        // To get "precise" movements use GetAxisRaw("Axis")
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        UpdateAnimation(dirX);
    }

    private void UpdateAnimation(float dirX)
    {
        MovementState state; 

        switch (dirX)
        {
            case > 0:
                state = MovementState.running;
                sr.flipX = false;
                break;
            case < 0:
                state = MovementState.running;
                sr.flipX = true;
                break;
            default:
                state = MovementState.idle;
                break;
        }

        switch (rb.velocity.y)
        {
            case > .1f:
                state = MovementState.jumping;
                break;
            case < -.1f:
                state = MovementState.falling;
                break;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
