using Advent2024.Utils;
using AoC2024Lib.Days.Day04Lib;
using CommonWPF;

namespace AoC2024.Days.Day04;

public class Day04ViewModel : ViewModelBase
{
    public Day04Work Worker { get; } = new();

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

    public Day04ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day04", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
