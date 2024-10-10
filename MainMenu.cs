using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public LevelLoader loader;
    public string levelToLoad;

    public void PlayGame()
    {
        loader.LoadNextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void LoadLevel()
    {
        loader.LoadByName(levelToLoad);
    } 
}
