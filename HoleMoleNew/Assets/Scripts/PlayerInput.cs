using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public Texture2D DefaultImg;
    public Texture2D PlankImg;
    public Texture2D WaterImg;
    public Texture2D ExplosionImg;

    public GameObject Hammer;
    [SerializeField]
    private float zOffset;
    private RaycastHit hit;
    private Ray ray1;
    private float yAxis;
    [SerializeField]
    private LayerMask posLayer;

    public enum PowerUp { None, Plank, Water, Fire }

    private PowerUp currentPowerUp = PowerUp.None;
    private Hole hole;
    private Animator HammerAnim;

    public static event Action<Mole> MoleHitted;
    public static event Action WaterPowerUp;
    public static event Action<Hole> PlankPowerUp;
    public static event Action<Hole> FirePowerUp;

    private void Start()
    {
        yAxis = Hammer.transform.position.y;
        HammerAnim = Hammer.GetComponent<Animator>();
        SetCursorImage();
    }

    void Update()
    {
        Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray1, out RaycastHit hitPos, 40f, posLayer))
        {
            Hammer.transform.position = new Vector3(hitPos.point.x, yAxis, hitPos.point.z - zOffset);
        };

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
                        HammerAnim.SetTrigger("SLAP");

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
                            hole = FieldController.Instance.Field.GetHoleAtPosition(hit.transform.position);
                            if (hole != null)
                                PlankPowerUp?.Invoke(hole);
                            break;
                        case PowerUp.Water:
                            WaterPowerUp?.Invoke();
                            break;
                        case PowerUp.Fire:
                            hole = FieldController.Instance.Field.GetHoleAtPosition(hit.transform.position);
                            if (hole != null)
                                FirePowerUp?.Invoke(hole);
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
            case "Explosion":
                currentPowerUp = PowerUp.Fire;
                SetCursorImage(ExplosionImg);
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
            Hammer.SetActive(true);
            Cursor.SetCursor(DefaultImg, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Hammer.SetActive(false);
            Cursor.SetCursor(img, Vector2.zero, CursorMode.Auto);
        }
    }
}
