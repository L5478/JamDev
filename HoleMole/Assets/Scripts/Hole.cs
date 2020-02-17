using UnityEngine;

public class Hole
{
    public enum HoleStatus { None, Empty, Mole, Plank, Water, Fire, }

    private HoleStatus status = HoleStatus.None;
    private int x;
    private int z;
    private Field world;

    public HoleStatus Status { get => status; set => status = value; }

    public Hole(int x, int z, Field world)
    {
        this.x = x;
        this.z = z;
        this.world = world;

        // Set starting area 3x3 in the middle
        if (this.x >= (world.Width - 2) /2 && this.x <= (world.Width + 2) / 2)
        {
            if (this.z >= (world.Depth - 2) / 2 && this.z <= (world.Depth + 2) / 2)
            {
                status = HoleStatus.Empty;
            }
        }
    }
}
