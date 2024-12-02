using Advent2024.Utils;
using AoC2024Lib.Days.Day02Lib;
using CommonWPF;

namespace AoC2024.Days.Day02;

public class Day02ViewModel : ViewModelBase
{

    public Day02Work Worker { get; } = new();

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

    public Day02ViewModel()
        {
            var fileData = ResourceUtils.LoadDataFromResource("Day02", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);

    }
}
