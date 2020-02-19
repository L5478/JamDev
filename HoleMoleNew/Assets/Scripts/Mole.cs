using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float waitTime = 3f;
    public float spawnTime = 2f;

    protected Hole hole;
    protected Animator animator;
    protected bool isActive = false;

    protected float health;
    protected float maxHealth;

    protected string dig;
    protected string hit;
    protected string water;

    protected virtual IEnumerator SetNewHole()
    {
        yield return null;
    }

    protected void NormalHit(Mole mole)
    {
        health--;

        if (mole == this && isActive == true && health <= 0)
        {
            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger(hit);

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    protected void WaterHit()
    {
        if (isActive == true)
        {
            hole.Status = Hole.HoleStatus.Water;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger(water);

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }
}
