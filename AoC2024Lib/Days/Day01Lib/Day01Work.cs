using Common;

namespace AoC2024Lib.Days.Day01Lib;

public class Day01Work
{
    private List<int> _firstList = new();
    private List<int> _secondList = new();

    public int CalculatePart1Solution(Filedata fileData)
    {
        foreach (var line in fileData.Lines)
        {
            var parts = line.Split("   ");
            var first = parts[0].Trim();
            var second = parts[1].Trim();
            
            _firstList.Add(int.Parse(first));
            _secondList.Add(int.Parse(second));
        }

        var orderedFirst = _firstList.OrderBy(x => x).ToList();
        var orderedSecond = _secondList.OrderBy(x => x).ToList();

        var diffsum = 0;

        for (int i = 0; i < orderedFirst.Count; i++)
        {
            var diff = Math.Abs(orderedFirst[i] - orderedSecond[i]);
            diffsum += diff;
        }

        return diffsum;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        var multsum = 0;

        foreach (var num in _firstList)
        {
            var count =  _secondList.Where(n => n == num).Count();
            var mult = num * count;
            multsum += mult;
        }

        return multsum;
    }
}
