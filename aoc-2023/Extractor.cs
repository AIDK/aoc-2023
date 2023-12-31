﻿using System.Diagnostics;

namespace aoc_2023;

public static class Extractor
{
    public static string[] Extract(string path) => File.ReadAllLines(GetFilePath(path));

    private static bool IsDebuggerAttached() => Debugger.IsAttached;

    private static string GetFilePath(string path) =>
        IsDebuggerAttached() ? $"{FileSystem.FilePath()}{path}" : path;

    public static FileStream ExtractUsingFileStream(string path) =>
        File.OpenRead(GetFilePath(path));
}
