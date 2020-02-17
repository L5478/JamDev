﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public Texture2D PlankImg;
    public Texture2D WaterImg;
    public Texture2D FireImg;

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
                    Debug.Log(hit.transform);
                } else
                {
                    //Powerup hit hole
                    currentPowerUp = PowerUp.None;
                    SetCursorImage();
                    Debug.Log("used: " + currentPowerUp.ToString() + " powerup on: " + hit.transform.parent);
                }
            }
        }
    }

    public void SetCurrentPowerUp(int i)
    {
        switch (i)
        {
            case 1:
                currentPowerUp = PowerUp.Plank;
                SetCursorImage(PlankImg);
                break;
            case 2:
                currentPowerUp = PowerUp.Water;
                SetCursorImage(WaterImg);
                break;
            case 3:
                currentPowerUp = PowerUp.Fire;
                SetCursorImage(FireImg);
                break;
            default:
                break;
        }
    }

    private void SetCursorImage(Texture2D img = null)
    {
        //if method called empty -> reset image
        if (img == null)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(img, Vector2.zero, CursorMode.Auto);
        }
    }
}
