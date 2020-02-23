﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMole : Mole
{
    private float health;
    private float maxHealth = 1f;
    private bool skip = false;

    protected override void Start()
    {
        dig = "Normal";
        hit = "Hit";
        water = "Water";

        health = maxHealth;

        hole = FieldController.Instance.Field.GetRandomHole();
        hole.Status = Hole.HoleStatus.Empty;

        StartCoroutine(SetNewHole());

        animator = GetComponentInChildren<Animator>();

        PlayerInput.MoleHitted += NormalHit;
        PlayerInput.WaterPowerUp += WaterHit;
    }

    private void OnDestroy()
    {
        PlayerInput.MoleHitted -= NormalHit;
        PlayerInput.WaterPowerUp -= WaterHit;
    }

    protected override IEnumerator SetNewHole()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnNextTime);

            if (hole.Status != lastHoleStatus)
                hole = FieldController.Instance.Field.GetRandomHole();

            skip = false;

            switch (hole.Status)
            {
                case Hole.HoleStatus.Empty:
                    hole.Status = Hole.HoleStatus.Mole;
                    break;
                case Hole.HoleStatus.None:
                    hole.Status = Hole.HoleStatus.NewHole;
                    break;
                case Hole.HoleStatus.Plank:
                    FieldController.Instance.AnimatePlank(hole, "Block");
                    hole = FieldController.Instance.Field.GetRandomHole();
                    skip = true;
                    break;
                case Hole.HoleStatus.Water:
                    WaterHit(this);
                    break;
                case Hole.HoleStatus.Mole:
                    hole = FieldController.Instance.Field.GetRandomHole();
                    skip = true;
                    break;
                default:
                    hole.Status = Hole.HoleStatus.Mole;
                    break;
            }

            if (skip == false)
            {
                health = maxHealth;
                isActive = true;

                FieldController.Instance.SwitchHoleGFX(hole);

                transform.position = hole.Position;

                animator.SetTrigger(dig);

                yield return new WaitForSeconds(waitForAnimationsEnd);

                hole.Status = Hole.HoleStatus.Empty;

                isActive = false;

                health = maxHealth;

                transform.position = Vector3.one * -3;

                FieldController.Instance.SwitchHoleGFX(hole);

                hole = FieldController.Instance.Field.GetNewHole();

                if (hole == null)
                    hole = FieldController.Instance.Field.GetNewHole();

                lastHoleStatus = hole.Status;
            } 
        }
    }

    protected override void NormalHit(Mole mole)
    {
        if (mole == this)
        {
            health--;

            if (health <= 0)
            {
                damageEffect.SetActive(true);
                PowerUpSelector.instance.AdjustCoins(2);
                PowerUpSelector.instance.AddNormalMole();
                base.NormalHit(mole);
            }
        }
    }
}
