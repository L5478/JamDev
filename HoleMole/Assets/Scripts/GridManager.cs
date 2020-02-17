using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject hole;

    void Start()
    {
        Grid grid = new Grid(10, 10, 2f, hole);
    }
}
