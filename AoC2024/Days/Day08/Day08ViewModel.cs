using Advent2024.Utils;
using AoC2024Lib.Days.Day08Lib;
using CommonWPF;

namespace AoC2024.Days.Day08;

public class Day08ViewModel : ViewModelBase
{
    public Day08Work Worker { get; } = new();

    public long Part1Solution
    {
        get => GetValue<long>();
        set => SetValue(value);
    }
    public long Part2Solution
    {
        get => GetValue<long>();
        set => SetValue(value);
    }

    public Day08ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day08", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
