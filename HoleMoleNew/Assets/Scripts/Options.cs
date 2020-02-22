using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Options : MonoBehaviour
{
    //Temp options
    public GameObject optionsMenu;
    public AudioClip clickSound;

    private AudioSource audioSource;

    private AudioListener audioListener;
    private bool openedDuringAnimation = false;

    private bool mute = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioListener = FindObjectOfType<AudioListener>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
    }

    public void ToggleOptions()
    {
        audioSource.PlayOneShot(clickSound);

        if (optionsMenu.active == true)
        {
            optionsMenu.SetActive(false);
            if (openedDuringAnimation)
            {
                Time.timeScale = 0.15f;
                openedDuringAnimation = false;
            } else
            {
                
                Time.timeScale = 1;
            }
        }
        else
        {
            optionsMenu.SetActive(true);
            if (Time.timeScale < 1)
            {
                openedDuringAnimation = true;
            }
            Time.timeScale = 0;
        }

        AudioListener.volume = mute == true ? 0 : 1;

    }

    public void ToggleMute()
    {
        mute = mute == false ? true : false;
        audioSource.PlayOneShot(clickSound);
    }
}
