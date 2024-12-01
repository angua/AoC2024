﻿using System;
using System.Windows.Media;

namespace AoC2024.Days;

public class Day
{
    public int DayNumber { get; set; }

    public string Title { get; set; }

    public ImageSource Image { get; set; }

    public Func<object> CreateViewModel { get; set; }

    public Type ViewType { get; set; }

}
