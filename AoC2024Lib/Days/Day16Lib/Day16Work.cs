using System.Linq;
using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day16Lib;

public class Day16Work
{
    public Dictionary<Vector2, char> Grid = new();

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    public Stack<Crossroad> AvailableCrossroads = new();

    public List<Crossroad> Crossroads = new();
    public List<Connection> Connections = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        Grid = Filedata.ParseGrid(fileData);

        _startPosition = Grid.First(p => p.Value == 'S').Key;
        _endPosition = Grid.First(p => p.Value == 'E').Key;

        // facing east at start
        var direction = new Vector2(1, 0);

        // find crossroads and connecting paths
        FindNextConnection();
        while (AvailableCrossroads.Count > 0)
        {
            FindNextConnection();
        }

        var startCrossRoad = Crossroads.First(c => c.Position == _startPosition);
        startCrossRoad.EnterDirection = direction;
        startCrossRoad.PointsFromStart = 0;

        var endCrossRoad = Crossroads.First(c => c.Position == _endPosition);

        var lowest = new SortedDictionary<long, List<Crossroad>>();
        lowest.Add(startCrossRoad.PointsFromStart, new List<Crossroad>() { startCrossRoad });
        var visited = new HashSet<Crossroad>();

        long bestPoints = 0;

        while (bestPoints == 0)
        {
            var minPointsCrossroads = lowest.First();
            var currentCrossroad = minPointsCrossroads.Value.First();
            visited.Add(currentCrossroad);
            minPointsCrossroads.Value.Remove(currentCrossroad);
            if (minPointsCrossroads.Value.Count == 0)
            {
                lowest.Remove(minPointsCrossroads.Key);
            }

            var nextConnections = new List<(int, Connection)>();

            foreach (var connection in currentCrossroad.Connections)
            {
                if (connection.CrossRoad2 == null)
                {
                    // dead end
                    continue;
                }

                var addpoints = 0;
                Crossroad other;

                if (connection.CrossRoad1 == currentCrossroad)
                {
                    // enter at c1, exit c2
                    other = connection.CrossRoad2;
                    if (visited.Contains(other))
                    {
                        continue;
                    }
                    if (currentCrossroad.EnterDirection == -connection.InDirection1)
                    {
                        // don't move back where we came from
                        continue;
                    }
                    if (currentCrossroad.EnterDirection != connection.InDirection1)
                    {
                        // need to turn
                        addpoints = 1000;
                    }

                    var points = currentCrossroad.PointsFromStart + addpoints + connection.Points;
                    var otherEnterDir = -connection.InDirection2;

                    if (other.PointsFromStart == -1 || other.PointsFromStart > points)
                    {
                        other.PointsFromStart = points;
                        other.EnterDirection = otherEnterDir;
                    }

                    if (other == endCrossRoad)
                    {
                        bestPoints = other.PointsFromStart;
                        break;
                    }

                    if (!lowest.TryGetValue(other.PointsFromStart, out var list))
                    {
                        list = new List<Crossroad>();
                        lowest.Add(other.PointsFromStart, list);
                    }
                    list.Add(other);
                }
                else
                {
                    // enter at c2, exit c1
                    other = connection.CrossRoad1;
                    if (visited.Contains(other))
                    {
                        continue;
                    }
                    if (currentCrossroad.EnterDirection == -connection.InDirection2)
                    {
                        continue;
                    }
                    if (currentCrossroad.EnterDirection != connection.InDirection2)
                    {
                        addpoints = 1000;
                    }
                    var otherEnterDir = -connection.InDirection1;
                    var points = currentCrossroad.PointsFromStart + addpoints + connection.Points;

                    if (other.PointsFromStart == -1 || other.PointsFromStart > points)
                    {
                        other.PointsFromStart = points;
                        other.EnterDirection = otherEnterDir;
                    }
                    if (other == endCrossRoad)
                    {
                        bestPoints = other.PointsFromStart;
                        break;
                    }
                    if (!lowest.TryGetValue(other.PointsFromStart, out var list))
                    {
                        list = new List<Crossroad>();
                        lowest.Add(other.PointsFromStart, list);
                    }
                    list.Add(other);
                }
            }
        }

        return bestPoints;
    }



    public long CalculatePart2Solution(Filedata fileData)
    {

        return 0;
    }

    public void Parse(Filedata fileData)
    {
        Grid = Filedata.ParseGrid(fileData);
        _startPosition = Grid.First(p => p.Value == 'S').Key;
        _endPosition = Grid.First(p => p.Value == 'E').Key;
    }

    public void FindNextConnection()
    {
        if (Crossroads.Count == 0)
        {
            var startCrossroad = new Crossroad()
            {
                Position = _startPosition,
            };
            Crossroads.Add(startCrossroad);
            AvailableCrossroads.Push(startCrossroad);
        }
        if (AvailableCrossroads.Count > 0)
        {
            var current = AvailableCrossroads.Pop();

            foreach (var dir in MathUtils.OrthogonalDirections)
            {
                var nextPos = current.Position + dir;

                if (Grid.ContainsKey(nextPos) && Grid[nextPos] != '#')
                {
                    if (current.Connections.Any(c => (c.CrossRoad1 == current && c.InDirection1 == dir) ||
                                                     (c.CrossRoad2 == current && c.InDirection2 == dir)))
                    {
                        continue;
                    }

                    var connection = new Connection()
                    {
                        CrossRoad1 = current,
                        InDirection1 = dir,
                        Points = 1
                    };
                    connection.Positions.Add(current.Position);
                    connection.Positions.Add(nextPos);
                    Connections.Add(connection);
                    current.Connections.Add(connection);

                    var lastDir = dir;

                    while (true)
                    {
                        var testCount = 0;
                        Vector2 proceedDir = Vector2.Zero;
                        Vector2 proceedPos = new Vector2(-1, -1);
                        var addPoints = 0;

                        // test positions straight ahead and left / right
                        var straightPos = nextPos + lastDir;
                        var leftDir = MathUtils.TurnLeft(lastDir);
                        var leftPos = nextPos + leftDir;
                        var rightDir = MathUtils.TurnRight(lastDir);
                        var rightPos = nextPos + rightDir;

                        if (Grid.ContainsKey(straightPos) && Grid[straightPos] != '#')
                        {
                            testCount++;
                            proceedDir = lastDir;
                            proceedPos = straightPos;
                            addPoints = 1;
                        }
                        if (Grid.ContainsKey(leftPos) && Grid[leftPos] != '#')
                        {
                            testCount++;
                            proceedDir = leftDir;
                            proceedPos = leftPos;
                            // turn+move
                            addPoints = 1001;
                        }
                        if (Grid.ContainsKey(rightPos) && Grid[rightPos] != '#')
                        {
                            testCount++;
                            proceedDir = rightDir;
                            proceedPos = rightPos;
                            addPoints = 1001;
                        }

                        if (testCount > 1 || Grid[nextPos] == 'E')
                        {
                            if (Grid[nextPos] == 'E')
                            {
                                var end = true;
                            }

                            // new croosroad
                            if (!Crossroads.Any(c => c.Position == nextPos))
                            {
                                var newCrossroad = new Crossroad()
                                {
                                    Position = nextPos,
                                };
                                connection.CrossRoad2 = newCrossroad;
                                connection.InDirection2 = -lastDir;
                                newCrossroad.Connections.Add(connection);
                                Crossroads.Add(newCrossroad);
                                AvailableCrossroads.Push(newCrossroad);
                            }
                            else
                            {
                                var otherCrossroad = Crossroads.First(c => c.Position == nextPos);
                                otherCrossroad.Connections.Add(connection);
                                connection.CrossRoad2 = otherCrossroad;
                                connection.InDirection2 = -lastDir;
                            }
                            break;
                        }
                        else if (testCount == 0)
                        {
                            // dead end
                            break;
                        }
                        else
                        {
                            // only 1 direction to move to, move along the path
                            nextPos = proceedPos;
                            connection.Positions.Add(nextPos);
                            lastDir = proceedDir;
                            connection.Points += addPoints;
                        }
                    }
                }
            }
        }

    }
}

