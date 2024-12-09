using Advent2024.Utils;
using AoC2024Lib.Days.Day09Lib;
using CommonWPF;

namespace AoC2024.Days.Day09;

public class Day09ViewModel : ViewModelBase
{
	public Day09Work Worker { get; } = new();

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

	public Day09ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day09", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
