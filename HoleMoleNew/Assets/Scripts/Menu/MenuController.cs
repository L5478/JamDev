using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject greditsMenu;

    public void GreditsMenu()
    {
        playMenu.SetActive(false);
        greditsMenu.SetActive(true);
    }

    public void Back()
    {
        playMenu.SetActive(true);
        greditsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
