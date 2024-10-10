using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
            gameObject.transform.position = new Vector2 (10, 1);
    }
}
