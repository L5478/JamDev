using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSelector : MonoBehaviour
{
    public GameObject powerupsGO;
    public GameObject grayBackground;
    public List<PowerUpSO> listPowerUps;
    public Button Spot1;
    public Button Spot2;
    

    private PlayerInput playerInput;
    private Animator animator;
    private float waitTime = 15f;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        animator = GetComponent<Animator>();
        Invoke("StartPowerupsAnimation", waitTime);
    }

    private void StartPowerupsAnimation ()
    {
        animator.SetTrigger("ShowPowerUps");
        Time.timeScale = .20f;

        var rdm1 = GetRandomPowerUp();
        var rdm2 = GetRandomPowerUp();
        Spot1.GetComponentInChildren<Text>().text = rdm1.name;
        Spot1.GetComponent<Image>().sprite = rdm1.spriteImg;
        Spot1.onClick.AddListener(() => OnPowerUpSelect(rdm1.name));
        Spot2.GetComponentInChildren<Text>().text = rdm2.name;
        Spot2.GetComponent<Image>().sprite = rdm2.spriteImg;
        Spot2.onClick.AddListener(() => OnPowerUpSelect(rdm2.name));
    }

    public void OnPowerUpSelect(string sPowerUp)
    {
        Time.timeScale = 1;
        playerInput.SetCurrentPowerUp(sPowerUp);
        animator.SetTrigger("Hide");
        Invoke("StartPowerupsAnimation", waitTime * 2f);
    }

    public PowerUpSO GetRandomPowerUp()
    {
        int totalWeight = 0;
        //add up total weight values
        foreach (var PU in listPowerUps)
        {
            totalWeight += PU.weight;
        }

        //Get random int in range
        float randomN = Random.Range(0, totalWeight);

        //Keep subtracting untill you find your random weighed number
        for (int i = 0; i < listPowerUps.Count; i++)
        {
            if (randomN < listPowerUps[i].weight) {
                return listPowerUps[i];
            }
            randomN -= listPowerUps[i].weight;
        }
        return null;
    }
}
