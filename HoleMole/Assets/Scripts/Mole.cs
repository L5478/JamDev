using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{


    private Hole hole;

    private void Start()
    {
        StartCoroutine(SetNewHole());
    }

    private IEnumerator SetNewHole()
    {
        while (true)
        {
            hole = FieldController.Instance.Field.GetRandomHole();

            hole.Status = Hole.HoleStatus.Mole;
            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            yield return new WaitForSeconds(2);

            hole.Status = Hole.HoleStatus.Empty;
            FieldController.Instance.SwitchHoleGFX(hole);
        }
    }
}
