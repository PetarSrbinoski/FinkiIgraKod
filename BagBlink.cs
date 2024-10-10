using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagBlink : StateMachineBehaviour
{
    float timer = 0;
    float timeToBlink;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToBlink = Random.Range(0, 7);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer >= timeToBlink)
        {
            timer = 0;
            animator.SetTrigger("Blink");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
