using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day10Lib;

public class Day10Work
{
    List<List<(Vector2, int)>> _finishedTrails = new ();

    public int CalculatePart1Solution(Filedata fileData)
    {
        var grid = Filedata.ParseGrid(fileData);

        var numGrid = new Dictionary<Vector2, int>();

        foreach (var pair in grid)
        {
            numGrid.Add(pair.Key, int.Parse(pair.Value.ToString()));
        }

        var startPositions = numGrid.Where(p => p.Value == 0);

        var directions = new List<Vector2>()
        {
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(0, -1),
        };

        _finishedTrails = new List<List<(Vector2, int)>>();
        foreach (var pos in startPositions)
        {
            var trails = new Queue<List<(Vector2, int)>>();
            var trail = new List<(Vector2, int)>();
            trail.Add((pos.Key, pos.Value));

            trails.Enqueue(trail);

            while (trails.Count > 0)
            {
                var next = trails.Dequeue();
                var currentPos = next.Last();

                var matchCount = 0;

                foreach (var dir in directions)
                {
                    var testPos = currentPos.Item1 + dir;
                    if (numGrid.ContainsKey(testPos))
                    {

                        var testValue = numGrid[testPos];
                        if (testValue == currentPos.Item2 + 1)
                        {
                            matchCount++;
                            var newTrail = new List<(Vector2, int)>(next);
                            newTrail.Add((testPos, testValue));
                            if (testValue == 9)
                            {
                                _finishedTrails.Add(newTrail);
                            }
                            else
                            {
                                trails.Enqueue(newTrail);
                            }
                        }
                    }
                }

            }
        }

        var endpoints = new List<(Vector2, Vector2)>();

        foreach (var route in _finishedTrails)
        {
            var startPos = route.First().Item1;
            var endPos = route.Last().Item1;
            endpoints.Add((startPos, endPos));
        }

        var different = endpoints.Distinct().ToList();

        return different.Count;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        return _finishedTrails.Count;
    }

}
