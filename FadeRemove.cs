using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{
    public float fadeTime = 1.5f;
    private float timeElapsed = 0;

    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;

    public GameObject heart;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        objToRemove = animator.gameObject;
        startColor = spriteRenderer.color;

    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        float newAlpha = startColor.a * (1 - timeElapsed / fadeTime);
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        if (timeElapsed >= fadeTime)
        {
            int number = Random.Range(1, 10);
            Debug.Log(number);
            if (number % 3 == 0)
                Instantiate(heart, objToRemove.transform.position, Quaternion.identity);
            Destroy(objToRemove);

        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
