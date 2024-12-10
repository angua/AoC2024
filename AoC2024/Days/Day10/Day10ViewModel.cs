using Advent2024.Utils;
using AoC2024Lib.Days.Day10Lib;
using CommonWPF;

namespace AoC2024.Days.Day10;

public class Day10ViewModel : ViewModelBase
{
	public Day10Work Worker { get; } = new();

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

	public Day10ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day10", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
