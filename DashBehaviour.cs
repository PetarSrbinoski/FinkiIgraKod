using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : StateMachineBehaviour
{
    Damage damage;
    Rigidbody2D rb;
    public float dashSpeed = 10f;
    private Vector2 direction;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damage = animator.GetComponent<Damage>();
        rb = animator.GetComponent<Rigidbody2D>();

        direction = new Vector2(Mathf.Sign(rb.transform.localScale.x), 0);
        rb.velocity = direction * dashSpeed;
        damage.isInvincible = true;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damage.isInvincible = false;

    }


}
