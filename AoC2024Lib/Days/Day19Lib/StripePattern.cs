namespace AoC2024Lib.Days.Day19Lib;

public class StripePattern
{
    public string Pattern { get; set; }
    public List<(string towel, StripePattern remainingSequence)> RemainingSequences { get; set; } = new();
    public long SequenceCount { get; set; }
}
