using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day05Lib;

public class Day05Work
{
    private List<(int, int)> _orderingRules = new List<(int, int)>();
    private List<List<int>> _updates = new();
    private List<List<int>> _incorrectUpdates = new();

    public int CalculatePart1Solution(Filedata fileData)
    {
        var i = 0;
        for (; i < fileData.Lines.Count; i++)
        {
            var line = fileData.Lines[i];
            if (string.IsNullOrEmpty(line)) break;

            var parts = line.Split('|');
            _orderingRules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }

        i++;

        for (; i < fileData.Lines.Count; i++)
        {
            var line = fileData.Lines[i];
            var parts = line.Split(',');
            _updates.Add(parts.Select(p => int.Parse(p)).ToList());
        }

        var correctRules = 0;

        foreach (var update in _updates)
        {
            bool rightorder = CheckRightOrder(update);
            if (rightorder)
            {
                var middleNumber = update[update.Count / 2];
                correctRules += middleNumber;
            }
            else
            {
                _incorrectUpdates.Add(update);
            }
        }

        return correctRules;
    }

    private bool CheckRightOrder(List<int> update)
    {
        var rightorder = true;
        for (int n = 0; n < update.Count; n++)
        {
            var num = update[n];
            var remaining = update.Skip(n + 1).ToList();

            var rules = _orderingRules.Where(r => r.Item2 == num);

            foreach (var rule in rules)
            {
                if (remaining.Contains(rule.Item1))
                {
                    rightorder = false;
                    break;
                }
            }
        }

        return rightorder;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        var sum = 0;
        foreach (var update in _incorrectUpdates)
        {
            var testUpdate = new List<int>(update);

            while (!CheckRightOrder(testUpdate))
            {
                var newUpdate = new List<int>(testUpdate);
                for (int n = 0; n < testUpdate.Count; n++)
                {
                    var swapDone = false;

                    var num = testUpdate[n];
                    var remaining = testUpdate.Skip(n + 1).ToList();

                    var rules = _orderingRules.Where(r => r.Item2 == num);

                    foreach (var rule in rules)
                    {
                        if (remaining.Contains(rule.Item1))
                        {
                            var otherpos = testUpdate.IndexOf(rule.Item1);

                            newUpdate[n] = rule.Item1;
                            newUpdate[otherpos] = rule.Item2;
                            swapDone = true;
                            break;

                        }
                    }
                    if (swapDone)
                    {
                        break;
                    }

                }
                testUpdate = newUpdate;
            }

            // is in right order
            var middleNumber = testUpdate[testUpdate.Count / 2];
            sum += middleNumber;

        }

        return sum;
    }

}
