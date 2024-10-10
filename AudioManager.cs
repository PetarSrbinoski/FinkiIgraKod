using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Source --------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    

    [Header("----------- Audio Clips --------------")]
    public AudioClip background;
    public AudioClip menuHover;
    public AudioClip punch;
    public AudioClip kick;
    public AudioClip headButt;
    public AudioClip spin;
    public AudioClip bite;
    public AudioClip dash;
    public AudioClip hurt;
    public AudioClip uppercut;


    public float volume;

    private void Start()
    {

        musicSource.clip = background;
        musicSource.volume = volume;
        musicSource.Play();
        
    }

    private void Update()
    {
        musicSource.volume = volume;

    }

    public void MouseHover()
    {
        SFXSource.clip = menuHover;
        SFXSource.Play();
    }

    public void dashSound()
    {
        SFXSource.clip = dash;
        SFXSource.Play();
    }

    public void attackSound(string sound)
    {
        if (sound == "punch")
            SFXSource.clip = punch;

        else if (sound == "kick")
            SFXSource.clip = kick;

        else if (sound == "spin")
            SFXSource.clip = spin;

        else if (sound == "bite")
            SFXSource.clip = bite;
        else if (sound=="head")
            SFXSource.clip = headButt;
        else if (sound=="uppercut")
            SFXSource.clip = uppercut;
        else if (sound=="hurt")
            SFXSource.clip = hurt;


        SFXSource.Play();
       
    }

    public void StopSFX()
    {
        SFXSource.Stop();

    }


}
