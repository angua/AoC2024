using Common;

namespace AoC2024Lib.Days.Day23Lib;

public class Day23Work
{
    private List<(string, string)> _connections = new();

    private Dictionary<string, List<string>> _computerConnections = new();

    private HashSet<string> _networksOf3 = new();
    private List<string> _historianNetworks = new();

    private HashSet<string> _networks = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        foreach (var line in fileData.Lines)
        {
            var parts = line.Split('-');
            _connections.Add((parts[0], parts[1]));
        }

        foreach (var connection in _connections)
        {
            var first = connection.Item1;
            var second = connection.Item2;

            AddConnected(first, second);
            AddConnected(second, first);
        }

        foreach (var computer in _computerConnections)
        {
            var connected = computer.Value;
            for (int i = 0; i < connected.Count; i++)
            {
                var connectedComputer = connected[i];
                var others = connected.Skip(i + 1);

                foreach (var other in others)
                {
                    if (_computerConnections[connectedComputer].Contains(other))
                    {
                        var network = new List<string>();
                        network.Add(computer.Key);
                        network.Add(connectedComputer);
                        network.Add(other);

                        network.Sort();
                        _networksOf3.Add(string.Join('-', network));
                    }
                }
            }
        }

        foreach (var network in _networksOf3)
        {
            var parts = network.Split("-");
            if (parts.Any(s => s.StartsWith("t")))
            {
                _historianNetworks.Add(network);
            }
        }

        return _historianNetworks.Count;
    }

    private void AddConnected(string first, string second)
    {
        if (!_computerConnections.TryGetValue(first, out var computers))
        {
            computers = new List<string>();
            _computerConnections[first] = computers;
        }
        computers.Add(second);
    }

    public string CalculatePart2Solution(Filedata fileData)
    {
        foreach (var computer in _computerConnections)
        {
            var connected = computer.Value;
            for (int i = 0; i < connected.Count; i++)
            {
                var connectedComputer = connected[i];
                var others = connected.Skip(i + 1);

                var network = new List<string>();
                network.Add(computer.Key);
                network.Add(connectedComputer);
                foreach (var other in others)
                {

                    var canAdd = true;
                    foreach (var link in network)
                    {
                        if (!_computerConnections[link].Contains(other))
                        {
                            canAdd = false;
                        }
                    }
                    if (canAdd)
                    {
                        network.Add(other);
                    }

                }
                if (network.Count > 2)
                {
                    network.Sort();
                    _networks.Add(string.Join(",", network));
                }
            }
        }
        var maxLength = _networks.Max(n => n.Length);
        var longest = _networks.First(n => n.Length == maxLength);
        return longest;
    }
}
