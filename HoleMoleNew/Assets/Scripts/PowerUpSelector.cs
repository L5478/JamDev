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
    private float waitTime = 5f;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        animator = FindObjectOfType<Animator>();
        Invoke("StartPowerupsAnimation", waitTime);
    }

    private void StartPowerupsAnimation ()
    {
        animator.SetTrigger("ShowPowerUps");
        Time.timeScale = .25f; 
    }

    public void OnPowerUpSelect(int iPowerUp)
    {
        Time.timeScale = 1;
        playerInput.SetCurrentPowerUp(iPowerUp);
        animator.SetTrigger("Hide");
        Invoke("StartPowerupsAnimation", waitTime * 2f);
    }
}
