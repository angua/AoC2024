using System.Numerics;

namespace AoC2024Lib.Days.Day12Lib;
internal class GardenPosition
{
    public Vector2 Position { get; set; }
    public List<Vector2> Perimeters { get; set; } = new();
}
