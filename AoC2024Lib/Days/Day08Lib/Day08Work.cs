using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day08Lib;

public class Day08Work
{
    private Dictionary<Vector2, char> _grid;
    private Dictionary<Vector2, char> _antennaGrid;
    private List<char> _frequencies = new();

    private HashSet<Vector2> _antiNodes = new();
    private HashSet<Vector2> _antiNodes2 = new();

    public int CalculatePart1Solution(Filedata fileData)
    {
        _grid = Filedata.ParseGrid(fileData);
        _antennaGrid = _grid.Where(p => p.Value != '.').ToDictionary(x => x.Key, x => x.Value);
        _frequencies = _antennaGrid.Values.Distinct().ToList();
        
        foreach (var frequency in _frequencies )
        {
            var positions = _antennaGrid.Where(p => p.Value == frequency).Select(p => p.Key).ToList();
            foreach (var position in positions)
            {
                foreach (var other in positions)
                {
                    if (position == other)
                    {
                        continue;
                    }

                    var dir = other - position;
                    var testPos = other + dir;
                    if (_grid.ContainsKey(testPos))
                    {
                        _antiNodes.Add(testPos);
                    }
                }
            }
        }

        return _antiNodes.Count;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        foreach (var frequency in _frequencies)
        {
            var positions = _antennaGrid.Where(p => p.Value == frequency).Select(p => p.Key).ToList();
            foreach (var position in positions)
            {
                foreach (var other in positions)
                {
                    if (position == other)
                    {
                        continue;
                    }

                    var dir = other - position;
                    var testPos = position + dir;
                    while (_grid.ContainsKey(testPos))
                    {
                        _antiNodes2.Add(testPos);
                        testPos = testPos + dir;
                    }
                }
            }
        }

        return _antiNodes2.Count;
    }

}
