using Advent2024.Utils;
using AoC2024Lib.Days.Day12Lib;
using CommonWPF;

namespace AoC2024.Days.Day12;

public class Day12ViewModel : ViewModelBase
{
	public Day12Work Worker { get; } = new();

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

	public Day12ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day12", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
