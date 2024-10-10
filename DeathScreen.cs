using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : StateMachineBehaviour
{

    public PauseMenu gameover;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameover = GameObject.Find("PauseMenuCanvas").GetComponent<PauseMenu>();

    }

   

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameover.LoadDeathScreen();

    }



}
