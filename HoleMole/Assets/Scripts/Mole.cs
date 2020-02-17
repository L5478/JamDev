using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    private Hole hole;

    private void Start()
    {
        hole = FieldController.Instance.Field.GetRandomHole();

        StartCoroutine(SetNewHole());
    }

    private IEnumerator SetNewHole()
    {
        while (true)
        {
            hole.Status = Hole.HoleStatus.Mole;
            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position + offset;

            yield return new WaitForSeconds(.5f);

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);

            hole = FieldController.Instance.Field.GetNewHole();
        }
    }
}
