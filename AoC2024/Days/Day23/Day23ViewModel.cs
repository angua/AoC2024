using Advent2024.Utils;
using AoC2024Lib.Days.Day23Lib;
using CommonWPF;

namespace AoC2024.Days.Day23;

public class Day23ViewModel : ViewModelBase
{
	public Day23Work Worker { get; } = new();

	public long Part1Solution
	{
		get => GetValue<long>();
		set => SetValue(value);
	}
	public string Part2Solution
	{
		get => GetValue<string>();
		set => SetValue(value);
	}

	public Day23ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day23", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
