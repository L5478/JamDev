using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSelector : MonoBehaviour
{
    public GameObject powerupsGO;
    public Button powerUp1;
    public Button powerUp2;

    private void Start()
    {
        Invoke("SetRandomPowerups", 20f);
    }

    private void SetRandomPowerups ()
    {
        powerupsGO.SetActive(true);
        powerUp1.GetComponentInChildren<Text>().text = "Fire";
        powerUp2.GetComponentInChildren<Text>().text = "Water";
    }
}
