using Common;

namespace AoC2024Lib.Days.Day02Lib;

public class Day02Work
{
    public int CalculatePart1Solution(Filedata fileData)
    {
        var safecount = 0;
        foreach (var line in fileData.Lines)
        {
            var levels = ParseLevels(line);

            if (Checksafe(levels))
            {
                safecount++;
            }
        }
        return safecount;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        var safecount = 0;
        foreach (var line in fileData.Lines)
        {
            var levels = ParseLevels(line);

            if (Checksafe(levels))
            {
                safecount++;
            }
            
            else
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    var testLevels = new List<int>(levels);
                    testLevels.RemoveAt(i);
                    var safenow = Checksafe(testLevels);
                    if (safenow)
                    {
                        safecount++;
                        break;
                    }
                }
            }
        }
        return safecount;
    }

    private static List<int> ParseLevels(string line)
    {
        var levels = new List<int>();
        var parts = line.Split(' ');
        foreach (var part in parts)
        {
            levels.Add(int.Parse(part));
        }
        return levels;
    }

    private static bool Checksafe(List<int> levels)
    {
        bool? increasing = null;
        var safe = true;

        for (int i = 0; i < levels.Count - 1; i++)
        {
            var diff = levels[i + 1] - levels[i];

            if (diff > 0)
            {
                if (increasing == false)
                {
                    safe = false;
                    break;
                }
                increasing = true;
            }
            else if (diff < 0)
            {
                if (increasing == true)
                {
                    safe = false;
                    break;
                }
                increasing = false;
            }
            else
            {
                safe = false;
                break;
            }

            if (Math.Abs(diff) > 3 || Math.Abs(diff) < 1)
            {
                // not safe
                safe = false;
                break;
            }
        }
        return safe;
    }
}
