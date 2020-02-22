using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject optionsMenu;

    private AudioListener audioListener;

    private void Start()
    {
        audioListener = FindObjectOfType<AudioListener>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.active == true)
            {
                optionsMenu.SetActive(false);
            }
            else
            {
                optionsMenu.SetActive(true);
            }
        }
    }

    private void ToggleMute()
    {
        AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
    }
}
