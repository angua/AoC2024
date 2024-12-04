using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day04Lib;

public class Day04Work
{
    private List<Vector2> _directions = new List<Vector2>()
    {
        new Vector2(-1, -1),
        new Vector2(-1, 0),
        new Vector2(-1, 1),
        new Vector2(0, -1),
        new Vector2(0, 1),
        new Vector2(1, -1),
        new Vector2(1, 0),
        new Vector2(1, 1)
    };

    private Dictionary<Vector2, char> _grid = new();


    public int CalculatePart1Solution(Filedata fileData)
    {

        for (int y = 0; y < fileData.Lines.Count; y++)
        {
            var line = fileData.Lines[y];

            for (int x = 0; x < line.Length; x++)
            {
                _grid.Add(new Vector2 ( x, y ), line[x]);
            }
        }

        var occurences = 0;

        var xpositions = _grid.Where(i => i.Value == 'X');

        foreach (var xpos in xpositions)
        {
            foreach (var dir in _directions)
            {
                var secondPos = xpos.Key + dir;
                if (_grid.ContainsKey(secondPos))
                {
                    if (_grid[secondPos] == 'M')
                    {
                        var thirdPos = secondPos + dir;

                        if (_grid.ContainsKey(thirdPos))
                        {
                            if (_grid[thirdPos] == 'A')
                            {
                                var fourthPos = thirdPos + dir;
                                if (_grid.ContainsKey(fourthPos))
                                {
                                    if (_grid[fourthPos] == 'S')
                                    {
                                        occurences++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        return occurences;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        var occurences = 0;

        var aPositions = _grid.Where(i => i.Value == 'A');

        var upleft = new Vector2(-1, -1);
        var downright = new Vector2(1, 1);

        var upright = new Vector2(1, -1);
        var downleft = new Vector2(-1, 1);

        foreach (var aPos in aPositions)
        {
            var upLeftPos = aPos.Key + upleft;
            if (!_grid.ContainsKey(upLeftPos))
            {
                continue;
            }
            var upRightPos = aPos.Key + upright;
            if (!_grid.ContainsKey(upRightPos))
            {
                continue;
            }
            var downLeftPos = aPos.Key + downleft;
            if (!_grid.ContainsKey(downLeftPos))
            {
                continue;
            }
            var downRightPos = aPos.Key + downright;
            if (!_grid.ContainsKey(downRightPos))
            {
                continue;
            }

            if (_grid[upLeftPos] == 'M')
            {
                if (_grid[downRightPos] != 'S')
                {
                    continue;
                }
            }
            else if (_grid[upLeftPos] == 'S')
            {
                if (_grid[downRightPos] != 'M')
                {
                    continue;
                }
            }
            else
            {
                continue;
            }

            if (_grid[upRightPos] == 'M')
            {
                if (_grid[downLeftPos] != 'S')
                {
                    continue;
                }
            }
            else if (_grid[upRightPos] == 'S')
            {
                if (_grid[downLeftPos] != 'M')
                {
                    continue;
                }
            }
            else
            {
                continue;
            }

            occurences++;
        }
        return occurences;
    }

}
