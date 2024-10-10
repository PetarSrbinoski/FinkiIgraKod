using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManaer : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex = 0;
    public Rigidbody2D player;
    public Animator anim;
    public GameObject bag;
    public Transform bagSpawnPosition;
    public Rigidbody2D rb;

    public bool move = false;
    public bool jump = false;
    public bool punch = false;
    public bool uppercut = false;
    public bool dash = false;
    bool key1 = false, key2 = false;

    float timer = 1f;
    float newTimer = 3f;

    private void Start()
    {
        SpawnBag();
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpIndex == popUps.Length)
        {
            return;
        }


        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[i].SetActive(true);

            else
                popUps[i].SetActive(false);
        }

        Debug.Log(popUpIndex);



        //movement left-right, jump
        if (popUpIndex == 0)
        {
            
            if (Input.GetAxisRaw("Horizontal") != 0)
                key1 = true;

            if (Input.GetKey(KeyCode.JoystickButton1))
                key2 = true;

            if (key1 && key2)
            {
                popUpIndex++;
                move = true;
                jump = true;
            }

        }


        //if (Input.GetKey(KeyCode.JoystickButton0)) Debug.Log(0);
        //if (Input.GetKey(KeyCode.JoystickButton1)) Debug.Log(1);
        //if (Input.GetKey(KeyCode.JoystickButton2)) Debug.Log(2);
        //if (Input.GetKey(KeyCode.JoystickButton3)) Debug.Log(3);
        //if (Input.GetKey(KeyCode.JoystickButton4)) Debug.Log(4);
        //if (Input.GetKey(KeyCode.JoystickButton5)) Debug.Log(5);
        //if (Input.GetKey(KeyCode.JoystickButton6)) Debug.Log(6);
        //if (Input.GetKey(KeyCode.JoystickButton7)) Debug.Log(7);
        //if (Input.GetKey(KeyCode.JoystickButton8)) Debug.Log(8);
        //if (Input.GetKey(KeyCode.JoystickButton9)) Debug.Log(9);
        //if (Input.GetKey(KeyCode.JoystickButton10)) Debug.Log(10);
        //if (Input.GetKey(KeyCode.JoystickButton11)) Debug.Log(11);
        //if (Input.GetKey(KeyCode.JoystickButton12)) Debug.Log(12);
        //if (Input.GetKey(KeyCode.JoystickButton13)) Debug.Log(13);
        //if (Input.GetKey(KeyCode.JoystickButton14)) Debug.Log(14);
        //if (Input.GetKey(KeyCode.JoystickButton15)) Debug.Log(15);
        //if (Input.GetKey(KeyCode.JoystickButton16)) Debug.Log(16);
        //if (Input.GetKey(KeyCode.JoystickButton17)) Debug.Log(17);
        //if (Input.GetKey(KeyCode.JoystickButton18)) Debug.Log(18);
        //if (Input.GetKey(KeyCode.JoystickButton19)) Debug.Log(19);


        //run
        if(popUpIndex == 1)
        {
            if(rb.velocity.x > 20) popUpIndex++;
        }


        //punch
        if(popUpIndex == 2)
        {
            newTimer -= Time.deltaTime;


            if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.JoystickButton0)) && newTimer <= 0)
            {
                punch = true;
                popUpIndex++;
            }
        }


        //uppercut
        if (popUpIndex == 3)
        {
            
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.JoystickButton0))
            {
                timer -= Time.deltaTime;
                Debug.Log(timer);

                if (timer <= 0)
                {
                    popUpIndex++;
                    uppercut = true;
                }
            }
        }

        //dash
        if (popUpIndex == 4)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton2))
            {
                popUpIndex++;
                anim.SetTrigger("openDoor");
            }
        }
        
    }

    void SpawnBag()
    {
        Instantiate(bag, bagSpawnPosition);
    }
}
