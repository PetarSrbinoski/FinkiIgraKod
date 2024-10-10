using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower_Shoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;
    private Animator animator;
    public Transform shootingPosition;


    private float timer;
    private float shootTime;
    private float distance;
    public bool isShooting;

    public float range;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        shootTime = Random.Range(1f, 3f);
    }

    void Update()
    {
        if (animator.GetBool("lockVelocity")) return;

        distance = Vector2.Distance(transform.position, player.position);

        if (distance <= range)
        {
            isShooting = true;
            timer += Time.deltaTime;

            if (timer > shootTime)
            {
                shootTime = Random.Range(1f, 3f);
                timer = 0;
                shoot();
            }
        }
        else
            isShooting = false;
    }

    void shoot()
    {
        animator.SetTrigger("Attack");

        StartCoroutine(makeProjectile());
    }

    IEnumerator makeProjectile() {
        yield return new WaitForSeconds(0.3f);
        GameObject newProjectile = Instantiate(projectile, shootingPosition.position, Quaternion.identity);
        newProjectile.transform.parent = gameObject.transform;
    }
}
