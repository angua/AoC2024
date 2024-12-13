using Advent2024.Utils;
using AoC2024Lib.Days.Day13Lib;
using CommonWPF;

namespace AoC2024.Days.Day13;

public class Day13ViewModel : ViewModelBase
{
	public Day13Work Worker { get; } = new();

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

	public Day13ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day13", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
