using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [Header("Number of moles at start")]
    public int normalMole;
    public int eliteMole;

    private float step = 0.1f;

    private void Start()
    {
        for (int i = 0; i < normalMole; i++)
        {
            GameObject mole = SpawnNewMole("MoleNormal", 3, 1 + step);
            mole.SetActive(true);
            step += 0.1f;
        }

        step = 0.1f;

        for (int i = 0; i < eliteMole; i++)
        {
            GameObject mole = SpawnNewMole("MoleElite", 3, 2 + step);
            mole.SetActive(true);
            step += 0.1f;
        }

    }

    private GameObject SpawnNewMole(string tag, float wait, float spawn)
    {
        GameObject moleGO = PoolerScript.current.GetPooledObject(tag);
        if(moleGO.TryGetComponent<Mole>(out Mole mole))
        {
            mole.waitForAnimationsEnd = wait;
            mole.spawnNextTime = spawn;
        }

        return moleGO;
    }
}
