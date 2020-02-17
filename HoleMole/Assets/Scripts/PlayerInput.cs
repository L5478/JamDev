using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    enum CurrentPowerUp { None, Plank, Water, Fire }

    private CurrentPowerUp powerUp = CurrentPowerUp.None;

    void Update()
    {
        //Mouse button down if not hovering over any UI elements
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log(hit.transform.gameObject);
                //Do something with the hit object
            }
        }
    }
}
