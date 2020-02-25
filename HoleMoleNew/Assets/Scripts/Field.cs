using UnityEngine;
using System.Collections.Generic;

public class Field
{
    private Hole[,] holes;

    private int width;
    private int depth;
    private int holesAmount = 0;

    public int Width { get => width; }
    public int Depth { get => depth; }
    public int HolesAmount { get => holesAmount; }

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
                holesAmount++;
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
    public Hole GetEmptyHole(bool acceptStatusMole = false)
    {
        List<Hole> hole_list = new List<Hole>();

        foreach (Hole hole in holes)
        {
            if (hole.Status == Hole.HoleStatus.Empty)
                hole_list.Add(hole);

            if (acceptStatusMole && hole.Status == Hole.HoleStatus.Mole)
                hole_list.Add(hole);
        }

        if (hole_list.Count > 0)
            return hole_list[Random.Range(0, hole_list.Count)];
        else
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

        if (hole_list.Count == 0)
            return null;

        return hole_list[Random.Range(0, hole_list.Count)];
    }


    public Hole GetNewHole()
    {
        List<Hole> availableHoles = new List<Hole>();
        List<Hole> noneHoles = new List<Hole>();
        Hole newHole = null;
        bool flag = true;

        foreach (Hole hole in holes)
        {
            if (hole.Status == Hole.HoleStatus.Empty)
                availableHoles.Add(hole);

            if (hole.Status == Hole.HoleStatus.Plank)
                availableHoles.Add(hole);

            if (hole.Status == Hole.HoleStatus.None)
                noneHoles.Add(hole);
        }

        do
        {
            int rndIndex = Random.Range(0, availableHoles.Count);

            foreach (Hole hole in noneHoles)
            {
                if (hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||
                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||
                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||
                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||

                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||
                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||
                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||
                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z - 1 ||


                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||
                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||
                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||
                 hole.X == availableHoles[rndIndex].X + 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||

                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||
                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||
                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z + 1 ||
                 hole.X == availableHoles[rndIndex].X - 1 && hole.Z == availableHoles[rndIndex].Z + 1)
                {
                    flag = false;
                    newHole = hole;
                }
            }

            availableHoles.Remove(availableHoles[rndIndex]);

            if (availableHoles.Count <= 0)
            {
                flag = false;
            }

        } while (flag);

        return newHole;
    }

    public int GetHoleCount(Hole.HoleStatus holeStatus)
    {
        int value = 0;

        foreach (Hole hole in holes)
        {
            if (hole.Status == holeStatus)
                value++;
        }

        return value;
    }

    public bool IsThereHolesInRow(int row)
    {
        foreach (Hole hole in holes)
        {
            if (hole.Status != Hole.HoleStatus.None && hole.Z == row)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsThereHolesInColum(int colum)
    {
        foreach (Hole hole in holes)
        {
            if (hole.Status != Hole.HoleStatus.None && hole.X == colum)
            {
                return true;
            }
        }
        return false;
    }
}
