using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMole : Mole
{
    public GameObject helmet;
    public GameObject helmetBreakEffect;

    private float health;
    private float maxHealth = 2f;
    private bool skip = false;

    private MoleSoundEffects soundEffects;

    protected override void Start()
    {
        type = MoleType.Elite;

        dig = "Elite";
        hit = "EliteHit";
        water = "EliteWater";
        
        health = maxHealth;

        hole = FieldController.Instance.Field.GetRandomHole();
        hole.Status = Hole.HoleStatus.Empty;

        StartCoroutine(SetNewHole());

        animator = GetComponentInChildren<Animator>();
        soundEffects = GetComponentInChildren<MoleSoundEffects>();

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
                    dig = "EliteDig";
                    break;
                case Hole.HoleStatus.Water:
                    WaterHit(this);
                    break;
                case Hole.HoleStatus.Mole:
                case Hole.HoleStatus.NewHole:
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

                if (hole.Status == Hole.HoleStatus.Plank)
                {
                    FieldController.Instance.AnimatePlank(hole, "Break");
                    StartCoroutine(FieldController.Instance.ResetHole(hole, Hole.HoleStatus.Empty, 1.5f));
                }

                helmet.SetActive(true);

                isActive = true;

                FieldController.Instance.SwitchHoleGFX(hole);

                transform.position = hole.Position;

                animator.SetTrigger(dig);

                yield return new WaitForSeconds(waitForAnimationsEnd);

                dig = "Elite";

                isActive = false;

                health = maxHealth;

                transform.position = Vector3.one * -3;

                FieldController.Instance.SwitchHoleGFX(hole);

                hole = FieldController.Instance.Field.GetNewHole();

                if (hole == null)
                    hole = FieldController.Instance.Field.GetRandomHole();

                lastHoleStatus = hole.Status;
            }
        }
    }

    protected override void NormalHit(Mole mole)
    {
        if (mole == this)
        {
            health--;

            if (health == 1 && helmet != null)
            {
                helmet.SetActive(false);
                helmetBreakEffect.SetActive(true);
                soundEffects.PlayHelmethitSound();
            }

            if (health <= 0)
            { 
                damageEffect.SetActive(true);
                PowerUpSelector.instance.AdjustCoins(5);
                PowerUpSelector.instance.AddEliteMole();
                base.NormalHit(mole);
            }
        }
    }
}
