using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    private Hole[,] holes;

    private int width;
    private int depth;
    private int holesAmount;

    public int Width { get => width; }
    public int Depth { get => depth; }

    public Field(int width = 3, int depth = 3)
    {
        this.width = width;
        this.depth = depth;

        holes = new Hole[width, depth];

        // Create Holes
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                holes[x, z] = new Hole(x, z, this);
            }
        }
    }

    // Get Hole at given coordinates
    public Hole GetHoleAt(int x, int z)
    {
        return holes[x, z];
    }
}
