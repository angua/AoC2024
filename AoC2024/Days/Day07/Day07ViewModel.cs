using Advent2024.Utils;
using AoC2024Lib.Days.Day07Lib;
using CommonWPF;

namespace AoC2024.Days.Day07;

public class Day07ViewModel : ViewModelBase
{
    public Day07Work Worker { get; } = new();

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

    public Day07ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day07", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
