using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    #region Singleton
    public static FieldController instance;
    public static FieldController Instance { get => instance; }
    #endregion

    public GameObject holePrefab;

    public FieldSizeSO fieldData;

    private float emptySpace;
    private int holesX;
    private int holesZ;

    private Vector3 offset;

    public Field Field;

    private Dictionary<Hole, GameObject> holeGODictonary = new Dictionary<Hole, GameObject>();

    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
            Destroy(instance);
        else
            instance = this;
        #endregion

        emptySpace = fieldData.emptySpace;
        holesX = fieldData.holesX;
        holesZ = fieldData.holesZ;

        // Prevent wierd behaviour
        if (emptySpace <= 0)
            emptySpace = 1;

        // Offset, so Holes will be build around of this gameobject
        // i.e. Hole in center of field will sit on the same position as this gameobject
        offset = new Vector3(-(holesX - 1) / 2, 0, -(holesZ - 1) / 2);

        // Create starting field / world
        Field = new Field(holesX, holesZ);

        // Loop through field
        for (int x = 0; x < Field.Width; x++)
        {
            for (int z = 0; z < Field.Depth; z++)
            {
                // Instantiate Hole gameobject, rename it and get all children obejcts (graphics)
                GameObject holeGO = Instantiate(holePrefab, (new Vector3(x, 0, z) + offset) * emptySpace, Quaternion.identity, this.transform);
                holeGO.name = "Hole_" + x + "_" + z;

                // Get right Hole information
                Hole hole_data = Field.GetHoleAtCoordinates(x, z);
                hole_data.Position = holeGO.transform.position;

                holeGODictonary.Add(hole_data, holeGO);

                // Set right graphics based on Hole status
                SwitchHoleGFX(hole_data);
            }
        }
    }

    private void Start()
    {
        PlayerInput.PlankPowerUpHole += SetPlankHole;
        PlayerInput.FirePowerUpHole += ExplodeHole;
        PlayerInput.PlankPowerUpMole += SetPlankMole;
        PlayerInput.FirePowerUpMole += ExplodeMole;
    }



    public void SwitchHoleGFX(Hole hole)
    {
        switch (hole.Status)
        {
            case Hole.HoleStatus.None:
                SetGFXByTag("HoleNone", hole);
                break;
            case Hole.HoleStatus.Empty:
                SetGFXByTag("HoleEmpty", hole);
                break;
            case Hole.HoleStatus.Mole:
                SetGFXByTag("HoleMole", hole);
                break;
            case Hole.HoleStatus.NewHole:
                SetGFXByTag("HoleNew", hole);
                break;
            case Hole.HoleStatus.Plank:
                SetGFXByTag("HolePlank", hole);
                break;
            case Hole.HoleStatus.Water:
                SetGFXByTag("HoleWater", hole);
                break;
            case Hole.HoleStatus.Exploded:
                SetGFXByTag("HoleFire", hole);
                break;
            default:
                break;
        }
    }

    // Set right grahics to Hole gameobject by tag
    private void SetGFXByTag(string tag, Hole hole)
    {
        GameObject holeGO = holeGODictonary[hole];

        foreach (var GFX in holeGO.GetComponentsInChildren<Transform>(true))
        {
            if (GFX.gameObject.CompareTag("Hole"))
            { 
                continue;
            }

            if (GFX.gameObject.CompareTag(tag) == true)
            {
                GFX.gameObject.SetActive(true);
            }
            else
            { 
                GFX.gameObject.SetActive(false);
            }
        }
    }

    private void SetPlankHole(Hole hole)
    {
        Hole thishole = hole;
        thishole.Status = Hole.HoleStatus.Plank;
        SwitchHoleGFX(thishole);
        PowerUpSelector.instance.AddPlanks();

        for (int i = 0; i < 2; i++)
        {
            thishole = Field.GetEmptyHole();
            if (thishole != null)
            {
                thishole.Status = Hole.HoleStatus.Plank;
                SwitchHoleGFX(thishole);
                PowerUpSelector.instance.AddPlanks();
            }
        }
    }

    private void ExplodeHole(Hole hole)
    {
        Hole thishole = hole;
        thishole.Status = Hole.HoleStatus.Exploded;
        SwitchHoleGFX(thishole);
        StartCoroutine(ResetHole(thishole, Hole.HoleStatus.None, 1.5f));
        PowerUpSelector.instance.AddHolesExploded();

        for (int i = 0; i < 2; i++)
        {
            thishole = Field.GetEmptyHole();
            if (thishole != null)
            {
                thishole.Status = Hole.HoleStatus.Exploded;
                SwitchHoleGFX(thishole);
                StartCoroutine(ResetHole(thishole, Hole.HoleStatus.None, 1.5f));
                PowerUpSelector.instance.AddHolesExploded();
            }
        }
    }

    private void ExplodeMole(Mole mole)
    {
        Hole thishole = mole.Hole;
        thishole.Status = Hole.HoleStatus.Exploded;
        mole.KillMole(mole);
        SwitchHoleGFX(thishole);
        StartCoroutine(ResetHole(thishole, Hole.HoleStatus.None, 1.5f));
        PowerUpSelector.instance.AddHolesExploded();

        for (int i = 0; i < 2; i++)
        {
            thishole = Field.GetEmptyHole();
            if (thishole != null)
            {
                thishole.Status = Hole.HoleStatus.Exploded;
                SwitchHoleGFX(thishole);
                StartCoroutine(ResetHole(thishole, Hole.HoleStatus.None, 1.5f));
                PowerUpSelector.instance.AddHolesExploded();
            }
        }
    }

    private void SetPlankMole(Mole mole)
    {
        Hole thishole = mole.Hole;
        thishole.Status = Hole.HoleStatus.Plank;
        mole.KillMole(mole);
        SwitchHoleGFX(thishole);
        PowerUpSelector.instance.AddPlanks();

        for (int i = 0; i < 2; i++)
        {
            thishole = Field.GetEmptyHole();
            if (thishole != null)
            {
                thishole.Status = Hole.HoleStatus.Plank;
                SwitchHoleGFX(thishole);
                PowerUpSelector.instance.AddPlanks();
            }
        }
    }

    public void AnimatePlank(Hole hole, string animationTrigger)
    {
        GameObject holeGO = holeGODictonary[hole];
        Animator animator = holeGO.GetComponentInChildren<Animator>();

        if (animator != null)
            animator.SetTrigger(animationTrigger);
        else
            Debug.LogWarning("Couldn't find any Animators in " + holeGO + " or in children");
    }


    // Resets Hole to given status after given time in seconds
    public IEnumerator ResetHole(Hole hole, Hole.HoleStatus status, float time)
    {
        yield return new WaitForSeconds(time);
        hole.Status = status;
        SwitchHoleGFX(hole);
        Debug.Log("ResetHole");
    }
}
