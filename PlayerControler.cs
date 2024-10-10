using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    TouchingSpaceDirections touchingDirections;
    Damage damage;
   // TutorialManaer tutorialManager;

    public AudioManager audio;

    public float walkSpeed = 5f;
    public float runSpeed = 15f;
    public float jumpImpulse = 10f;

    public float maxFallingSpeed = 30f;

    public float dashSpeed = 10f;
    public float dashTime = 0.5f;
    private Vector2 dashDirection;
    private bool isDashing = false;
    private bool canDash = true;


    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter = 0;

    private float startGravity;

    bool jump = false;
    


    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    public bool IsAlive { get { return animator.GetBool(AnimationStrings.isAlive); } }

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)

                transform.localScale *= new Vector2(-1, 1);

            _isFacingRight = value;
        }
    }
    public bool _isFacingRight = true;

    public float CurrentMoveSpeed
    {
        get
        {
            if (!CanMove)
                return 4;
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }
                else return walkSpeed;
            }
            else return 0;
        }
    }



    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    private bool _isMoving = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    private bool _isRunning = false;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingSpaceDirections>();
        damage = GetComponent<Damage>();
       // tutorialManager = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<TutorialManaer>();


        startGravity = rb.gravityScale;
    }

    private void Update()
    {
        //  if (pause.GameIsPaused) return;

        if (!animator.GetBool(AnimationStrings.isAlive)) return;

        if (touchingDirections.IsGrounded)
            coyoteTimeCounter = coyoteTime;

        else
            coyoteTimeCounter -= Time.deltaTime;

        jumpBufferCounter -= Time.deltaTime;

        if (!isDashing )//&& touchingDirections.IsGrounded)
            canDash = true;


        if (gameObject.transform.position.y < -40)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 30);

        if (rb.velocity.y < -50)
            rb.velocity = new Vector2(rb.velocity.x, -50);
        
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        if (!animator.GetBool("lockVelocity"))
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
           // tutorialManager.move = true;

        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (coyoteTimeCounter > 0 && CanMove && jumpBufferCounter > 0)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        

        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);
        }
        else
            IsMoving = false;

    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;

        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
            IsRunning = true;

        else if (context.canceled)
            IsRunning = false;

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isDashing || !CanMove) return;

        //TODO check if is alive 
        if (context.started )
        {
            jumpBufferCounter = jumpBufferTime;
            //tutorialManager.jump = true;
            
        }
        if (context.canceled)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0;


        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isDashing) return;


        if (context.performed)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
           // tutorialManager.punch = true;

            if (!touchingDirections.IsGrounded)
            {
                rb.velocity = Vector2.up * 4;

                rb.gravityScale = 3;

                StartCoroutine(changeGravity());
            }

            //if (jump && touchingDirections.IsGrounded)
            //{
            //    jump = false;
            //    rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            //}
        }

    }

    public void OnHit(int damage , Vector2 knockback)
    {
        
        animator.SetBool("lockVelocity", true);
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnUppercut(InputAction.CallbackContext context)
    {
        if (isDashing) return;

        if (context.performed)
        {
            jump = true;
            animator.SetTrigger(AnimationStrings.uppercutTrigger);
            //tutorialManager.uppercut = true;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && canDash)
        {
            canDash = false;
            isDashing = true;

            damage.isInvincible = true;

            dashDirection = new Vector2(transform.localScale.x, 0);
            animator.SetTrigger(AnimationStrings.dash);
            // collider.enabled = false;
            audio.dashSound();
            rb.velocity = new Vector2(dashDirection.normalized.x * dashSpeed, transform.position.y);
            //tutorialManager.dash = true;
            StartCoroutine(stopDash());
         
        }
        
    }



    private IEnumerator changeGravity()
    {
        yield return new WaitForSeconds(0.6f);
        rb.gravityScale = startGravity;
    }


    private IEnumerator stopDash()
    {
        yield return new WaitForSeconds(0.3f);
       // collider.enabled = true;
        damage.isInvincible = false;

        
        isDashing = false;
    }

   

   

}
