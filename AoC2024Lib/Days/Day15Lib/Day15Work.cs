using System.ComponentModel;
using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day15Lib;

public class Day15Work
{
    private Dictionary<Vector2, char> _startGrid = new();
    public Dictionary<Vector2, char> Grid { get; private set; } = new();
    private Dictionary<Vector2, char> _startLargeGrid = new();
    public Dictionary<Vector2, char> LargeGrid { get; private set; } = new();

    public List<Vector2> Instructions { get; private set; } = new();

    public int CalculatePart1Solution(Filedata fileData)
    {
        Grid = new Dictionary<Vector2, char>(_startGrid);

        var currentPos = Grid.First(c => c.Value == '@').Key;

        foreach (var instruction in Instructions)
        {
            var nextPos = currentPos + instruction;

            if (Grid[nextPos] == '#')
            {
                // wall
                continue;
            }
            else if (Grid[nextPos] == 'O')
            {
                var boxCount = 0;
                // box, try to move boxes
                while (Grid[nextPos] == 'O')
                {
                    boxCount++;
                    nextPos = nextPos + instruction;
                }
                // pos after boxes
                if (Grid[nextPos] == '#')
                {
                    // would push box into wall
                    continue;
                }
                else
                {
                    // push boxes
                    for (int x = 0; (x < boxCount); x++)
                    {
                        Grid[nextPos] = 'O';
                        nextPos = nextPos - instruction;
                    }
                    // pos before boxes is current pos
                    currentPos = nextPos;
                    Grid[currentPos] = '.';
                }
            }
            else
            {
                // move
                currentPos = nextPos;
            }
        }

        var boxes = Grid.Where(p => p.Value == 'O');
        var gps = boxes.Select(p => p.Key.X + p.Key.Y * 100);
        return (int)gps.Sum(x => x);
    }

    private void ParseFile(Filedata fileData)
    {
        var y = 0;

        for (; y < fileData.Lines.Count; y++)
        {
            var line = fileData.Lines[y];

            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            for (int x = 0; x < line.Length; x++)
            {
                _startGrid.Add(new Vector2(x, y), line[x]);
            }
        }
        y++;
        for (; y < fileData.Lines.Count; y++)
        {
            var line = fileData.Lines[y];

            foreach (var character in line)
            {
                var dir = GetDir(character);
                Instructions.Add(dir);
            }
        }
    }

    private Vector2 GetDir(char character)
    {
        return character switch
        {
            '>' => new Vector2(1, 0),
            'v' => new Vector2(0, 1),
            '^' => new Vector2(0, -1),
            '<' => new Vector2(-1, 0),
            _ => throw new Exception()
        };
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        LargeGrid = new Dictionary<Vector2, char>(_startLargeGrid);

        var currentPos = LargeGrid.First(c => c.Value == '@').Key;

        for (var i = 0; i < Instructions.Count; i++)
        {
            PerformInstruction(i);
        }

        var boxes = LargeGrid.Where(p => p.Value == '[');
        var gps = boxes.Select(p => p.Key.X + p.Key.Y * 100);
        return (int)gps.Sum(x => x);
    }

    private Dictionary<Vector2, char> ExpandGrid(Dictionary<Vector2, char> grid)
    {
        // If the tile is #, the new map contains ## instead.
        // If the tile is O, the new map contains[] instead.
        // If the tile is ., the new map contains..instead.
        // If the tile is @, the new map contains @. instead.
        var wideGrid = new Dictionary<Vector2, char>();

        for (int y = 0; y <= grid.Max(p => p.Key.Y); y++)
        {
            for (int x = 0; x <= grid.Max(q => q.Key.X); x++)
            {
                var pos = new Vector2(x, y);
                var gridPos = grid[pos];

                var newPosLeft = new Vector2(2 * x, y);
                var newPosRight = new Vector2(2 * x + 1, y);

                if (gridPos == '#')
                {
                    wideGrid[newPosLeft] = '#';
                    wideGrid[newPosRight] = '#';
                }
                else if (gridPos == 'O')
                {
                    wideGrid[newPosLeft] = '[';
                    wideGrid[newPosRight] = ']';
                }
                else if (gridPos == '@')
                {
                    wideGrid[newPosLeft] = '@';
                    wideGrid[newPosRight] = '.';
                }
                else
                {
                    wideGrid[newPosLeft] = '.';
                    wideGrid[newPosRight] = '.';
                }
            }
        }
        return wideGrid;
    }

    public void ParseGrids(Filedata filedata)
    {
        ParseFile(filedata);
        _startLargeGrid = ExpandGrid(_startGrid);
        Grid = _startGrid;
        LargeGrid = _startLargeGrid;
    }

    public bool PerformInstruction(int nextInstructionNum)
    {
        var instruction = Instructions[nextInstructionNum];
        var currentPos = LargeGrid.First(c => c.Value == '@').Key;
        var nextPos = currentPos + instruction;

        if (LargeGrid[nextPos] == '#')
        {
            // wall
            return false;
        }
        else if (LargeGrid[nextPos] == '[' || LargeGrid[nextPos] == ']')
        {
            if (instruction.X != 0)
            {
                // horizontal movement
                var boxCount = 0;
                // box, try to move boxes
                while (LargeGrid[nextPos] == '[' || LargeGrid[nextPos] == ']')
                {
                    boxCount++;
                    nextPos = nextPos + instruction;
                }
                // pos after boxes 
                if (LargeGrid[nextPos] == '#')
                {
                    // would push box into wall
                    return false;
                }
                // push boxes
                for (int x = 0; (x < boxCount); x++)
                {
                    var previousPos = nextPos - instruction;
                    LargeGrid[nextPos] = LargeGrid[previousPos];
                    nextPos = nextPos - instruction;
                }
                // pos before boxes is current pos
                LargeGrid[currentPos + instruction] = '@';
                LargeGrid[currentPos] = '.';
            }
            else
            {
                // vertical movement
                var boxesToMove = new List<HashSet<Vector2>>();
                var currentBoxes = new HashSet<Vector2>();
                currentBoxes.Add(nextPos);
                if (LargeGrid[nextPos] == '[')
                {
                    currentBoxes.Add(new Vector2(nextPos.X + 1, nextPos.Y));
                }
                else
                {
                    currentBoxes.Add(new Vector2(nextPos.X - 1, nextPos.Y));
                }

                boxesToMove.Add(currentBoxes);

                while (true)
                {
                    currentBoxes = boxesToMove.Last();
                    var nextBoxes = new HashSet<Vector2>();

                    foreach (var box in currentBoxes)
                    {
                        nextPos = box + instruction;
                        if (LargeGrid[nextPos] == '#')
                        {
                            // wall, can't move
                            return false;
                        }
                        if (LargeGrid[nextPos] == '[')
                        {
                            nextBoxes.Add(nextPos);
                            nextBoxes.Add(new Vector2(nextPos.X + 1, nextPos.Y));
                        }
                        else if (LargeGrid[nextPos] == ']')
                        {
                            nextBoxes.Add(nextPos);
                            nextBoxes.Add(new Vector2(nextPos.X - 1, nextPos.Y));
                        }
                    }

                    if (nextBoxes.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        boxesToMove.Add(nextBoxes);
                    }

                }

                // move boxes
                for (int i = boxesToMove.Count - 1; i >= 0; i--)
                {
                    var currentboxes = boxesToMove[i];

                    foreach (var box in currentboxes)
                    {
                        var newPos = box + instruction;
                        LargeGrid[newPos] = LargeGrid[box];
                        LargeGrid[box] = '.';
                    }
                }

                LargeGrid[currentPos] = '.';
                LargeGrid[currentPos + instruction] = '@';
            }
        }
        else
        {
            // move
            LargeGrid[currentPos] = '.';
            LargeGrid[nextPos] = '@';
        }

        return true;
    }

    public void Reset()
    {
        Grid = new Dictionary<Vector2, char>(_startGrid);
        LargeGrid = new Dictionary<Vector2, char>(_startLargeGrid);
    }
}
