namespace AoC2024.Days.Day10;

internal class Day10Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 10,
            Title = "Title",
            // Image,
            CreateViewModel = () => new Day10ViewModel(),
            ViewType = typeof(Day10Control)
        };
    }
}
