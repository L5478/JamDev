using System;
using System.Collections;
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

    public static event Action<Mole> MoleHitted;
    public static event Action WaterPowerUp;
    public static event Action<Transform> PlankPowerUp;
    public static event Action<Transform> FirePowerUp;

    void Update()
    {
        //Mouse button down if not hovering over any UI elements
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (currentPowerUp == PowerUp.None)
                {
                    //Regular hit mole
                    Mole mole = hit.transform.GetComponentInParent<Mole>();

                    if (mole != null)
                    {
                        MoleHitted?.Invoke(mole);
                    }
                }
                else
                {
                    //PowerUp hit EVENTS hole
                    switch (currentPowerUp)
                    {
                        case PowerUp.None:
                            break;
                        case PowerUp.Plank:
                            PlankPowerUp?.Invoke(hit.transform);
                            break;
                        case PowerUp.Water:
                            WaterPowerUp?.Invoke();
                            break;
                        case PowerUp.Fire:
                            FirePowerUp?.Invoke(hit.transform);
                            break;
                        default:
                            break;
                    }

                    currentPowerUp = PowerUp.None;
                    SetCursorImage();
                }
            }
        }
    }

    public void SetCurrentPowerUp(string i)
    {
        switch (i)
        {
            case "Plank":
                currentPowerUp = PowerUp.Plank;
                SetCursorImage(PlankImg);
                break;
            case "Water":
                currentPowerUp = PowerUp.Water;
                SetCursorImage(WaterImg);
                break;
            case "Fire":
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
