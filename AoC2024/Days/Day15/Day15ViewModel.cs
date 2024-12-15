using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Advent2024.Utils;
using AoC2024Lib.Days.Day15Lib;
using CommonWPF;

namespace AoC2024.Days.Day15;

public class Day15ViewModel : ViewModelBase
{
    public Day15Work Worker { get; } = new();

    private BitmapGridDrawing _gridDrawing;
    public Image MapImage
    {
        get => GetValue<Image>();
        set => SetValue(value);
    }

    public int NextInstructionNum
    {
        get => GetValue<int>();
        set
        {
                SetValue(value);
            if (value < Worker.Instructions.Count)
            {
                NextInstruction = Worker.Instructions[NextInstructionNum];
            }
            else
            {
                NextInstruction = new Vector2(0, 0);
            }
        }
    }
    public Vector2 NextInstruction
    {
        get => GetValue<Vector2>();
        set => SetValue(value);
    }

    private CancellationTokenSource _tokenSource;
    private CancellationToken _token;

    public bool MoveResult
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }


    public long Part1Solution
    {
        get => GetValue<long>();
        set => SetValue(value);
    }
    public long Part2Solution
    {
        get => GetValue<long>();
        set => SetValue(value);
    }

    public Day15ViewModel()
    {
        NextMove = new RelayCommand(CanNextMove, DoNextMove);
        Reset = new RelayCommand(CanReset, DoReset);
        Run = new RelayCommand(CanRun, DoRun);
        // Stop = new RelayCommand(CanStop, DoStop);

        var fileData = ResourceUtils.LoadDataFromResource("Day15", "input.txt");

        Worker.ParseGrids(fileData);
        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);

        DoReset();
        Draw();
    }
    public RelayCommand NextMove { get; }
    public bool CanNextMove()
    {
        return NextInstructionNum < Worker.Instructions.Count;
    }
    public void DoNextMove()
    {
        MoveResult = Worker.PerformInstruction(NextInstructionNum);
        NextInstructionNum++;
        Draw();
    }

    public RelayCommand Reset { get; }
    public bool CanReset()
    {
        return true;
    }
    public void DoReset()
    {
        Worker.Reset();
        NextInstructionNum = 0;
        Draw();
    }

    public RelayCommand Run { get; }
    public bool CanRun()
    {
        return true;
    }
    public void DoRun()
    {
        _tokenSource = new CancellationTokenSource();
        _token = _tokenSource.Token;

        StartRunning();
    }
    private async Task StartRunning()
    {
        var num = NextInstructionNum;
        while (num < Worker.Instructions.Count)
        {
            Worker.PerformInstruction(num);
            App.Current.Dispatcher.Invoke(() =>
            {
                NextInstructionNum = num;
                Draw();
            });
            if (_token.IsCancellationRequested)
            {
                break;
            }
            await Task.Delay(10);
            num++;
        }
    }



    public void Draw()
    {

        _gridDrawing = new BitmapGridDrawing((int)Worker.LargeGrid.Max(p => p.Key.Y) + 1, (int)Worker.LargeGrid.Max(p => p.Key.X) + 1, 7, 1);

        foreach (var location in Worker.LargeGrid)
        {
            var color = new Color();
            color = location.Value switch
            {
                '#' => Color.FromRgb(50, 50, 50),
                '@' => Color.FromRgb(0, 0, 255),
                '[' => Color.FromRgb(255, 0, 0),
                ']' => Color.FromRgb(255, 128, 0),
                _ => Color.FromRgb(200, 200, 200)

            };
            _gridDrawing.FillGridCell((int)location.Key.X, (int)location.Key.Y, color);
        }

        var image = new Image();
        image.Stretch = Stretch.None;
        image.Margin = new Thickness(0);
        image.Source = _gridDrawing.Picture;
        MapImage = image;

        RaisePropertyChanged(nameof(MapImage));
    }


}
