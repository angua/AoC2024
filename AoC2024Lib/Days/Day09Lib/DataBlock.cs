namespace AoC2024Lib.Days.Day09Lib;

internal class DataBlock
{
    public bool IsEmpty { get; set; }
    public int Id { get; set; } = -1;
    public int Count { get; set; }

    public bool Ignore { get; set; } = false;
}
