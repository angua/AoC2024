using Common;

namespace AoC2024Lib.Days.Day07Lib;

public class Day07Work
{
    private List<Equation> _equations = new();

    private List<Equation> _validEquations = new();
    private List<Equation> _validEquations2 = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        foreach (var line in fileData.Lines)
        {
            var equation = new Equation();
            var parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
            equation.TestValue = long.Parse(parts[0]);

            var numParts = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            equation.Numbers = numParts.Select(n => long.Parse(n)).ToList();

            _equations.Add(equation);
        }

        foreach (var equation in _equations)
        {
            var currentValues = new List<long>
            {
                equation.Numbers.First()
            };

            for (int i = 1; i < equation.Numbers.Count; i++)
            {
                var next = equation.Numbers[i];

                var nextNumbers = new List<long>();

                foreach (var current in currentValues)
                {
                    nextNumbers.Add(current + next);
                    nextNumbers.Add(current * next);
                }

                currentValues = new List<long>(nextNumbers);
            }

            if (currentValues.Any(v => v == equation.TestValue))
            {
                _validEquations.Add(equation);
            }
        }

        return _validEquations.Sum(e => e.TestValue);
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        foreach (var equation in _equations)
        {
            var currentValues = new List<long>
            {
                equation.Numbers.First()
            };

            for (int i = 1; i < equation.Numbers.Count; i++)
            {
                var next = equation.Numbers[i];

                var nextNumbers = new List<long>();

                foreach (var current in currentValues)
                {
                    nextNumbers.Add(current + next);
                    nextNumbers.Add(current * next);

                    var currentStr = current.ToString();
                    var nextStr = next.ToString();
                    var newStr = string.Join("", currentStr, nextStr);
                    nextNumbers.Add(long.Parse(newStr));
                }

                currentValues = new List<long>(nextNumbers);
            }

            if (currentValues.Any(v => v == equation.TestValue))
            {
                _validEquations2.Add(equation);
            }
        }

        return _validEquations2.Sum(e => e.TestValue);
    }

}
