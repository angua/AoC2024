using System.Numerics;

namespace AoC2024Lib.Days.Day16Lib;

public class Crossroad
{
    public Vector2 Position { get; set; }
    public List<Connection> Connections { get; set; } = new();

    public long PointsFromStart { get; set; } = -1;
    public Vector2 EnterDirection { get; set; }

    public List<List<Connection>> Incoming = new();


}