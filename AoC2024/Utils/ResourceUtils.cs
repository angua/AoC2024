﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;

namespace Advent2024.Utils;

public static class ResourceUtils
{
    public static Filedata LoadDataFromResource(string folderName, string fileName)
    {
        using var stream = typeof(ResourceUtils).Assembly.GetManifestResourceStream($"AoC2024.Resources.Days.{folderName}.{fileName}");
        using var tr = new StreamReader(stream);

        return Filedata.CreateFromStream(tr);
    }

    internal static ImageSource LoadBitmapFromResource(string folderName, string fileName)
    {
        using var stream = typeof(ResourceUtils).Assembly.GetManifestResourceStream($"AoC2024.Resources.Days.{folderName}.{fileName}");
        var bitmap = new BitmapImage();

        if (stream != null)
        {
            bitmap.BeginInit();
            bitmap.StreamSource = stream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
        }

        return bitmap;
    }

    public static IEnumerable<string> ListFileNames(string folderName)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        string combinedFolderName = $"{executingAssembly.GetName().Name}.Resources.Days.{folderName}";
        return executingAssembly
            .GetManifestResourceNames()
            .Where(r => r.StartsWith(combinedFolderName))
            .Select(r => r.Substring(combinedFolderName.Length + 1));
    }

}
