using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day14Lib;

public class Day14Work
{
    public List<Robot> Robots = new();

    private Filedata _fileData;

    //  101 tiles wide and 103 tiles tall
    public int RoomWidth { get; } = 101;
    public int RoomHeight { get; } = 103;

    public long CalculatePart1Solution(Filedata fileData)
    {
        _fileData = fileData;
        // p=73,39 v=-17,38

        ParseRobots();

        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in Robots)
            {
                robot.Move(RoomWidth, RoomHeight);
            }
        }

        var middleWidth = RoomWidth / 2;
        var middleHeight = RoomHeight / 2;

        var topLeft = Robots.Where(r => r.Position.X < middleWidth && r.Position.Y < middleHeight);
        var bottomLeft = Robots.Where(r => r.Position.X < middleWidth && r.Position.Y > middleHeight);
        var topRight = Robots.Where(r => r.Position.X > middleWidth && r.Position.Y < middleHeight);
        var bottomRight = Robots.Where(r => r.Position.X > middleWidth && r.Position.Y > middleHeight);

        return topLeft.Count() * topRight.Count() * bottomLeft.Count() * bottomRight.Count();
    }

    public void ParseRobots()
    {
        Robots.Clear();
        foreach (var line in _fileData.Lines)
        {
            var robot = new Robot();
            Robots.Add(robot);

            var parts = line.Split(' ');

            var posParts = parts[0].Split("=");
            var posCoords = posParts[1].Split(",");
            robot.Position = new Vector2(int.Parse(posCoords[0]), int.Parse(posCoords[1]));

            var velParts = parts[1].Split("=");
            var velCoords = velParts[1].Split(",");
            robot.Velocity = new Vector2(int.Parse(velCoords[0]), int.Parse(velCoords[1]));
        }
    }


    public void NextSecond()
    {
        foreach (var robot in Robots)
        {
            robot.Move(RoomWidth, RoomHeight);
        }
    }

    public void PreviousSecond()
    {
        foreach (var robot in Robots)
        {
            robot.MoveBack(RoomWidth, RoomHeight);
        }
    }

    public void DoSeconds(int secondsInput)
    {
        for (int i = 0; i < secondsInput; i++)
        {
            NextSecond();
        }
    }
}
