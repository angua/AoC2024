using Advent2024.Utils;
using AoC2024Lib.Days.Day03Lib;
using CommonWPF;

namespace AoC2024.Days.Day03;

public class Day03ViewModel : ViewModelBase
{
    public Day03Work Worker { get; } = new();

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

    public Day03ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day03", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
