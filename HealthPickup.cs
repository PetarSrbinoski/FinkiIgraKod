using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if (damage)
        {
            bool wasHealed = damage.Heal(healthRestore);

            if (wasHealed)
                Destroy(gameObject);
        }
    }
}
