using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingSpaceDirections : MonoBehaviour
{
    public float groundDistance = 0.05f;
    public float wallDistance = 0.02f;
    public float ceilingDistance = 0.05f;

    public ContactFilter2D castFilter;
    CapsuleCollider2D touchColider;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    private bool _isGrounded = true;

    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);

        }
    }
    bool _isOnWall = false;
    public bool IsOnCeiling
    {
        get { return _isOnCeiling; }
        private set
        {
            _isOnCeiling = value;
        }
    }
    bool _isOnCeiling = false;

    private void Awake()
    {
        touchColider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

   

    void FixedUpdate()
    {
       IsOnWall = touchColider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
       IsOnCeiling = touchColider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
       IsGrounded =  touchColider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
