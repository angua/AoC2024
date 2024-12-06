namespace AoC2024.Days.Day06;

internal class Day06Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 6,
            Title = "",
            // Image,
            CreateViewModel = () => new Day06ViewModel(),
            ViewType = typeof(Day06Control)
        };
    }
}
