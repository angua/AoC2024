using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Advent2024.Utils;
using AoC2024Lib.Days.Day06Lib;
using CommonWPF;

namespace AoC2024.Days.Day06;

public class Day06ViewModel : ViewModelBase
{
    public Day06Work Worker { get; } = new();

    private BitmapGridDrawing _gridDrawing;
    private int _gridWidth;
    private int _gridHeight;

    public Image MapImage
    {
        get => GetValue<Image>();
        set => SetValue(value);
    }


    public int Part1Solution
    {
        get => GetValue<int>();
        set => SetValue(value);
    }
    public int Part2Solution
    {
        get => GetValue<int>();
        set => SetValue(value);
    }

    public Day06ViewModel()
    {
        var fileData = ResourceUtils.LoadDataFromResource("Day06", "input.txt");

        var grid = Worker.Parse(fileData);
        _gridWidth = (int)grid.Max(v => v.Key.X) + 1;
        _gridHeight = (int)grid.Max(v => v.Key.Y) + 1;

        _gridDrawing = new BitmapGridDrawing(_gridWidth, _gridHeight, 7, 1);

        foreach (var pos in grid)
        {
            var color = new Color();
            color = pos.Value switch
            {
                '.' => Color.FromRgb(230, 180, 100),
                '#' => color = Color.FromRgb(0, 128, 20),
                _ => Color.FromRgb(255, 128, 0)
            };

            _gridDrawing.FillGridCell((int)pos.Key.X, (int)pos.Key.Y, color);
        }

        var image = new Image();

        image.Stretch = Stretch.None;
        image.Margin = new Thickness(0);

        image.Source = _gridDrawing.Picture;

        MapImage = image;

        RaisePropertyChanged(nameof(MapImage));

        Part1Solution = Worker.CalculatePart1Solution(fileData);
        Part2Solution = Worker.CalculatePart2Solution(fileData);
    }
}
