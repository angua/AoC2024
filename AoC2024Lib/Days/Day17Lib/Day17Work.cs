using Common;

namespace AoC2024Lib.Days.Day17Lib;

public class Day17Work
{
    public long RegisterA { get; set; }
    private long _startA;
    public long RegisterB { get; set; }
    private long _startB;
    public long RegisterC { get; set; }
    private long _startC;
    public List<int> Program { get; set; } = new();

    public List<int> Output { get; set; } = new();

    public string CalculatePart1Solution(Filedata fileData)
    {
        foreach (var line in fileData.Lines)
        {
            if (line.Contains(":"))
            {
                var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);

                if (parts[0].Contains("A"))
                {
                    RegisterA = int.Parse(parts[1].Trim());
                    _startA = RegisterA;
                }
                else if (parts[0].Contains("B"))
                {
                    RegisterB = int.Parse(parts[1].Trim());
                    _startB = RegisterB;
                }
                else if (parts[0].Contains("C"))
                {
                    RegisterC = int.Parse(parts[1].Trim());
                    _startC = RegisterC;
                }
                else
                {
                    var nums = parts[1].Trim().Split(',');
                    Program = nums.Select(n => int.Parse(n)).ToList();
                }
            }
        }

        var pointer = 0;

        while (pointer < Program.Count)
        {
            var opcode = Program[pointer];
            var operand = Program[pointer + 1];

            pointer = DoOperation(pointer, opcode, operand);
        }

        return string.Join(",", Output);
    }

    private int DoOperation(int pointer, int opcode, int operand)
    {
        switch (opcode)
        {
            case 0:
                var denom = Math.Pow(2, GetCombo(operand));
                var result = RegisterA / denom;
                RegisterA = (long)result;
                pointer += 2;
                break;

            case 1:
                result = RegisterB ^ operand;
                RegisterB = (long)result;
                pointer += 2;

                break;

            case 2:
                RegisterB = GetCombo(operand) % 8;
                pointer += 2;

                break;

            case 3:
                if (RegisterA == 0)
                {
                    pointer += 2;
                }
                else
                {
                    pointer = operand;
                }
                break;


            case 4:
                result = RegisterB ^ RegisterC;
                RegisterB = (long)result;
                pointer += 2;
                break;

            case 5:
                result = GetCombo(operand) % 8;
                Output.Add((int)result);
                pointer += 2;
                break;

            case 6:
                denom = Math.Pow(2, GetCombo(operand));
                result = RegisterA / denom;
                RegisterB = (long)result;
                pointer += 2;
                break;

            case 7:
                denom = Math.Pow(2, GetCombo(operand));
                result = RegisterA / denom;
                RegisterC = (long)result;
                pointer += 2;
                break;
        }

        return pointer;
    }

    private long GetCombo(int operand)
    {
        if (operand == 4)
        {
            return RegisterA;
        }
        else if (operand == 5)
        {
            return RegisterB;
        }
        else if (operand == 6)
        {
            return RegisterC;
        }
        else if (operand == 7)
        {
            throw new Exception("wrong number");
        }
        else
        {
            return operand;
        }
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        var startbits = 10;
        var numbersToTest = Math.Pow(2, startbits);
        var candidates = new List<long>();

        for (int num = 0; num < numbersToTest; num++)
        {
            RegisterA = num;
            RegisterB = _startB;
            RegisterC = _startC;
            var pointer = 0;
            Output.Clear();

            while (pointer < Program.Count)
            {
                var opcode = Program[pointer];
                var operand = Program[pointer + 1];

                pointer = DoOperation(pointer, opcode, operand);

                if (Output.Count >= 1)
                {
                    if (Output[0] == Program[0])
                    {
                        candidates.Add(num);
                        break;
                    }
                }
            }
        }

        for (int i = 1; i < Program.Count; i++)
        {
            // add 3 bits 
            var newCandidates = new List<long>();

            var bitPos = startbits + 3 * (i - 1);
            for (int addbits = 0; addbits < 8; addbits++)
            {
                var addValue = (long)(addbits * Math.Pow(2, bitPos));

                foreach (var candidate in candidates)
                {
                    var newTest = candidate + addValue;

                    RegisterA = newTest;
                    RegisterB = _startB;
                    RegisterC = _startC;
                    var pointer = 0;
                    Output.Clear();

                    while (pointer < Program.Count)
                    {
                        var opcode = Program[pointer];
                        var operand = Program[pointer + 1];

                        pointer = DoOperation(pointer, opcode, operand);
                        if (Output.Count >= i + 1)
                        {
                            if (Output[i] == Program[i])
                            {
                                newCandidates.Add(newTest);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            candidates = newCandidates;
        }

        return candidates.First();
    }
}
