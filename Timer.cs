using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 60f;
    [SerializeField] Animator anim;
    [SerializeField] Animator animTimeUp;
    [SerializeField] Damage dmg;

    float animationtime = 1.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        animationtime -= Time.deltaTime;
        if (animationtime > 0)
            return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 6 )
            {
                anim.SetTrigger(AnimationStrings.flashTimer);
                timerText.faceColor = Color.red;
            }
        }

        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            animTimeUp.SetTrigger("time");
            dmg.Kill();

        }

        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{00}", seconds);


    }

    //IEnumerator kill()
    //{
    //    yield return new WaitForSeconds(1.5f);

    //}

}
