namespace AoC2024.Days.Day03;

internal class Day03Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 3,
            Title = "",
            // Image,
            CreateViewModel = () => new Day03ViewModel(),
            ViewType = typeof(Day03Control)
        };
    }
}
