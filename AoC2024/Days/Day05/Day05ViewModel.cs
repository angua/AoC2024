using Advent2024.Utils;
using AoC2024Lib.Days.Day05Lib;
using CommonWPF;

namespace AoC2024.Days.Day05;

public class Day05ViewModel : ViewModelBase
{
    public Day05Work Worker { get; } = new();

    public int Part1Solution
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    public int Part2Solution
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    public Day05ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day05", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
