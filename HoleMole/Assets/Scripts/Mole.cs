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
        int count = 0;
        while (true)
        {
            hole = FieldController.Instance.Field.GetRandomHole();

            hole.Status = Hole.HoleStatus.Mole;
            //Debug.Log(hole.Status);
            FieldController.Instance.SwitchHoleGFX(hole);

            transform.position = hole.Position;

            count++;

            yield return new WaitForSeconds(2);

            hole.Status = Hole.HoleStatus.Empty;
            //Debug.Log(hole.Status);
            FieldController.Instance.SwitchHoleGFX(hole);
        }
    }
}
