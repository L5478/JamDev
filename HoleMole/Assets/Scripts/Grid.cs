using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int rows, columns;
    [SerializeField]
    private float cellSize;
    private int[,] gridArray;

    private GameObject Prefab;
    public Vector3 origin;

    public Grid(int rows, int columns, float cellSize, GameObject Prefab)
    {
        this.rows = rows;
        this.columns = columns;
        this.cellSize = cellSize;

        gridArray = new int[rows, columns];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject obj = Instantiate(Prefab, new Vector3(origin.x + cellSize * x, origin.y, origin.x + cellSize * y), Quaternion.identity);
                obj.GetComponent<GridStatus>().x = x;
                obj.GetComponent<GridStatus>().y = y;
                if (x == gridArray.GetLength(0) / 2 && y == gridArray.GetLength(0) / 2)
                {

                } else
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
}
