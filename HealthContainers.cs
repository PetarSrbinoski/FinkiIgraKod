using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainers : MonoBehaviour
{

    public GameObject[] hearts;

    Damage damage;

    private void Awake()
    {
        damage = GetComponent<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < damage.Health)
                hearts[i].GetComponent<SpriteRenderer>().enabled = true;
            else
                hearts[i].GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}
