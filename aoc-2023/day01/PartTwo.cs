using System.Text.RegularExpressions;

namespace aoc_2023.day01;

public class PartTwo
{
    private static readonly Dictionary<string, string> numberMap =
        new()
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" },
        };

    /*
    two1nine = 29
    eightwothree = 83
    abcone2threexyz = 13
    xtwone3four = 24
    4nineeightseven2 = 42
    zoneight234 = 14
    7pqrstsixteen = 76
    */

    // https://www.reddit.com/r/adventofcode/comments/188bu8v/comment/kbkm70x/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
    private static readonly string pattern = @"(\d|one|two|three|four|five|six|seven|eight|nine)";

    public static void Process()
    {
        var lines = Extractor.Extract(@"day01\input.txt");

        /*
          - we need to keep track of the first and last numbers found, so we creaet an array to hold these values
          - [[one, two], [three, four], [5, 6]]
        */
        var result = new int[lines.Length, 2];

        for (int i = 0; i < lines.Length; i++)
        {
            // using RegEx pattern matching we get the first occurance of a digit or "word number"
            var firstValue = Regex.Match(lines[i], pattern).Value;

            // using the same regex pattern matching we get the first occurance of a digit
            // or "word number" starting from the right side of the string
            var lastValue = Regex.Match(lines[i], pattern, RegexOptions.RightToLeft).Value;

            /*
              - now we store our "numbers" into our array dimentions,
              (multiply the result by 10 - so we can perform a Sum() on the results later)
            
              we also perform a quick check on the value to see whether its numeric or not,
              when not numeric we get the numeric counterpart from our "numberMap"
            */
            result[i, 0] =
                (
                    !IsDigit(firstValue)
                        ? Convert.ToInt32(numberMap[firstValue])
                        : Convert.ToInt32(firstValue)
                ) * 10;

            // last number
            result[i, 1] = !IsDigit(lastValue)
                ? Convert.ToInt32(numberMap[lastValue])
                : Convert.ToInt32(lastValue);
        }

        // here we sum our array results
        Console.WriteLine($"{result.Cast<int>().Sum()}");
    }

    private static bool IsDigit(string value) => int.TryParse(value, out _);
}
