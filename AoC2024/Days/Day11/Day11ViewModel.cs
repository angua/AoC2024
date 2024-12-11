using Advent2024.Utils;
using AoC2024Lib.Days.Day11Lib;
using CommonWPF;

namespace AoC2024.Days.Day11;

public class Day11ViewModel : ViewModelBase
{
	public Day11Work Worker { get; } = new();

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

	public Day11ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day11", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
