using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proffesor : MonoBehaviour
{
    public float walkAccel = 30f;
    public float slowRate = 0.02f;
    public float maxSpeed = 4f;
    public DetectionZone attackZone;
    public DetectionZone clifDetection;
    public Vector2 directionOfAttack;
    public bool bag = false;

    int bagNumber = 1;




    Animator animator;
    Rigidbody2D rb;
    Vector2 walkDirectionVector = Vector2.right;
    TouchingSpaceDirections touchingDirections;

    public Transform player;

    public enum WalkableDirection { Right, Left}

    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0));
        }
    }

    private WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection { get { return _walkDirection; }
        private set
        {
            if(_walkDirection != value)
            {

                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }

                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
      
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingSpaceDirections>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      
    }

    private void FixedUpdate()
    {
        Vector2 prevDirAttack = directionOfAttack;
        directionOfAttack = (player.position - this.rb.transform.position).normalized;



        if (Mathf.Sign(prevDirAttack.x) != Mathf.Sign(directionOfAttack.x))
        {
            //gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * (-1), gameObject.transform.localScale.y);
            FlipDirection();
        }

        if (!animator.GetBool("lockVelocity") && touchingDirections.IsGrounded)
        {
            if (CanMove)
     
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAccel * directionOfAttack.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y
                    );
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, slowRate), rb.velocity.y);
        }
    }
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        AttackCooldown -= Time.deltaTime;

        if (player == null) Debug.Log("PLayerNOtFound");
    }

    void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }

        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
            
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (bag)
        {
            if (bagNumber == 1)
            {
                animator.SetTrigger("Hit");
                bagNumber = -1;
            }
            else if (bagNumber == -1)
            {
                animator.SetTrigger("Hit2");
                bagNumber = 1;
            }
        }

        animator.SetBool("lockVelocity", true);

        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }



}
