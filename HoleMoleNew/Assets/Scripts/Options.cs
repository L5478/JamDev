using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{

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
    }

    private void ToggleMute()
    {
        AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
    }
}
