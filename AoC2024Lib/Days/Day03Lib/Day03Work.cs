using Common;

namespace AoC2024Lib.Days.Day03Lib;

public class Day03Work
{
    private List<string> _mulSequences = new();

    public int CalculatePart1Solution(Filedata fileData)
    {



        foreach (var line in fileData.Lines)
        {
            var parts = line.Split("mul");
            _mulSequences.AddRange(parts);
        }

        var mulsum = 0;

        foreach (var sequence in _mulSequences)
        {
            var firstNumChars = new List<char>();
            var secondNumChars = new List<char>();


            if (!sequence.StartsWith("("))
            {
                continue;
            }
            if (!Char.IsDigit(sequence[1]))
            {
                continue;
            }
            var i = 1;
            while (Char.IsDigit(sequence[i]))
            {
                firstNumChars.Add(sequence[i++]);
            }
            if (!sequence[i].Equals(','))
            {
                continue;
            }
            i++;
            if (!Char.IsDigit(sequence[i]))
            {
                continue;
            }
            while (Char.IsDigit(sequence[i]))
            {
                secondNumChars.Add(sequence[i++]);
            }
            if (!sequence[i].Equals(')'))
            {
                continue;
            }
            var fisrtNumStr = string.Join("", firstNumChars);
            var secondNumStr = string.Join("", secondNumChars);

            var firstNum = int.Parse(fisrtNumStr);
            var secondNum = int.Parse(secondNumStr);

            var mul = firstNum * secondNum;
            mulsum += mul;
        }


        return mulsum;
    }

    public int CalculatePart2Solution(Filedata fileData)
    {
        var text = new List<string>();

        foreach (var line in fileData.Lines)
        {
            text.Add(line);
        }

        var fulltext = string.Join("", text);

        var doString = "do()";
        var dontString = "don't()";

        var stringsToExecute = new List<string>();


        var processNextPart = true;




        var doPosition = -1;
        var dontPosition = -1;

        while (fulltext.Length > 0)
        {



            doPosition = fulltext.IndexOf(doString);


            dontPosition = fulltext.IndexOf(dontString);


            if (doPosition > -1 && dontPosition > -1)
            {
                var nextPos = Math.Min(doPosition, dontPosition);
                var part = fulltext.Substring(0, nextPos);
                if (processNextPart)
                {
                    stringsToExecute.Add(part);
                }

                if (doPosition < dontPosition)
                {
                    processNextPart = true;
                }
                else
                {
                    processNextPart = false;
                }
                fulltext = fulltext.Substring(nextPos + 1);
            }

            else if (doPosition > -1)
            {
                var part = fulltext.Substring(0, doPosition);
                if (processNextPart)
                {
                    stringsToExecute.Add(part);
                }
                processNextPart = true;
                fulltext = fulltext.Substring(doPosition + 1);

            }

            else if (dontPosition > -1)
            {
                var part = fulltext.Substring(0, dontPosition);
                if (processNextPart)
                {
                    stringsToExecute.Add(part);
                }
                processNextPart = false;
                fulltext = fulltext.Substring(dontPosition + 1);

            }
            else
            {
                if (processNextPart)
                {
                    stringsToExecute.Add(fulltext);
                }
                fulltext = "";
            }

        }

            var mulsum = 0;

        
        foreach (var str in stringsToExecute)
        {

            var sequences = new List<string>();
            var parts = str.Split("mul");
            sequences.AddRange(parts);

            foreach (var sequence in sequences)
            {
                var firstNumChars = new List<char>();
                var secondNumChars = new List<char>();


                if (!sequence.StartsWith("("))
                {
                    continue;
                }
                if (!Char.IsDigit(sequence[1]))
                {
                    continue;
                }
                var i = 1;
                while (Char.IsDigit(sequence[i]))
                {
                    firstNumChars.Add(sequence[i++]);
                }
                if (!sequence[i].Equals(','))
                {
                    continue;
                }
                i++;
                if (!Char.IsDigit(sequence[i]))
                {
                    continue;
                }
                while (Char.IsDigit(sequence[i]))
                {
                    secondNumChars.Add(sequence[i++]);
                }
                if (!sequence[i].Equals(')'))
                {
                    continue;
                }
                var fisrtNumStr = string.Join("", firstNumChars);
                var secondNumStr = string.Join("", secondNumChars);

                var firstNum = int.Parse(fisrtNumStr);
                var secondNum = int.Parse(secondNumStr);

                var mul = firstNum * secondNum;
                mulsum += mul;
            }


        }


        return mulsum;
    }


}
