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

    private void Start()
    {
        dig = "Elite";
        hit = "EliteHit";
        water = "EliteWater";
        
        health = maxHealth;

        hole = FieldController.Instance.Field.GetRandomHole();
        hole.Status = Hole.HoleStatus.Empty;

        animator = GetComponentInChildren<Animator>();

        StartCoroutine(SetNewHole());

        PlayerInput.MoleHitted += NormalHit;
        PlayerInput.WaterPowerUp += WaterHit;
    }

    protected override IEnumerator SetNewHole()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnNextTime);

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
                    WaterHit();
                    break;
                case Hole.HoleStatus.Mole:
                    hole = FieldController.Instance.Field.GetRandomHole();
                    yield return null;
                    continue;
                default:
                    hole.Status = Hole.HoleStatus.Mole;
                    break;
            }


            health = maxHealth;

            if (hole.Status == Hole.HoleStatus.Plank)
            {
                FieldController.Instance.AnimatePlank(hole, "Break");
                StartCoroutine(FieldController.Instance.ResetHole(hole, Hole.HoleStatus.Empty));
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
            }

            if (health <= 0)
            { 
                damageEffect.SetActive(true);
                base.NormalHit(mole);
            }
        }

    }
}
