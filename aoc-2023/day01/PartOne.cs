﻿namespace aoc_2023.day01;

public class PartOne
{
    public static void Process()
    {
        var result = 0;
        var filePath = Path.Combine(@"day01\input.txt");
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var digits = line.Where(Char.IsDigit).ToArray();
            if (digits.Length > 0)
            {
                result += int.Parse($"{digits.First()}{digits.Last()}");
            }
        }

        Console.WriteLine(result);
    }
}
