using System.Text.RegularExpressions;

namespace aoc_2023.day02;

public static partial class PartTwo
{
    [GeneratedRegex(@"(?<number>\d+) (?<color>red|green|blue)", RegexOptions.IgnoreCase, "en-ZA")]
    private static partial Regex NumberColorRegEx();

    private static readonly Regex numberColorRegEx = NumberColorRegEx();

    /*
     Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
     Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
     Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
     Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
     Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
 */

    private const string Red = "red";
    private const string Blue = "blue";
    private const string Green = "green";

    public static void Process(string fileName)
    {
        var lines = File.ReadAllLines(Path.Combine(Global.InputFilePath, "day02", fileName));
        var powerSet = new List<int>();

        foreach (var line in lines)
        {
            // we have to keep track of the which of the numbered cubes is the largest per color,
            // so we create a dictionary object to store the highest numbered color value
            var colorValues = new Dictionary<string, int>
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 }
            };

            var sets = line.Split(":", StringSplitOptions.TrimEntries)[1].Split(
                ";",
                StringSplitOptions.TrimEntries
            );

            foreach (var set in sets)
            {
                var cubes = set.Split(",", StringSplitOptions.TrimEntries);

                foreach (var cube in cubes)
                {
                    // we make use of our custom "number color" regex pattern - https://regex101.com/
                    var match = numberColorRegEx.Match(cube);
                    var number = int.Parse(match.Groups["number"].Value);
                    var color = match.Groups["color"].Value;

                    // now we need to store the highest color value, remembering that the dictionary can already
                    // contain a color value and that it shouldn't be replaced if its higher than the cube value.
                    // so we use Math.Max in order to get the larger value between the two possible values
                    colorValues[color] = Math.Max(colorValues[color], number);
                }
            }

            // now we have to calculate the power of for each set of values
            powerSet.Add(CalculatePower(colorValues));
        }

        Console.WriteLine(powerSet.Sum());
    }

    private static int CalculatePower(Dictionary<string, int> colorValues) =>
        // we simply calculate Red * Green * Blue
        colorValues[Red] * colorValues[Green] * colorValues[Blue];
}
