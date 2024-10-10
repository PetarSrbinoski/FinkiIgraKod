using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public PauseMenu pauseMenu;
    public GameObject interact;
    LevelLoader loader;
    public string levelToLoad;

    bool canLeave = false;

    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        pauseMenu = GameObject.FindGameObjectWithTag("AllScreens").GetComponent<PauseMenu>();
    }

    private void Update()
    {
        if (canLeave && (Input.GetKey(KeyCode.JoystickButton3)|| Input.GetKey(KeyCode.L))) pauseMenu.OnWin();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interact.SetActive(true);
        canLeave = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interact.SetActive(false);

    }


}
