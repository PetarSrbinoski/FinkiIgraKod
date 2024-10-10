using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public string nextLevelName;

    public GameObject pauseMenuUI;
    public GameObject pauseES;

    public GameObject gameOverUI;
    public GameObject gameOverES;

    public GameObject victoryUI;
    public GameObject victoryES;

    public LevelLoader loader;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameIsPaused)
            { 
                Resume();
            }

            else
            { 
                Pause();
            }
        }
    }

    public void LoadMenu()
    {
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        victoryUI.SetActive(false);

        Time.timeScale = 1f;
        loader.LoadByName("TitleScreen");

    }

    public void LoadDeathScreen()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void RestartLevel()
    {

        string name = SceneManager.GetActiveScene().name;
        gameOverUI.SetActive(false);
        loader.LoadByName(name);
        Time.timeScale = 1f;

    }
    public void NextLevel()
    {
        victoryUI.SetActive(false);
        loader.LoadByName(nextLevelName);
        Time.timeScale = 1f;

    }
    public void QuitGame()
    {
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        victoryUI.SetActive(false);



        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Resume()
    {
        pauseES.SetActive(false);
        victoryES.SetActive(false);
        gameOverES.SetActive(true);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    private void Pause()
    {
        gameOverES.SetActive(false);
        victoryES.SetActive(false);
        pauseES.SetActive(true);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OnWin()
    {
        gameOverES.SetActive(false);
        pauseES.SetActive(false);
        victoryES.SetActive(true);

        victoryUI.SetActive(true);

    }



}
