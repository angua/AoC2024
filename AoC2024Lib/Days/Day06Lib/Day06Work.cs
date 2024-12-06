using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day06Lib;

public class Day06Work
{
    private Dictionary<Vector2, char> _grid = new();

    private HashSet<Vector2> _positions = new();
    private List<Configuration> _path = new();

    public int CalculatePart1Solution(Filedata fileData)
    {
        _positions = new HashSet<Vector2>();

        for (int y = 0; y < fileData.Lines.Count; y++)
        {
            var line = fileData.Lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                var character = line[x];
                _grid.Add(new Vector2(x, y), character);
            }
        }

        var start = _grid.First(c => c.Value != '.' && c.Value != '#');

        var dir = start.Value switch
        {
            '^' => new Vector2(0, -1),
            '>' => new Vector2(1, 0),
            'v' => new Vector2(0, -1),
            '<' => new Vector2(-1, 0),
            _ => throw new InvalidOperationException($"Invalid direction character {start.Value}")
        };

        var currentPos = start.Key;
        _positions.Add(currentPos);
        _path.Add(new Configuration(currentPos, dir));

        while (true)
        {
            var nextPos = currentPos + dir;
            if (!_grid.ContainsKey(nextPos))
            {
                // left grid
                break;
            }
            else if (_grid[nextPos] == '#')
            {
                // obstacle, turn right
                dir = MathUtils.TurnRight(dir);
                continue;
            }
            else
            {
                // suitable position
                _positions.Add(nextPos);
                _path.Add(new Configuration(nextPos, dir));
                currentPos = nextPos;
            }
        }

        return _positions.Count;
    }


    public int CalculatePart2Solution(Filedata fileData)
    {
        var suitablePositions = new List<Vector2>();
        var triedPositons = new HashSet<Vector2>();

        for (int i = 1; i < _path.Count; i++)
        {
            var testConfig = _path[i];

            if (_grid[testConfig.Position] != '.')
            {
                continue;
            }
            if (triedPositons.Contains((testConfig.Position)))
            {
                continue;
            }

            var walkedPath = _path.Take(i - 1).ToHashSet();

            var currentPos = _path[i - 1].Position;
            var dir = _path[i - 1].Direction;

            var testConfigs = new HashSet<Configuration>();

            while (true)
            {
                var nextPos = currentPos + dir;
                if (!_grid.ContainsKey(nextPos))
                {
                    // walked out of grid
                    triedPositons.Add(testConfig.Position);
                    break;
                }
                else if (_grid[nextPos] == '#' || nextPos == testConfig.Position)
                {
                    // obstacle, turn right
                    dir = MathUtils.TurnRight(dir);
                    continue;
                }
                else
                {
                    // suitable position
                    var conf = new Configuration(nextPos, dir);
                    if (walkedPath.Contains(conf) || testConfigs.Contains(conf))
                    {
                        // loop
                        suitablePositions.Add(testConfig.Position);
                        triedPositons.Add(testConfig.Position);
                        break;
                    }
                    else
                    {
                        // keep walking
                        testConfigs.Add(conf);
                        currentPos = nextPos;
                    }
                }
            }
        }

        return suitablePositions.Count;
    }

}
