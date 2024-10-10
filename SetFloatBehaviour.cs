using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;
    public bool onStateEnter, onStateExit;
    public bool onStateMachineEnter, onStateMachineExit;
    public float valueOnEnter, valueOnExit;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (onStateEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (onStateExit)
            animator.SetFloat(floatName, valueOnExit);
    }


    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (onStateMachineEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (onStateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }
}
