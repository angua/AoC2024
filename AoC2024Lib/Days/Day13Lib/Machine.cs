using System.Numerics;

namespace AoC2024Lib.Days.Day13Lib;

public class Machine
{
    public Vector2 ButtonA { get; set; }
    public Vector2 ButtonB { get; set; }
    public Vector2 Prize { get; set; }

    public long RealPrizeX { get; set; }
    public long RealPrizeY { get; set; }

    public int CountA { get; set; } = 0;
    public int CountB { get; set; } = 0;
    public int Tokens { get; set; } = 0;

    public long RealCountA { get; set; } = 0;
    public long RealCountB { get; set; } = 0;
    public long RealTokens { get; set; } = 0;
}
