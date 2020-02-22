using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuController : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject greditsMenu;

    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GreditsMenu()
    {
        audioSource.PlayOneShot(clickSound);

        playMenu.SetActive(false);
        greditsMenu.SetActive(true);
    }

    public void Back()
    {
        audioSource.PlayOneShot(clickSound);

        playMenu.SetActive(true);
        greditsMenu.SetActive(false);
    }

    public void Play()
    {
        audioSource.PlayOneShot(clickSound);

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
