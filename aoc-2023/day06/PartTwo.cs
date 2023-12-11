using System.Linq;

namespace aoc_2023.day06;

public static class PartTwo
{
    public static void Process()
    {
        /*
            Time:      71530
            Distance:  940200
        */

        var lines = Extractor.Extract(@"day06/input.txt");
        var time = GetNumbers(lines[0]);
        var output = 0L;

        for (long i = 0; i < time; i++)
        {
            // we perform the same calculation as in PartOne
            if ((time - i) * i > GetNumbers(lines[1]))
            {
                // we increment our output by 1 if the condition is met (i.e we have a way to win)
                output++;
            }
        }

        Console.WriteLine(output);
    }

    private static long GetNumbers(string line)
    {
        // Extract the numbers from the line and remove all spaces from it
        var numbers = line.Split(
            ":",
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
        )[1].Replace(" ", string.Empty);

        return long.Parse(numbers);
    }
}
