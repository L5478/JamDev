using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public enum PowerUp { None, Plank, Water, Fire }

    private PowerUp currentPowerUp = PowerUp.None;

    void Update()
    {
        //Mouse button down if not hovering over any UI elements
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (currentPowerUp == PowerUp.None)
                {
                    //Regular hit mole
                    Debug.Log(hit.transform.gameObject);
                } else
                {
                    Debug.Log("used: " + currentPowerUp.ToString() + " powerup");
                    currentPowerUp = PowerUp.None;
                }
                //Do something with the hit object
            }
        }
    }

    public void SetCurrentPowerUp(int i)
    {
        switch (i)
        {
            case 1:
                currentPowerUp = PowerUp.Plank;
                break;
            case 2:
                currentPowerUp = PowerUp.Water;
                break;
            case 3:
                currentPowerUp = PowerUp.Fire;
                break;
            default:
                break;
        }
    }
}
