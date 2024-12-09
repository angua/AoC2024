using Common;

namespace AoC2024Lib.Days.Day09Lib;

public class Day09Work
{
    private List<DataBlock> _blocks1 = new();
    private List<DataBlock> _blocks2 = new();

    public long CalculatePart1Solution(Filedata fileData)
    {
        var input = fileData.Lines[0];
        _blocks1 = ParseBlocks(input);

        while (true)
        {
            var firstEmptyBlock = _blocks1.First(b => b.IsEmpty == true);
            var firstEmptyPosition = _blocks1.IndexOf(firstEmptyBlock);

            var lastFilledBlock = _blocks1.Last(b => b.IsEmpty == false);
            var lastFilledBlockPosition = _blocks1.IndexOf(lastFilledBlock);

            if (firstEmptyPosition > lastFilledBlockPosition)
            {
                // finished
                break;
            }

            MoveData(_blocks1, firstEmptyBlock, firstEmptyPosition, lastFilledBlock, lastFilledBlockPosition);
        }

        var checksum = CalculateChecksum(_blocks1);
        return checksum;
    }

    public long CalculatePart2Solution(Filedata fileData)
    {
        var input = fileData.Lines[0];
        _blocks2 = ParseBlocks(input);

        for (int i = _blocks2.Max(b => b.Id); i >= 0; i--)
        {
            var lastFilledBlock = _blocks2.First(b => b.Id == i);
            var lastFilledBlockPosition = _blocks2.IndexOf(lastFilledBlock);

            var firstEmptyBlock = _blocks2.FirstOrDefault(b => b.IsEmpty == true && b.Count >= lastFilledBlock.Count);
            if (firstEmptyBlock == null)
            {
                continue;
            }
            var firstEmptyPosition = _blocks2.IndexOf(firstEmptyBlock);

            if (firstEmptyPosition > lastFilledBlockPosition)
            {
                continue;
            }

            MoveData(_blocks2, firstEmptyBlock, firstEmptyPosition, lastFilledBlock, lastFilledBlockPosition);
        }

        var checksum = CalculateChecksum(_blocks2);
        return checksum;
    }

    private List<DataBlock> ParseBlocks(string input)
    {
        var blocks = new List<DataBlock>();
        for (int i = 0; i < input.Length; i++)
        {
            var num = input[i];

            if (i % 2 == 0)
            {
                // file
                blocks.Add(new DataBlock()
                {
                    IsEmpty = false,
                    Id = i / 2,
                    Count = int.Parse(num.ToString())
                });
            }
            else
            {
                // empty space;
                if (num != '0')
                {
                    blocks.Add(new DataBlock()
                    {
                        IsEmpty = true,
                        Count = int.Parse(num.ToString())
                    });
                }
            }
        }
        return blocks;
    }

    private void MoveData(List<DataBlock> blocks, DataBlock firstEmptyBlock, int firstEmptyPosition, DataBlock lastFilledBlock, int lastFilledBlockPosition)
    {
        if (lastFilledBlock.Count > firstEmptyBlock.Count)
        {
            // fill from last
            firstEmptyBlock.IsEmpty = false;
            firstEmptyBlock.Id = lastFilledBlock.Id;

            // reduce last filled
            lastFilledBlock.Count -= firstEmptyBlock.Count;

            //Create empty space
            var newEmptyBlock = new DataBlock()
            {
                IsEmpty = true,
                Id = -1,
                Count = firstEmptyBlock.Count
            };
            blocks.Insert(lastFilledBlockPosition + 1, newEmptyBlock);

        }
        else if (lastFilledBlock.Count == firstEmptyBlock.Count)
        {
            // swap
            // fill from last
            firstEmptyBlock.IsEmpty = false;
            firstEmptyBlock.Id = lastFilledBlock.Id;

            // empty filled block
            lastFilledBlock.IsEmpty = true;
            lastFilledBlock.Id = -1;
        }
        else
        {
            // filled smaller tham empty
            // fill part of empty space
            var newEmpty = new DataBlock()
            {
                IsEmpty = true,
                Id = -1,
                Count = firstEmptyBlock.Count - lastFilledBlock.Count
            };
            blocks.Insert(firstEmptyPosition + 1, newEmpty);

            firstEmptyBlock.IsEmpty = false;
            firstEmptyBlock.Id = lastFilledBlock.Id;
            firstEmptyBlock.Count = lastFilledBlock.Count;

            // empty last filled
            lastFilledBlock.IsEmpty = true;
            lastFilledBlock.Id = -1;
        }
    }

    private long CalculateChecksum(List<DataBlock> blocks)
    {
        long checksum = 0;
        long mult = 0;
        for (int i = 0; i < blocks.Count; i++)
        {
            var block = blocks[i];

            if (block.IsEmpty)
            {
                for (int n = 0; n < block.Count; n++)
                {
                    mult++;
                }
            }
            else
            {
                for (int n = 0; n < block.Count; n++)
                {
                    checksum += mult * block.Id;
                    mult++;
                }
            }
        }
        return checksum;
    }

}
