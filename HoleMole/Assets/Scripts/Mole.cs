using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public float waitTime = 1f;
    public float spawnTime = 2f;
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    private Hole hole;

    private void Start()
    {
        hole = FieldController.Instance.Field.GetRandomHole();

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

            transform.position = hole.Position + offset;

            yield return new WaitForSeconds(waitTime);

            transform.position = Vector3.one * -3;

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();

            if (hole == null)
                hole = FieldController.Instance.Field.GetRandomHole();
        }
    }

    private void NormalHit(Transform mole)
    {
        if (mole == this.transform)
        {
            StopAllCoroutines();

            transform.position = Vector3.one * - 3;

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }

    private void WaterHit(Transform mole)
    {
        StopAllCoroutines();

        transform.position = Vector3.one * -3;

        hole.Status = Hole.HoleStatus.Empty;
        FieldController.Instance.SwitchHoleGFX(hole);

        hole = FieldController.Instance.Field.GetRandomHole();

        StartCoroutine(SetNewHole());
    }
}
