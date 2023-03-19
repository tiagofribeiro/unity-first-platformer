using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the script responsible for player movement.
/// Comments will be left in for future studies.
/// </summary>
namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {

        #region COMPONENTS
        public Rigidbody2D PlayerRigidB { get; private set; }
        public BoxCollider2D PlayerBoxC { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public SpriteRenderer PlayerSpriteR { get; private set; }
        #endregion

        #region LAYERS & TAGS
        [SerializeField] private LayerMask groundLayer;
        #endregion

        #region INPUT PARAMETERS
        /// <summary>
        /// Vectorizes player horizontal input
        /// <list type="table">
        /// <item>-1 (left)</item>
        /// <item>0 (still)</item>
        /// <item>1 (right)</item>
        /// </list>
        /// </summary>
        private Vector2 moveInput;
        #endregion

        #region STATE PARAMETERS
        // Variables control the various actions the player can perform at any time.
        public bool IsFacingRight { get; private set; }
        public bool CanMove { get; set; }

        /// <summary>
        /// Enumerator for Animator var <i>"state"</i>
        /// <list type="table">
        /// <item>0 - idle</item>
        /// <item>1 - running</item>
        /// <item>2 - jumping</item>
        /// <item>3 - falling</item>
        /// </list>
        /// </summary>
        private enum MovementState { idle, running, jumping, falling }
        #endregion

        #region CHECK PARAMETERS
        [Header("Movement")]
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float jumpForce = 16f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float deacceleration = 10f;
        [SerializeField] private float currentSpeed = 0f;
        #endregion

        // #1
        private void Awake()
        {
            PlayerRigidB = GetComponent<Rigidbody2D>();
            PlayerBoxC = GetComponent<BoxCollider2D>();
            PlayerSpriteR = GetComponent<SpriteRenderer>();
            PlayerAnimator = GetComponent<Animator>();
        }

        // #2
        private void Start()
        {
            //Debug.Log("Hello World!");
            IsFacingRight = true;
            CanMove = true;
        }

        // #3 (once per frame)
        private void Update()
        {
            #region INPUT HANDLER
            // To get any key from the keyboard use GetKey(KeyCode)
            // Reference: https://docs.unity3d.com/ScriptReference/KeyCode.html

            if (CanMove)
            {
                moveInput.x = Input.GetAxis("Horizontal");

                if (moveInput.x != 0)
                {
                    CheckDirectionToFace(moveInput.x > 0);
                }

                if (Input.GetButton("Jump") && IsGrounded())
                    PlayerRigidB.velocity = new Vector3(PlayerRigidB.velocity.x, jumpForce, 0);

                UpdateAnimation(moveInput.x);
            }
            #endregion
        }

        private void FixedUpdate()
        {
            if (CanMove)
                Run();
        }

        #region MOVEMENT METHODS

        private void Run()
        {
            //maxSpeed 20
            //jumpForce 20
            //acceleration 10
            //deacceleration 10
            //currentSpeed 0

            if (moveInput.x > 0)
            {
                if (currentSpeed >= 0 && currentSpeed < maxSpeed)
                {
                    currentSpeed += acceleration * moveInput.x;
                    PlayerRigidB.AddForce(new Vector2(currentSpeed, PlayerRigidB.velocity.y));
                    currentSpeed = PlayerRigidB.velocity.x;
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed -= deacceleration;
                    PlayerRigidB.AddForce(new Vector2(currentSpeed, PlayerRigidB.velocity.y));
                    currentSpeed = PlayerRigidB.velocity.x;
                }
            }
            else if (moveInput.x < 0)
            {
                if (currentSpeed <= 0 && currentSpeed > -maxSpeed)
                {
                    currentSpeed += acceleration * moveInput.x;
                    PlayerRigidB.AddForce(new Vector2(currentSpeed, PlayerRigidB.velocity.y));
                    currentSpeed = PlayerRigidB.velocity.x;
                }
                else if (currentSpeed > 0)
                {
                    currentSpeed += deacceleration;
                    PlayerRigidB.AddForce(new Vector2(currentSpeed, PlayerRigidB.velocity.y));
                    currentSpeed = PlayerRigidB.velocity.x;
                }
            }




            //switch (Mathf.Abs(moveInput.x))
            //{
            //    case > 0:
            //        currentSpeed += acceleration * Time.deltaTime;
            //        break;
            //    default:
            //        currentSpeed -= deacceleration * Time.deltaTime;
            //        break;
            //}

            //currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

            //PlayerRigidB.AddForce(new Vector2(moveInput.x * currentSpeed, PlayerRigidB.velocity.y));

            ////PlayerRigidB.velocity = new Vector2(moveInput.x * currentSpeed, PlayerRigidB.velocity.y);

        }
        #endregion

        #region ANIMATION METHODS
        private void UpdateAnimation(float moveInput)
        {
            MovementState state;

            switch (moveInput)
            {
                case > 0:
                    state = MovementState.running;
                    break;
                case < 0:
                    state = MovementState.running;
                    break;
                default:
                    state = MovementState.idle;
                    break;
            }

            switch (PlayerRigidB.velocity.y)
            {
                case > .1f:
                    state = MovementState.jumping;
                    break;
                case < -.1f:
                    state = MovementState.falling;
                    break;
            }

            PlayerAnimator.SetInteger("state", (int)state);
        }
        #endregion


        #region CHECK METHODS
        public void CheckDirectionToFace(bool isMovingRight)
        {
            _ = isMovingRight != IsFacingRight ? PlayerSpriteR.flipX = true : PlayerSpriteR.flipX = false;
        }

        private bool IsGrounded()
        {
            return Physics2D.BoxCast(PlayerBoxC.bounds.center, PlayerBoxC.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        }
        #endregion
    }
}