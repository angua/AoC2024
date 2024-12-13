using System.Numerics;
using Common;

namespace AoC2024Lib.Days.Day13Lib;

public class Day13Work
{

    public List<Machine> Machines = new();

    private Machine _currentMachine;

    public long CalculatePart1Solution(Filedata fileData)
    {
        var i = 0;
        for (; i < fileData.Lines.Count; i++)
        {
            var line = fileData.Lines[i];

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
            var coords = parts[1].Trim().Split(",");
            var coordXStr = coords[0].Trim();
            var coordYStr = coords[1].Trim();

            var coordX = 0;
            var coordY = 0;

            if (coordXStr.Contains("="))
            {
                var coordXparts = coordXStr.Split("=", StringSplitOptions.RemoveEmptyEntries);
                coordX = int.Parse(coordXparts[1]);
            }
            else
            {
                coordX = int.Parse(coordXStr.Substring(1));
            }

            if (coordYStr.Contains("="))
            {
                var coordYparts = coordYStr.Split("=", StringSplitOptions.RemoveEmptyEntries);
                coordY = int.Parse(coordYparts[1]);
            }
            else
            {
                coordY = int.Parse(coordYStr.Substring(1));
            }

            if (line.Contains("Button A"))
            {
                // Button A: X+17, Y+84
                _currentMachine = new Machine();
                Machines.Add(_currentMachine);

                _currentMachine.ButtonA = new Vector2(coordX, coordY);
            }
            else if (line.Contains("Button B"))
            {
                _currentMachine.ButtonB = new Vector2(coordX, coordY);
            }
            else
            {
                // Prize: X=2159, Y=6678
                _currentMachine.Prize = new Vector2(coordX, coordY);
            }
        }

        foreach (var machine in Machines)
        {
            // m * A + n * B = prize

            // m * Ax + n * Bx = Px
            // m * Ay + n * By = Py

            // n = (Px Ay - Py Ax) / (Bx Ay - By Ax)
            // m = (Px - Bx n) / Ax

            var countButtonB = (machine.Prize.X * machine.ButtonA.Y - machine.Prize.Y * machine.ButtonA.X) /
                            (machine.ButtonB.X * machine.ButtonA.Y - machine.ButtonB.Y * machine.ButtonA.X);

            if ((int)countButtonB == countButtonB)
            {
                var countButtonA = (machine.Prize.X - machine.ButtonB.X * countButtonB) / machine.ButtonA.X;
                if ((int)countButtonA == countButtonA)
                {
                    if (countButtonB <= 100 && countButtonA <= 100)
                    {
                        machine.CountA = (int)countButtonA;
                        machine.CountB = (int)countButtonB;

                        var resutlX = countButtonA * machine.ButtonA.X + countButtonB * machine.ButtonB.X;
                        var resultY = countButtonA * machine.ButtonA.Y + countButtonB * machine.ButtonB.Y;

                        machine.Tokens = 3 * machine.CountA + machine.CountB;
                    }
                }
            }
        }

        return Machines.Sum(m => m.Tokens);
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        long add = 10000000000000;
        long sum = 0;

        foreach (var machine in Machines)
        {
            machine.RealPrizeX = add + (long)machine.Prize.X;
            machine.RealPrizeY = add + (long)machine.Prize.Y;

            var numB = machine.RealPrizeX * (long)machine.ButtonA.Y - machine.RealPrizeY * (long)machine.ButtonA.X;
            var denomB = (long)machine.ButtonB.X * (long)machine.ButtonA.Y - (long)machine.ButtonB.Y * (long)machine.ButtonA.X;

            if (numB % denomB == 0)
            {
                var countButtonB = numB / denomB;

                var numA = machine.RealPrizeX - (long)machine.ButtonB.X * countButtonB;

                if (numA % (long)machine.ButtonA.X == 0)
                {
                    var countButtonA = numA / (long)machine.ButtonA.X;

                    machine.RealCountA = (long)countButtonA;
                    machine.RealCountB = (long)countButtonB;

                    var resutlX = countButtonA * machine.ButtonA.X + countButtonB * machine.ButtonB.X;
                    var resultY = countButtonA * machine.ButtonA.Y + countButtonB * machine.ButtonB.Y;

                    machine.RealTokens = 3 * machine.RealCountA + machine.RealCountB;
                    sum += machine.RealTokens;
                }
            }
        }
        return sum;
    }




}
