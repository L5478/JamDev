using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldData", menuName = "ScriptableObjects/FieldData")]
public class FieldSizeSO : ScriptableObject
{
    public int holesX;
    public int holesZ;

    public int emptySpace;
}
