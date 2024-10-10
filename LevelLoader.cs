using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;
    

    public void LoadNextLevel()
    {
        animator.SetTrigger("start");

        StartCoroutine(transitionTo(SceneManager.GetActiveScene().buildIndex + 1));

    }

    

    public void LoadByName(string name)
    {
        animator.SetTrigger("start");

        StartCoroutine(loadLevelByName(name));

    }


    IEnumerator transitionTo(int buildIndex)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(buildIndex);

    }

    IEnumerator deathScreen()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("DeathScreen");

    }

    IEnumerator loadLevelByName(string level)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(level);

    }

    


    
}
