using System.Numerics;
using Advent2024.Utils;
using AoC2024Lib.Days.Day18Lib;
using CommonWPF;

namespace AoC2024.Days.Day18;

public class Day18ViewModel : ViewModelBase
{
	public Day18Work Worker { get; } = new();

	public long Part1Solution
	{
		get => GetValue<long>();
		set => SetValue(value);
	}
	public Vector2 Part2Solution
	{
		get => GetValue<Vector2>();
		set => SetValue(value);
	}

	public Day18ViewModel()
	{
		var fileData = ResourceUtils.LoadDataFromResource("Day18", "input.txt");
		
		Part1Solution = Worker.CalculatePart1Solution(fileData);
		Part2Solution = Worker.CalculatePart2Solution(fileData);
	}
}
