using Advent2024.Utils;
using AoC2024Lib.Days.Day17Lib;
using CommonWPF;

namespace AoC2024.Days.Day17;

public class Day17ViewModel : ViewModelBase
{
	public Day17Work Worker { get; } = new();

	public string Part1Solution
	{
		get => GetValue<string>();
		set => SetValue(value);
	}
	public long Part2Solution
	{
		get => GetValue<long>();
		set => SetValue(value);
	}

	public Day17ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day17", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
