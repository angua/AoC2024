using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Advent2024.Utils;
using AoC2024Lib.Days.Day16Lib;
using CommonWPF;

namespace AoC2024.Days.Day16;

public class Day16ViewModel : ViewModelBase
{
    public Day16Work Worker { get; } = new();

    private BitmapGridDrawing _background;
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
    public long Part2Solution
    {
        get => GetValue<long>();
        set => SetValue(value);
    }

    public Day16ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day16", "input.txt");

        FindNextConnection = new RelayCommand(CanFindNextConnection, DoFindNextConnection);

        Worker.Parse(fileData);
        CreateBackground();
        Draw();

         Part1Solution = Worker.CalculatePart1Solution(fileData);
        //Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
    public RelayCommand FindNextConnection { get; }
    public bool CanFindNextConnection()
    {
        return true;
    }
    public void DoFindNextConnection()
    {
        Worker.FindNextConnection();
        Draw();
    }

    private void CreateBackground()
    {
        _background = new BitmapGridDrawing((int)Worker.Grid.Max(p => p.Key.Y) + 1, (int)Worker.Grid.Max(p => p.Key.X) + 1, 7, 1);

        foreach (var location in Worker.Grid)
        {
            var color = new Color();
            color = location.Value switch
            {
                '#' => Color.FromRgb(50, 50, 50),
                _ => Color.FromRgb(200, 200, 200)
            };
            _background.FillGridCell((int)location.Key.X, (int)location.Key.Y, color);
        }
    }

    public void Draw()
    {
        _gridDrawing = new BitmapGridDrawing(_background);

        // connections
        foreach (var connection in Worker.Connections)
        {
            foreach (var pos in connection.Positions)
            {
                _gridDrawing.FillGridCell((int)pos.X, (int)pos.Y, Color.FromRgb(0, 0, 255));
            }
        }

        // crossroads
        foreach (var crossroad in Worker.Crossroads)
        {
            _gridDrawing.FillGridCell((int)crossroad.Position.X, (int)crossroad.Position.Y, Color.FromRgb(255, 0, 0));
        }
        foreach (var crossroad in Worker.AvailableCrossroads)
        {
            _gridDrawing.FillGridCell((int)crossroad.Position.X, (int)crossroad.Position.Y, Color.FromRgb(255, 128, 0));
        }

        var image = new Image();
        image.Stretch = Stretch.None;
        image.Margin = new Thickness(0);
        image.Source = _gridDrawing.Picture;
        MapImage = image;

        RaisePropertyChanged(nameof(MapImage));
    }

}
