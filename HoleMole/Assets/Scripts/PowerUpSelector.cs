using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSelector : MonoBehaviour
{
    public GameObject powerupsGO;
    public GameObject grayBackground;

    private PlayerInput playerInput;
    private Animator animator;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        animator = FindObjectOfType<Animator>();
        Invoke("StartPowerupsAnimation", 10f);
    }

    private void StartPowerupsAnimation ()
    {
        animator.SetTrigger("ShowPowerUps");
        grayBackground.SetActive(true);
        Time.timeScale = .25f; 
    }

    private void ShowPowerUps()
    {
        powerupsGO.SetActive(true);
    }

    public void OnPowerUpSelect(int iPowerUp)
    {
        Time.timeScale = 1;
        playerInput.SetCurrentPowerUp(iPowerUp);
        powerupsGO.SetActive(false);
        grayBackground.SetActive(false);
    }
}
