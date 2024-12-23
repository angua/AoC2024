using Advent2024.Utils;
using AoC2024Lib.Days.Day19Lib;
using CommonWPF;

namespace AoC2024.Days.Day19;

public class Day19ViewModel : ViewModelBase
{
	public Day19Work Worker { get; } = new();

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

	public Day19ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day19", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
