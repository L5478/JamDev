using UnityEngine;

public class WorldController : MonoBehaviour
{
    public GameObject holePrefab;

    public float emptySpace = 1f;
    public int holesX;
    public int holesZ;

    private Vector3 offset;

    private World world;
    private Transform[] holeGFX;

    private void Start()
    {
        offset = new Vector3(-holesX +1, 0, -holesZ+1);

        world = new World(holesX,holesZ);

        for (int x = 0; x < world.Width; x++)
        {
            for (int z = 0; z < world.Depth; z++)
            {
                GameObject holeGO = Instantiate(holePrefab, new Vector3(x, 0, z) * emptySpace + offset, Quaternion.identity, this.transform);
                holeGO.name = "Hole_" + x + "_" + z;
                holeGFX = holeGO.GetComponentsInChildren<Transform>();

                Hole hole_data = world.GetHoleAt(x, z);

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
