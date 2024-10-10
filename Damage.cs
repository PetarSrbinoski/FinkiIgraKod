using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageHit;
    Animator animator;
    TouchingSpaceDirections touchingDirections;
    Rigidbody2D rb;

    [SerializeField]
    public bool isInvincible = false;
    public float invincibilityTime = 0.25f;
    private float timeSinceHit = 0;
    private float startGravity;



    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        private set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
   
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingSpaceDirections>();
    }
    private void Start()
    {
        startGravity = rb.gravityScale;

    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }


    public void Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger("Hit");
            damageHit?.Invoke(damage, knockback);

            if (!touchingDirections.IsGrounded)
            {
                rb.velocity = Vector2.up * 6;

                rb.gravityScale = 3;

                StartCoroutine(changeGravity());
            }
        }
    }


    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int finalHeal = Mathf.Min(maxHeal, healthRestore);
            Health += finalHeal;
            return true;
        }
        return false;
    }

    private IEnumerator changeGravity()
    {
        
        yield return new WaitForSeconds(0.6f);
        rb.gravityScale = startGravity;
    }

    public void Kill()
    {
        IsAlive = false;
    }
}
