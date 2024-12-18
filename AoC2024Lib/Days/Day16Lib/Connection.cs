using System.Numerics;

namespace AoC2024Lib.Days.Day16Lib;

public class Connection
{
    public List<Vector2> Positions { get; set; } = new();
    public int Points { get; set; }

    public Crossroad? CrossRoad1 { get; set; }
    public Vector2 InDirection1 { get; set; }

    public Crossroad? CrossRoad2 { get; set; }
    public Vector2 InDirection2 { get; set; }
}
