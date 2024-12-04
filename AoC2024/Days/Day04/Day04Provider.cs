namespace AoC2024.Days.Day04;

internal class Day04Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 4,
            Title = "",
            // Image,
            CreateViewModel = () => new Day04ViewModel(),
            ViewType = typeof(Day04Control)
        };
    }
}
