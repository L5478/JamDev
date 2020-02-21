﻿using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [Header("Number of moles at start")]
    public int normalMole;
    public int eliteMole;

    [Header("Max number of moles in game")]
    public int maxNormalMole;
    public int maxEliteMole = 3;

    private int emptyHoleCount;
    private int noneHoleCount;
    private int plankHoleCount;
    private int moleHoleCount;

    private int moleNormalCount;
    private int moleEliteCount;

    private int lastHoleCount;

    private void Start()
    {
        Hole.HoleStatusChange += HoleStatusHasChanged;

        lastHoleCount = FieldController.Instance.Field.GetHoleCount(Hole.HoleStatus.Plank);

        StartSpawn();
    }

    // Handles mole spawning at start of the game
    private void StartSpawn()
    {
        float step = 0.1f;

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

    // Spawn new mole by tag
    private GameObject SpawnNewMole(string tag, float wait = 3, float spawn = 1.5f)
    {
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

        PlankEliteMoleBalance();

    }



    private void PlankEliteMoleBalance()
    {
        if (plankHoleCount != lastHoleCount && plankHoleCount >= 3 && moleEliteCount < maxEliteMole)
        {
            GameObject mole = SpawnNewMole("MoleElite", 3, 2);
            mole.SetActive(true);
            Debug.Log("Elite Mole Spawned, because so many planks.");
            lastHoleCount = plankHoleCount;
        }
    }
}
