using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private Hole hole;
    private FieldController fieldController;

    private void Start()
    {
        fieldController = FindObjectOfType<FieldController>();
        hole = fieldController.Field.GetEmptyHole();

        transform.position = hole.Position;
    }
}
