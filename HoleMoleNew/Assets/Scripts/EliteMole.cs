﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMole : Mole
{
    private void Start()
    {
        dig = "Elite";
        hit = "EliteHit";
        water = "EliteWater";
        maxHealth = 2f;
        
        health = maxHealth;

        hole = FieldController.Instance.Field.GetRandomHole();
        hole.Status = Hole.HoleStatus.Mole;

        animator = GetComponentInChildren<Animator>();

        StartCoroutine(SetNewHole());

        PlayerInput.MoleHitted += NormalHit;
        PlayerInput.WaterPowerUp += WaterHit;
    }

    protected override IEnumerator SetNewHole()
    {
        while (true)
        {
            switch (hole.Status)
            {
                case Hole.HoleStatus.None:
                    hole.Status = Hole.HoleStatus.NewHole;
                    break;
                case Hole.HoleStatus.Plank:
                    FieldController.Instance.AnimatePlank(hole, "Break");
                    dig = "EliteDig";
                    break;
                case Hole.HoleStatus.Water:
                    WaterHit();
                    break;
                default:
                    hole.Status = Hole.HoleStatus.Mole;
                    break;
            }

            yield return new WaitForSeconds(spawnTime);
            health = maxHealth;
            isActive = true;

            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            animator.SetTrigger(dig);

            yield return new WaitForSeconds(waitTime);

            dig = "Elite";

            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;

            health = maxHealth;

            transform.position = Vector3.one * -3;

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();

            if (hole == null)
                hole = FieldController.Instance.Field.GetRandomHole();
        }
    }
}