using System.Numerics;

namespace AoC2024Lib.Days.Day18Lib;

internal class Path
{
    public Vector2 Position {  get; set; }
    public int Steps { get; set; }
    public Path? PreviousPath { get; set; }
}
