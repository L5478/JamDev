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
    public Hole Hole { get => hole; set => hole = value; }

    protected FieldController Instance;

    protected virtual IEnumerator SetNewHole()
    {
        yield return null;
    }

    public void KillMole(Mole mole)
    {
        if (mole == this && isActive == true)
        {
            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger(hit);

            hole = Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    protected virtual void NormalHit(Mole mole)
    {
        if (mole == this && isActive == true)
        {
            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger(hit);

            Instance.SwitchHoleGFX(hole);

            hole = Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    protected void WaterHit(Mole mole)
    {
        if (isActive == true)
        {
            hole.Status = Hole.HoleStatus.Water;

            isActive = false;

            if(mole != null)
                StopAllCoroutines();

            animator.SetTrigger(water);

            PowerUpSelector.instance.AdjustCoins(1);

            PowerUpSelector.instance.AddWaterHosed();

            Instance.SwitchHoleGFX(hole);

            hole = Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }
}
