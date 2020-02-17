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

        PlayerInput.moleHitted += Hit;
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

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();

            if (hole == null)
                hole = FieldController.Instance.Field.GetRandomHole();
        }
    }

    private void Hit(Transform mole)
    {
        if (mole == this.transform)
        {
            StopAllCoroutines();

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetRandomHole();

            StartCoroutine(SetNewHole());
        }
    }
}
