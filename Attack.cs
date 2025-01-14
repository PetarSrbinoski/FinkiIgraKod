using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 1;
    public Vector2 knockback = Vector2.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if (damage != null)
        {           
            Vector2 finalKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damage.Hit(attackDamage, finalKnockback);
        }
    }

}
