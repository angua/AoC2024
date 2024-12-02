namespace AoC2024.Days.Day01;

internal class Day01Provider : IDayProvider
{
    public Day GetDay()
    {
        return new Day
        {
            DayNumber = 1,
            Title = "Historian Hysteria",
            // Image,
            CreateViewModel = () => new Day01ViewModel(),
            ViewType = typeof(Day01Control)
        };
    }
}
