using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    #region Singleton
    public static FieldController instance;
    public static FieldController Instance { get => instance; }
    #endregion

    public GameObject holePrefab;

    public float emptySpace = 1f;
    public int holesX;
    public int holesZ;

    private Vector3 offset;

    public Field Field;
    private List<Transform> holeGFX = new List<Transform>();

    private Dictionary<Hole, GameObject> holeGODictonary = new Dictionary<Hole, GameObject>();

    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
            Destroy(instance);
        else
            instance = this;
        #endregion

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
                Hole hole_data = Field.GetHoleAt(x, z);
                hole_data.Position = holeGO.transform.position;

                holeGODictonary.Add(hole_data, holeGO);

                // Set right graphics based on Hole status
                SwitchHoleGFX(hole_data);
            }
        }
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
            case Hole.HoleStatus.Plank:
                SetGFXByTag("HolePlank", hole);
                break;
            case Hole.HoleStatus.Water:
                SetGFXByTag("HoleWater", hole);
                break;
            case Hole.HoleStatus.Fire:
                SetGFXByTag("HoleFire", hole);
                break;
            default:
                break;
        }
    }

    // Set right grahics to Hole gameobject by tag
    private void SetGFXByTag(string tag, Hole hole)
    {
        Debug.Log(tag + "   " + hole.Status);
        GameObject holeGO = holeGODictonary[hole];

        foreach (var GFX in holeGO.GetComponentsInChildren<Transform>())
        {
            if (GFX.CompareTag("Hole"))
            { 
                continue;
            }
            else if (GFX.CompareTag(tag))
            {
                GFX.gameObject.SetActive(true);
            }
            else
            {
                GFX.gameObject.SetActive(false);
            }
        }
    }
}
