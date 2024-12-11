using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day11Lib;

public class Day11Work
{
    private Dictionary<(long num, int blinks), long> _stoneCounts = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        var maxBlinks = 25;
        return GetAllStoneCount(fileData, maxBlinks);
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        var maxBlinks = 75;
        return GetAllStoneCount(fileData, maxBlinks);
    }

    private long GetAllStoneCount(Filedata fileData, int maxBlinks)
    {
        var input = fileData.Lines[0].Split(" ");
        var stones = input.Select(i => long.Parse(i));

        long sum = 0;
        foreach (var stone in stones)
        {
            var stoneCount = GetStoneCount(stone, maxBlinks, 0);
            sum += stoneCount;
        }
        return sum;
    }

    private long GetStoneCount(long stone, int blinks, int previousBlinks)
    {
        if (_stoneCounts.TryGetValue((stone, blinks), out var count))
        {
            return count;
        }

        var newStones = NextBlink(stone);

        if (blinks == 1)
        {
            if (newStones.Item2 == -1)
            {
                _stoneCounts[(stone, 1)] = 1;
                return 1;
            }
            else
            {
                _stoneCounts[(stone, 1)] = 2;
                return 2;
            }
        }

        long sum = GetStoneCount(newStones.Item1, blinks - 1, previousBlinks + 1);

        if (newStones.Item2 != -1)
        {
            sum += GetStoneCount(newStones.Item2, blinks - 1, previousBlinks + 1);
        }
        _stoneCounts[(stone, blinks)] = sum;

        return sum;
    }

    private static (long, long) NextBlink(long stone)
    {
        (long, long) newStones = new();

        if (stone == 0)
        {
            newStones.Item1 = 1;
            newStones.Item2 = -1;
        }
        else
        {
            var numstr = stone.ToString();
            if (numstr.Length % 2 == 0)
            {
                var firststr = numstr.Take(numstr.Length / 2);
                var secondstr = numstr.Skip(numstr.Length / 2);

                newStones.Item1 = long.Parse(string.Join("", firststr));
                newStones.Item2 = long.Parse(string.Join("", secondstr));
            }
            else
            {
                newStones.Item1 = (stone * 2024);
                newStones.Item2 = -1;
            }
        }
        
        return newStones;
    }

}
