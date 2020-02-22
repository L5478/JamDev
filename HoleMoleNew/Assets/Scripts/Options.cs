using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    //Temp options
    public GameObject optionsMenu;

    private AudioListener audioListener;
    private bool openedDuringAnimation;

    private void Start()
    {
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
        if (optionsMenu.active == true)
        {
            optionsMenu.SetActive(false);
            if (openedDuringAnimation)
            {
                Time.timeScale = 0.15f;
                openedDuringAnimation = false;
            }
        }
        else
        {
            optionsMenu.SetActive(true);
            if (Time.timeScale < 1)
            {
                openedDuringAnimation = true;
                Time.timeScale = 0;
            }
        }
    }

    public void ToggleMute()
    {
        AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
    }
}
