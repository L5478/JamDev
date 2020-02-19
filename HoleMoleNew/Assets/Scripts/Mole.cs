using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float waitTime = 3f;
    public float spawnTime = 2f;
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    private Hole hole;
    private Animator animator;
    private bool isActive = false;

    private void Start()
    {
        hole = FieldController.Instance.Field.GetRandomHole();
        animator = GetComponentInChildren<Animator>();

        StartCoroutine(SetNewHole());

        PlayerInput.moleHitted += NormalHit;
        PlayerInput.waterPowerUP += WaterHit;
    }

    private IEnumerator SetNewHole()
    {
        while (true)
        {
            if (hole.Status == Hole.HoleStatus.None)
                hole.Status = Hole.HoleStatus.Mole;
            else
                hole.Status = Hole.HoleStatus.Empty;

            yield return new WaitForSeconds(spawnTime);
            isActive = true;

            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            animator.SetTrigger("Normal");

            yield return new WaitForSeconds(waitTime);

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
            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger("Hit");

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    private void WaterHit()
    {
        if (isActive == true)
        {
            isActive = false;
            StopAllCoroutines();

            animator.SetTrigger("Water");

            hole.Status = Hole.HoleStatus.Water;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());

        }
    }
}
