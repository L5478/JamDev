using UnityEngine;

public class Hole
{
    public enum HoleStatus { None, Empty, Mole, Plank, Water, Fire, }

    private HoleStatus status = HoleStatus.None;
    private int x;
    private int z;
    private World world;

    public HoleStatus Status { get => status; set => status = value; }

    public Hole(int x, int z, World world)
    {
        this.x = x;
        this.z = z;
        this.world = world;

        if (this.x == (world.Width - 1)/2 )
        {
            if (this.z == (world.Depth - 1) / 2)
            {
                status = HoleStatus.Empty;
                Debug.Log("Hole status Empty");
            }
        }
    }
}
