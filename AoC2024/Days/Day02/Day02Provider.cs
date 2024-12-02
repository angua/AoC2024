namespace AoC2024.Days.Day02;

internal class Day02Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 2,
            Title = "Red-Nosed Reports",
            // Image,
            CreateViewModel = () => new Day02ViewModel(),
            ViewType = typeof(Day02Control)
        };
    }
}
