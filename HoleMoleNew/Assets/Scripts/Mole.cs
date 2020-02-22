using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float waitForAnimationsEnd = 3f;
    public float spawnNextTime = 2f;
    public GameObject damageEffect;

    protected Hole hole;
    protected Animator animator;
    protected bool isActive = false;
    
    protected string dig;
    protected string hit;
    protected string water;

    public bool IsActive { get => isActive; }

    protected virtual IEnumerator SetNewHole()
    {
        yield return null;
    }

    protected virtual void NormalHit(Mole mole)
    {
        if (mole == this && isActive == true)
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

            PowerUpSelector.instance.AdjustCoins(1);

            PowerUpSelector.instance.AddWaterHosed();

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }
}
