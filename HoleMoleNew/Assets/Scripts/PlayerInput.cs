using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
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
    private float hammerDistance;
    [SerializeField]
    private LayerMask posLayer;

    public enum PowerUp { None, Plank, Water, Fire }

    private PowerUp currentPowerUp = PowerUp.None;
    private Hole hole;
    private Mole mole;
    private Animator HammerAnim;
    private new Camera camera;

    public static event Action<Mole> MoleHitted;
    public static event Action<Mole> WaterPowerUp;
    public static event Action<Hole> PlankPowerUpHole;
    public static event Action<Mole> PlankPowerUpMole;
    public static event Action<Hole> FirePowerUpHole;
    public static event Action<Mole> FirePowerUpMole;

    public AudioClip waterSound;
    public AudioClip explosionSound;
    public AudioClip plankMakeSound;
    private AudioSource audioSource;
    public AudioClip clickSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        yAxis = Hammer.transform.position.y;
        HammerAnim = Hammer.GetComponent<Animator>();
        camera = GetComponent<Camera>();
        SetCursorImage();
    }

    void Update()
    {
        Ray ray1 = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray1, out RaycastHit hitPos, 40f, posLayer))
        {
            hammerDistance = Vector3.Distance(ray1.origin, hitPos.point);
            if (Time.timeScale == 1)
            {
                Hammer.transform.position = new Vector3(hitPos.point.x, yAxis, hitPos.point.z - zOffset - (hammerDistance - 6)/5);
            }
        };

        //Mouse button down if not hovering over any UI elements
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //HammerAnim.SetTrigger("SLAP");

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (currentPowerUp == PowerUp.None)
                {
                    //Regular hit mole
                    mole = hit.transform.GetComponentInParent<Mole>();


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
                            mole = hit.transform.GetComponentInParent<Mole>();
                            if (hole != null)
                            {
                                PlankPowerUpHole?.Invoke(hole);
                                audioSource.PlayOneShot(plankMakeSound, .5f);
                            }
                            if (mole != null)
                            {
                                PlankPowerUpMole?.Invoke(mole);
                                audioSource.PlayOneShot(plankMakeSound, .5f);
                            }
                            break;
                        case PowerUp.Water:
                            mole = hit.transform.GetComponentInParent<Mole>();
                            WaterPowerUp?.Invoke(mole);
                            audioSource.PlayOneShot(waterSound, .5f);
                            break;
                        case PowerUp.Fire:
                            hole = FieldController.Instance.Field.GetHoleAtPosition(hit.transform.position);
                            mole = hit.transform.GetComponentInParent<Mole>();
                            if (hole != null)
                            {
                                FirePowerUpHole?.Invoke(hole);
                                audioSource.PlayOneShot(explosionSound, .5f);
                            }
                            if (mole != null)
                            {
                                FirePowerUpMole?.Invoke(mole);
                                audioSource.PlayOneShot(explosionSound, .5f);
                            }
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
        audioSource.PlayOneShot(clickSound);

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
