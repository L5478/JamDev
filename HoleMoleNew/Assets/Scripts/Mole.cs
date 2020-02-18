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
            yield return new WaitForSeconds(spawnTime);

            hole.Status = Hole.HoleStatus.Mole;
            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            animator.SetTrigger("Normal");

            yield return new WaitForSeconds(waitTime);

            transform.position = Vector3.one * -3;

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();

            if (hole == null)
                hole = FieldController.Instance.Field.GetRandomHole();
        }
    }

    private void NormalHit(Mole mole)
    {
        if (mole == this)
        {
            StopAllCoroutines();

            transform.position = Vector3.one * - 3;

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    private void WaterHit()
    {
        StopAllCoroutines();

        transform.position = Vector3.one * -3;

        hole.Status = Hole.HoleStatus.Empty;
        FieldController.Instance.SwitchHoleGFX(hole);

        hole = FieldController.Instance.Field.GetRandomHole();

        StartCoroutine(SetNewHole());
    }
}
