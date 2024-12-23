using Common;

namespace AoC2024Lib.Days.Day19Lib;

public class Day19Work
{
    public HashSet<string> AvailableTowels { get; set; } = new();
    public List<string> DesiredPatterns { get; set; } = new();

    public List<StripePattern> PossiblePatterns { get; set; } = new();

    private int _minLength;
    private int _maxLength;

    private HashSet<string> _invalidPatterns = new();

    private Dictionary<string, StripePattern> _sequences = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        var towelLine = fileData.Lines[0];
        var parts = towelLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
        AvailableTowels = parts.Select(t => t.Trim()).ToHashSet();

        for (var i = 2; i < fileData.Lines.Count; i++)
        {
            var line = fileData.Lines[i];
            DesiredPatterns.Add(line.Trim());
        }

        _minLength = AvailableTowels.Min(t => t.Length);
        _maxLength = AvailableTowels.Max(t => t.Length);

        var count = 0;
        foreach (var pattern in DesiredPatterns)
        {
            if (TryBuildSequences(pattern, out var stripePattern))
            {
                PossiblePatterns.Add(stripePattern);
                count++;
            }
        }
        return count;
    }

    private bool TryBuildSequences(string pattern, out StripePattern stripePattern)
    {
        stripePattern = new StripePattern();
        if (_invalidPatterns.Contains(pattern))
        {
            return false;
        }
        if (_sequences.ContainsKey(pattern))
        {
            stripePattern = _sequences[pattern];
            return true;
        }
        var maxLength = Math.Min(_maxLength, pattern.Length);

        long count = 0;
        var possibleCombinations = new List<(string, StripePattern)>();

        for (int i = _minLength; i <= maxLength; i++)
        {
            var testPattern = pattern.Substring(0, i);
            if (AvailableTowels.Contains(testPattern))
            {
                if (pattern.Length == testPattern.Length)
                {
                    var singlePattern = new StripePattern()
                    {
                        Pattern = pattern,
                        SequenceCount = 1,
                    };
                    _sequences.Add(pattern, singlePattern);
                    possibleCombinations.Add((pattern, singlePattern));
                    count++;
                }
                else
                {
                    var remainingPattern = pattern.Substring(i);
                    if (TryBuildSequences(remainingPattern, out var remainingsequnce))
                    {
                        possibleCombinations.Add((testPattern, remainingsequnce));
                        count += remainingsequnce.SequenceCount;
                    }
                    else
                    {
                        _invalidPatterns.Add(remainingPattern);
                    }
                }
            }
        }

        stripePattern.Pattern = pattern;
        stripePattern.SequenceCount = count;
        stripePattern.RemainingSequences = possibleCombinations;
        if (count > 0)
        {
            _sequences[pattern] = stripePattern;
            return true;
        }
        else
        {
            _invalidPatterns.Add(pattern);
            return false;
        }
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        long count = 0;

        for (int i = 0; i < PossiblePatterns.Count; i++)
        {
            count += PossiblePatterns[i].SequenceCount;
        }
        return count;
    }

}
