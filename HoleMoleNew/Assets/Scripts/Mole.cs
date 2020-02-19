using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float waitTime = 3f;
    public float spawnTime = 2f;

    private Hole hole;
    private Animator animator;
    private bool isActive = false;
    private Hole.HoleStatus currentHoleStatus;

    private void Start()
    {
        hole = FieldController.Instance.Field.GetRandomHole();
        hole.Status = Hole.HoleStatus.Mole;

        animator = GetComponentInChildren<Animator>();

        StartCoroutine(SetNewHole());

        PlayerInput.MoleHitted += NormalHit;
        PlayerInput.WaterPowerUp += WaterHit;
    }

    private IEnumerator SetNewHole()
    {
        while (true)
        {
            if (hole.Status == Hole.HoleStatus.Water)
                WaterHit();
            else if (hole.Status == Hole.HoleStatus.None)
                hole.Status = Hole.HoleStatus.NewHole;
            else
                hole.Status = Hole.HoleStatus.Mole;

            yield return new WaitForSeconds(spawnTime);
            isActive = true;

            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            animator.SetTrigger("Normal");

            yield return new WaitForSeconds(waitTime);

            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;

            transform.position = Vector3.one * -3;

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();

            if (hole == null)
                hole = FieldController.Instance.Field.GetRandomHole();
        }
    }

    private void NormalHit(Mole mole)
    {
        if (mole == this && isActive == true)
        {
            hole.Status = Hole.HoleStatus.Empty;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger("Hit");

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    private void WaterHit()
    {
        if (isActive == true)
        {
            hole.Status = Hole.HoleStatus.Water;

            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger("Water");

            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());

        }
    }
}
