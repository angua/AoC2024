using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day12Lib;

public class Day12Work
{
    private Dictionary<Vector2, char> _grid = new();
    private List<List<GardenPosition>> _regions = new();
    private HashSet<Vector2> _visited = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        _grid = Filedata.ParseGrid(fileData);
        var width = _grid.Max(p => p.Key.X) + 1;
        var height = _grid.Max(p => p.Key.Y) + 1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var pos = new Vector2(x, y);
                if (_visited.Contains(pos))
                {
                    continue;
                }
                var region = GetRegion(pos);
                _regions.Add(region);
            }
        }

        var sum = 0;
        foreach (var region in _regions)
        {
            var perimeters = region.Sum(g => g.Perimeters.Count);
            var area = region.Count;
            sum += perimeters * area;
        }

        return sum;
    }

    private List<GardenPosition> GetRegion(Vector2 pos)
    {
        var plant = _grid[pos];

        var region = new List<GardenPosition>();

        var toVisit = new Queue<Vector2>();
        toVisit.Enqueue(pos);

        while (toVisit.Count > 0)
        {
            var current = toVisit.Dequeue();

            if (_visited.Contains(current))
            {
                continue;
            }
            _visited.Add(current);

            if (_grid[current] == plant)
            {
                var garden = new GardenPosition();
                garden.Position = current;

                foreach (var dir in MathUtils.OrthogonalDirections)
                {
                    var nextPos = current + dir;

                    if (!_grid.ContainsKey(nextPos))
                    {
                        // fence position facing outside the garden
                        // store direction of neighbor (which is the normal vector of the fence)
                        garden.Perimeters.Add(dir);
                        continue;
                    }
                    var neighborGarden = _grid[nextPos];
                    if (neighborGarden == plant)
                    {
                        if (!_visited.Contains(nextPos))
                        {
                            toVisit.Enqueue(nextPos);
                        }
                    }
                    else
                    {
                        // fence position facing different plant
                        garden.Perimeters.Add(dir);
                    }
                }

                region.Add(garden);
            }
        }
        return region;
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        var sum = 0;    

        foreach (var region in _regions)
        {
            var edges = 0;

            foreach (var dir in MathUtils.OrthogonalDirections)
            {
                var facingPerimeters = region.Where(g => g.Perimeters.Contains(dir));

                if (dir.Y != 0)
                {
                    // positions at the same height
                    var levels = facingPerimeters.Select(g => g.Position.Y).Distinct();
                    foreach (var level in levels)
                    {
                        edges++;
                        var positionsOnLevel = facingPerimeters.Where(g => g.Position.Y == level).Select(g => g.Position.X).OrderBy(x => x).ToList();
                        for (int i = 0; i < positionsOnLevel.Count - 1; i++)
                        {
                            // when the positions are not continous, they belong to different edges
                            var dist = positionsOnLevel[i + 1] - positionsOnLevel[i];
                            if (dist > 1)
                            {
                                edges++;
                            }
                        }
                    }
                }
                else
                {
                    var levels = facingPerimeters.Select(g => g.Position.X).Distinct();
                    foreach (var level in levels)
                    {
                        edges++;
                        var positionsOnLevel = facingPerimeters.Where(g => g.Position.X == level).Select(g => g.Position.Y).OrderBy(y => y).ToList();
                        for (int i = 0; i < positionsOnLevel.Count - 1; i++)
                        {
                            var dist = positionsOnLevel[i + 1] - positionsOnLevel[i];
                            if (dist > 1)
                            {
                                edges++;
                            }
                        }
                    }

                }
            }

            sum += edges * region.Count;
        }

        return sum;
    }

}
