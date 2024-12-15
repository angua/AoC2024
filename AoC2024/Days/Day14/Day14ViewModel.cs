using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Advent2024.Utils;
using AoC2024Lib.Days.Day14Lib;
using CommonWPF;

namespace AoC2024.Days.Day14;

public class Day14ViewModel : ViewModelBase
{
    public Day14Work Worker { get; } = new();

    public int Seconds
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    public int SecondsInput
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    private BitmapGridDrawing _gridDrawing;
    public Image MapImage
    {
        get => GetValue<Image>();
        set => SetValue(value);
    }

    public long Part1Solution
    {
        get => GetValue<long>();
        set => SetValue(value);
    }

    private bool _running = false;
    private CancellationTokenSource _tokensource;
    private CancellationToken _token;

    public Day14ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day14", "input.txt");

        Part1Solution = Worker.CalculatePart1Solution(fileData);

        Worker.ParseRobots();

        Seconds = 0;
        Draw();

        NextSecond = new RelayCommand(CanNextSecond, DoNextSecond);
        PreviousSecond = new RelayCommand(CanPreviousSecond, DoPreviousSecond);
        Reset = new RelayCommand(CanReset, DoReset);
        Run = new RelayCommand(CanRun, DoRun);
        Stop = new RelayCommand(CanStop, DoStop);
        AfterSeconds = new RelayCommand(CanAfterSeconds, DoAfterSeconds);
        ShowTree = new RelayCommand(CanShowTree, DoShowTree);
    }

    public RelayCommand NextSecond { get; }
    public bool CanNextSecond()
    {
        return true;
    }
    public void DoNextSecond()
    {
        Seconds++;
        Worker.NextSecond();
        Draw();
    }

    public RelayCommand PreviousSecond { get; }
    public bool CanPreviousSecond()
    {
        return true;
    }
    public void DoPreviousSecond()
    {
        Seconds--;
        Worker.PreviousSecond();
        Draw();
    }

    public RelayCommand AfterSeconds { get; }
    public bool CanAfterSeconds()
    {
        return true;
    }
    public void DoAfterSeconds()
    {
        Seconds += SecondsInput;
        Worker.DoSeconds(SecondsInput);
        Draw();
    }

    public RelayCommand Reset { get; }
    public bool CanReset()
    {
        return true;
    }
    public void DoReset()
    {
        Seconds = 0;
        Worker.ParseRobots();
        Draw();
    }


    public RelayCommand Stop { get; }
    public bool CanStop()
    {
        return true;
    }
    public void DoStop()
    {
        _tokensource.Cancel();
    }

    public RelayCommand Run { get; }
    public bool CanRun()
    {
        return true;
    }
    public void DoRun()
    {
        _tokensource = new CancellationTokenSource();
        _token = _tokensource.Token;

        StartRunning();
    }
    private async Task StartRunning()
    {
        _running = true;

        while (_running)
        {
            Worker.NextSecond();
            App.Current.Dispatcher.Invoke(() =>
            {
                Seconds++;
                Draw();
            });
            if (_token.IsCancellationRequested)
            {
                _running = false;
                break;
            }
            await Task.Delay(100);
        }
    }

    // 7286
    // found by staring at the screen until the tree appeared
    public RelayCommand ShowTree { get; }
    public bool CanShowTree()
    {
        return true;
    }
    public void DoShowTree()
    {
        DoReset();
        SecondsInput = 7286;
        DoAfterSeconds();
    }

    public void Draw()
    {
        _gridDrawing = new BitmapGridDrawing(Worker.RoomHeight, Worker.RoomWidth, 7, 1);

        foreach (var robot in Worker.Robots)
        {
            var color = new Color();
            color = Color.FromRgb(230, 180, 100);
            _gridDrawing.FillGridCell((int)robot.Position.X, (int)robot.Position.Y, color);
        }

        var image = new Image();
        image.Stretch = Stretch.None;
        image.Margin = new Thickness(0);
        image.Source = _gridDrawing.Picture;
        MapImage = image;

        RaisePropertyChanged(nameof(MapImage));
    }

}
