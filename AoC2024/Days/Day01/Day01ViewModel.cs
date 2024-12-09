using Advent2024.Utils;
using AoC2024Lib.Days.Day01Lib;
using CommonWPF;

namespace AoC2024.Days.Day01;

public class Day01ViewModel : ViewModelBase
{
    public Day01Work Worker { get; } = new();

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

    public Day01ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day01", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}

