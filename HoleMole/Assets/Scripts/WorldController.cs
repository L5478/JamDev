using UnityEngine;

public class WorldController : MonoBehaviour
{
    public GameObject holePrefab;

    public float emptySpace = 1f;
    public int holesX;
    public int holesZ;

    private Vector3 offset;

    private Field field;
    private Transform[] holeGFX;

    private void Start()
    {
        // Offset, so Holes will be build around of this gameobject
        // i.e. Hole in center of field will sit on the same position as this gameobject
        offset = new Vector3(-(holesX -1)/2, 0, -(holesZ-1)/2);

        // Prevent wierd behaviour
        if (emptySpace <= 0)
            emptySpace = 1;

        // Create starting field / world
        field = new Field(holesX,holesZ);

        // Loop through field
        for (int x = 0; x < field.Width; x++)
        {
            for (int z = 0; z < field.Depth; z++)
            {
                // Instantiate Hole gameobject, rename it and get all children obejcts (graphics)
                GameObject holeGO = Instantiate(holePrefab, (new Vector3(x, 0, z) + offset) * emptySpace, Quaternion.identity, this.transform);
                holeGO.name = "Hole_" + x + "_" + z;
                holeGFX = holeGO.GetComponentsInChildren<Transform>();

                // Get right Hole information
                Hole hole_data = field.GetHoleAt(x, z);

                // Set right graphics based on Hole status
                switch (hole_data.Status)
                {
                    case Hole.HoleStatus.None:
                        SetGFXByTag("HoleNone");
                        break;
                    case Hole.HoleStatus.Empty:
                        SetGFXByTag("HoleEmpty");
                        break;
                    case Hole.HoleStatus.Mole:
                        SetGFXByTag("HoleMole");
                        break;
                    case Hole.HoleStatus.Plank:
                        SetGFXByTag("HolePlank");
                        break;
                    case Hole.HoleStatus.Water:
                        SetGFXByTag("HoleWater");
                        break;
                    case Hole.HoleStatus.Fire:
                        SetGFXByTag("HoleFire");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Set right grahics to Hole gameobject by tag
    private void SetGFXByTag(string tag)
    {
        if (holeGFX == null)
            return;

        foreach (var GFX in holeGFX)
        {
            if (GFX.CompareTag("Hole"))
                continue;

            if (GFX.CompareTag(tag))
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
