using Advent2024.Utils;
using AoC2024Lib.Days.Day06Lib;
using CommonWPF;

namespace AoC2024.Days.Day06;

public class Day06ViewModel : ViewModelBase
{
    public Day06Work Worker { get; } = new();

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

    public Day06ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day06", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
