using System.Diagnostics;

namespace aoc_2023;

public static class Extractor
{
    public static string[] Extract(string path) => File.ReadAllLines(GetFilePath(path));

    private static bool IsDebuggerAttached() => Debugger.IsAttached;

    private static string GetFilePath(string path) =>
        IsDebuggerAttached() ? @$"E:\Repository\Github\Projects\aoc-2023\aoc-2023\{path}" : path;
}
