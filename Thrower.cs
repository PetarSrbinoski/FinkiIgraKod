using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private Thrower_Shoot TShoot;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        TShoot = GetComponent<Thrower_Shoot>();
    }

    void Update()
    {
        if (animator.GetBool("lockVelocity")) return;

        float distanceToPlayer = (TShoot.player.transform.position.x - this.transform.position.x);
    

        if (TShoot.isShooting)
        {
            if (distanceToPlayer <= 3f && distanceToPlayer >= -3f)
            {
                animator.SetBool("IsWalking", true);
                rb.velocity = new Vector2(-Mathf.Sign(distanceToPlayer) * 2f, rb.velocity.y);
            }
            transform.localScale = new Vector2(Mathf.Sign(distanceToPlayer) * 3f, transform.localScale.y);
            return;

        }

        if (rb.velocity.x != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
            animator.SetBool("IsWalking", false);
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        animator.SetBool("lockVelocity", true);
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }
}
