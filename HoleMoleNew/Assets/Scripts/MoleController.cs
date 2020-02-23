using UnityEngine;

public class MoleController : MonoBehaviour
{
    [Header("Number of moles at start")]
    public int normalMole;
    public int eliteMole;

    [Header("Max number of moles in game")]
    public int maxNormalMole;
    public int maxEliteMole = 3;

    [Header("Hole thresholds for mole spawns")]
    [Tooltip("How many holes there need to be in game to spawn next Normal mole. List should be from lowest to highest")]
    public int[] holes;

    [Tooltip("How many planks there need to be to spawn extra Elite mole")]
    public int planks;

    private bool[] wavesRegistered;

    private int emptyHoleCount;
    private int noneHoleCount;
    private int plankHoleCount;
    private int moleHoleCount;

    private int moleNormalCount = 0;
    private int moleEliteCount = 0;

    private int lastHoleCount;
    private int maxHoles;
    private int holeCount;

    private void Start()
    {
        Hole.HoleStatusChange += HoleStatusHasChanged;

        lastHoleCount = FieldController.Instance.Field.GetHoleCount(Hole.HoleStatus.Plank);

        StartSpawn();
        maxHoles = FieldController.Instance.Field.HolesAmount;

        wavesRegistered = new bool[holes.Length];

        for (int i = 0; i < holes.Length; i++)
        {
            wavesRegistered[i] = false;
        }
    }

    private void OnDestroy()
    {
        Hole.HoleStatusChange -= HoleStatusHasChanged;
    }

    // Handles mole spawning at start of the game
    private void StartSpawn()
    {
        //NormalMoleBalance();
        float step = 0.1f;

        for (int i = 0; i < normalMole; i++)
        {
            GameObject mole = SpawnNewMole("MoleNormal", 3, 1 + step);
            if (mole == null)
                return;
            mole.SetActive(true);
            step += 0.1f;
        }

        step = 0.1f;

        for (int i = 0; i < eliteMole; i++)
        {
            GameObject mole = SpawnNewMole("MoleElite", 3, 2 + step);
            if (mole == null)
                return;

            mole.SetActive(true);
            step += 0.1f;
        }

    }

    // Spawn new mole by tag
    private GameObject SpawnNewMole(string tag, float wait = 3, float spawn = 1.5f)
    {
        if (moleEliteCount >= maxEliteMole && tag == "MoleElite" || moleNormalCount >= maxNormalMole && tag == "MoleNormal")
        {
            Debug.Log("Max amount of "+tag+"s reached");
            return null;
        }
        
        
        GameObject moleGO = PoolerScript.current.GetPooledObject(tag);
        if(moleGO.TryGetComponent<Mole>(out Mole mole))
        {
            mole.waitForAnimationsEnd = wait;
            mole.spawnNextTime = spawn;
        }

        // Count moles
        switch (tag)
        {
            case "MoleNormal":
                moleNormalCount++;
                break;
            case "MoleElite":
                moleEliteCount++;
                break;
            default:
                break;
        }

        return moleGO;
    }

    // Count how many hole in certain status is active in game
    // and act on changes in realtime
    private void HoleStatusHasChanged()
    {
        emptyHoleCount = FieldController.Instance.Field.GetHoleCount(Hole.HoleStatus.Empty);
        noneHoleCount = FieldController.Instance.Field.GetHoleCount(Hole.HoleStatus.None);
        plankHoleCount = FieldController.Instance.Field.GetHoleCount(Hole.HoleStatus.Plank);
        moleHoleCount = FieldController.Instance.Field.GetHoleCount(Hole.HoleStatus.Mole);

        holeCount = maxHoles - noneHoleCount;

        PlankEliteMoleBalance();
        NormalMoleBalance();
    }

    private void NormalMoleBalance()
    {
        for (int i = holes.Length; i > 0; i--)
        {
            if (holeCount >= holes[i-1] && wavesRegistered[i-1] == false && moleNormalCount <= maxNormalMole)
            {
                GameObject mole = SpawnNewMole("MoleNormal", 3, 1.5f);
                if (mole == null)
                    return;
                mole.SetActive(true);

                wavesRegistered[i - 1] = true;
            }
        }
    }

    private void PlankEliteMoleBalance()
    {
        if (plankHoleCount != lastHoleCount && plankHoleCount >= planks && moleEliteCount <= maxEliteMole)
        {
            GameObject mole = SpawnNewMole("MoleElite", 3, 2);
            if (mole == null)
                return;
            mole.SetActive(true);
            lastHoleCount = plankHoleCount;
        }
    }
}
