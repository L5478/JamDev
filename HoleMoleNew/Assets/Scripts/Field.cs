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
    public Hole GetHoleAtCoordinates(int x, int z)
    {
        return holes[x, z];
    }

    // Get Hole at given position
    public Hole GetHoleAtPosition(Vector3 position)
    {
        foreach (Hole hole in holes)
        {
            if (hole.Position == position)
            {
                return hole;
            }
        }

        return null;
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

    // Get randomly hole which status is not None or Mole
    public Hole GetRandomHole()
    {
        List<Hole> hole_list = new List<Hole>();

        foreach (Hole hole in holes)
        {
            if (hole.Status != Hole.HoleStatus.Mole && 
                hole.Status != Hole.HoleStatus.None && 
                hole.Status != Hole.HoleStatus.Exploded && 
                hole.Status != Hole.HoleStatus.NewHole)
                hole_list.Add(hole);
        }

        return hole_list[Random.Range(0, hole_list.Count)];
    }


    public Hole GetNewHole()
    {
        List<Hole> emptyHoles = new List<Hole>();
        List<Hole> noneHoles = new List<Hole>();
        Hole newHole = null;

        foreach (Hole hole in holes)
        {
            if (hole.Status == Hole.HoleStatus.Empty)
                emptyHoles.Add(hole);

            if (hole.Status == Hole.HoleStatus.Plank)
                emptyHoles.Add(hole);

            if (hole.Status == Hole.HoleStatus.None)
                noneHoles.Add(hole);
        }

        bool flag = true;
          
        do
        {
            int rndIndex = Random.Range(0, emptyHoles.Count);

            foreach (Hole hole in noneHoles)
            {
                if (hole.X == emptyHoles[rndIndex].X -1 && hole.Z == emptyHoles[rndIndex].Z -1||
                 hole.X == emptyHoles[rndIndex].X -1 && hole.Z == emptyHoles[rndIndex].Z -1 ||
                 hole.X == emptyHoles[rndIndex].X -1 && hole.Z == emptyHoles[rndIndex].Z -1 ||
                 hole.X == emptyHoles[rndIndex].X -1 && hole.Z == emptyHoles[rndIndex].Z -1 ||

                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z - 1 ||
                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z - 1 ||
                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z - 1 ||
                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z - 1 ||


                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||
                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||
                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||
                 hole.X == emptyHoles[rndIndex].X + 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||

                 hole.X == emptyHoles[rndIndex].X - 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||
                 hole.X == emptyHoles[rndIndex].X - 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||
                 hole.X == emptyHoles[rndIndex].X - 1 && hole.Z == emptyHoles[rndIndex].Z + 1 ||
                 hole.X == emptyHoles[rndIndex].X - 1 && hole.Z == emptyHoles[rndIndex].Z + 1)
                {
                    flag = false;
                    newHole = hole;
                }
            }

            emptyHoles.Remove(emptyHoles[rndIndex]);

            if (emptyHoles.Count <= 0)
            {
                flag = false;
            }

        } while (flag);

        return newHole;
    }
}
