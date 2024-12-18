using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day18Lib;

public class Day18Work
{
    private List<Vector2> _bytes = new();
    private HashSet<Vector2> _fallenBytes = new();

    private Vector2 _startPosition;
    private Vector2 _endPosition;


    public int CalculatePart1Solution(Filedata fileData)
    {
        foreach (var line in fileData.Lines)
        {
            var parts = line.Split(',');
            _bytes.Add(new Vector2(int.Parse(parts[0]), int.Parse(parts[1])));
        }

        _fallenBytes = _bytes.Take(1024).ToHashSet();

        _startPosition = new Vector2(0, 0);
        _endPosition = new Vector2(70, 70);

        var startPath = new Path()
        {
            Position = _startPosition,
            Steps = 0
        };

        var available = new Queue<Path>();
        available.Enqueue(startPath);

        var visited = new HashSet<Vector2>();

        Path? bestPath = null;

        while (bestPath == null)
        {
            var current = available.Dequeue();

            foreach (var dir in MathUtils.OrthogonalDirections)
            {
                var nextPos = current.Position + dir;
                if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X > _endPosition.X || nextPos.Y > _endPosition.Y || _fallenBytes.Contains(nextPos) || visited.Contains(nextPos))
                {
                    continue;
                }
                visited.Add(nextPos);
                var nextPath = new Path()
                {
                    Position = nextPos,
                    Steps = current.Steps + 1,
                    PreviousPath = current,
                };

                if (nextPos == _endPosition)
                {
                    bestPath = nextPath;
                    break;
                }

                available.Enqueue(nextPath);
            }

        }
        return bestPath.Steps;
    }

    public Vector2 CalculatePart2Solution(Filedata fileData)
    {
        var fallingByte = 1024;

        var fallen = new HashSet<Vector2>(_fallenBytes);

        while (true)
        {
            fallen.Add(_bytes[fallingByte]);

            if (!CanReachExit(fallen))
            {
                break;
            }
            fallingByte++;
        }

        return _bytes[fallingByte];
    }

    private bool CanReachExit(HashSet<Vector2> fallen)
    {
        var available = new Queue<Vector2>();
        available.Enqueue(_startPosition);

        var visited = new HashSet<Vector2>();

        while (available.Count > 0)
        {
            var current = available.Dequeue();

            foreach (var dir in MathUtils.OrthogonalDirections)
            {
                var nextPos = current + dir;
                if (nextPos == _endPosition)
                {
                    return true;
                }
                if (nextPos.X < 0 || nextPos.Y < 0 || nextPos.X > _endPosition.X || nextPos.Y > _endPosition.Y || fallen.Contains(nextPos) || visited.Contains(nextPos))
                {
                    continue;
                }
                visited.Add(nextPos);
                available.Enqueue(nextPos);
            }

        }
        return false;
    }
}
