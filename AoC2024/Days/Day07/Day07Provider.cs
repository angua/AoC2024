namespace AoC2024.Days.Day07;

internal class Day07Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 7,
            Title = "Title",
            // Image,
            CreateViewModel = () => new Day07ViewModel(),
            ViewType = typeof(Day07Control)
        };
    }
}
