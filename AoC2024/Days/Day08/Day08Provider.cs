namespace AoC2024.Days.Day08;

internal class Day08Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 8,
            Title = "Title",
            // Image,
            CreateViewModel = () => new Day08ViewModel(),
            ViewType = typeof(Day08Control)
        };
    }
}
