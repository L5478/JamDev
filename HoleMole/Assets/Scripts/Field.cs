using UnityEngine;
using System.Collections.Generic;

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

    // Get empty Hole
    public Hole GetEmptyHole()
    {
        foreach (Hole hole in holes)
        {
            if (hole.Status == Hole.HoleStatus.Empty)
                return hole;
        }

        return null;
    }


    public Hole GetRandomHole()
    {
        List<Hole> hole_list = new List<Hole>();

        foreach (Hole hole in holes)
        {
            if (hole.Status != Hole.HoleStatus.Mole && hole.Status != Hole.HoleStatus.None)
                hole_list.Add(hole);
        }

        return hole_list[Random.Range(0, hole_list.Count)];
    }


    public Hole GetNewHole()
    {
        foreach (Hole hole in holes)
        {
            if (hole.Status == Hole.HoleStatus.None)
                return hole;
        }

        return null;
    }
}
