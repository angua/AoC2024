using System.Numerics;

namespace AoC2024Lib.Days.Day14Lib;

public class Robot
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    internal void Move(int roomWidth, int roomHeight)
    {
        var newPos = Position + Velocity;
        if (newPos.X >= roomWidth)
        {
            newPos.X -= roomWidth;
        }
        else if (newPos.X < 0 )
        {
            newPos.X += roomWidth;

        }
        if (newPos.Y >= roomHeight)
        {
            newPos.Y -= roomHeight;
        }
        else if (newPos.Y < 0)
        {
            newPos.Y += roomHeight;

        }
        Position = newPos;
    }

    internal void MoveBack(int roomWidth, int roomHeight)
    {
        var newPos = Position - Velocity;
        if (newPos.X >= roomWidth)
        {
            newPos.X -= roomWidth;
        }
        else if (newPos.X < 0)
        {
            newPos.X += roomWidth;

        }
        if (newPos.Y >= roomHeight)
        {
            newPos.Y -= roomHeight;
        }
        else if (newPos.Y < 0)
        {
            newPos.Y += roomHeight;

        }
        Position = newPos;
    }
}
