using System;
using UnityEngine;

public class Hole
{
    public enum HoleStatus { None, Empty, NewHole, Mole, Plank, Water, Exploded, }

    private HoleStatus status = HoleStatus.None;
    private int x;
    private int z;
    private Vector3 position;
    private Field field;

    public HoleStatus Status
    {
        get => status; set
        {
            status = value;
            HoleStatusChange?.Invoke();
        }
    }
    public Vector3 Position { get => position; set => position = value; }
    public int X { get => x; }
    public int Z { get => z; }

    public static event Action HoleStatusChange;

    public Hole(int x, int z, Field field)
    {
        this.x = x;
        this.z = z;
        this.field = field;

        // Set starting area 3x3 in the middle
        if (this.x >= (field.Width - 2) /2 && this.x <= (field.Width + 2) / 2)
        {
            if (this.z >= (field.Depth - 2) / 2 && this.z <= (field.Depth + 2) / 2)
            {
                status = HoleStatus.Empty;
            }
        }
    }
}
