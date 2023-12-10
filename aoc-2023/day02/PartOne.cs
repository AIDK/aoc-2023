using System.Text.RegularExpressions;

namespace aoc_2023.day02;

public static partial class PartOne
{
    private static readonly Dictionary<string, int> colors =
        new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

    #region  RegEx - https://regex101.com/

    [GeneratedRegex(@"(?<number>\d+) (?<color>red|green|blue)", RegexOptions.IgnoreCase, "en-ZA")]
    private static partial Regex NumberColorRegEx();

    [GeneratedRegex(@"(?<Game>\d+)", RegexOptions.IgnoreCase)]
    private static partial Regex GameIdRegEx();

    private static readonly Regex numberColorRegEx = NumberColorRegEx();
    private static readonly Regex gameIdRegEx = GameIdRegEx();

    #endregion

    /*
        Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
    */

    public static void Process()
    {
        var lines = Extractor.Extract(@"day02\input.txt");
        var result = new List<int>();
        var sum = 0;

        foreach (var line in lines)
        {
            // we extract the game id from our input
            var id = ExtractId(line);
            // now we get our game sets (1 blue, 2 red, etc)
            var sets = line.Split(":", StringSplitOptions.TrimEntries)[1].Split(
                ";",
                StringSplitOptions.TrimEntries
            );

            foreach (var set in sets)
            {
                // we split our sets so we can retrieve the individual cube results (1 blue, etc)
                var results = set.Split(",", StringSplitOptions.TrimEntries);

                // we attempt retrieving the game id where the current number of the cube is
                // outside our max cube count (red 12, green 13, blue 14)

                // SUGGEST: for readability we can make use of a for loop here instead of LINQ?
                result.AddRange(
                    from resultItem in results

                    // here we use our custom "number color" regex to get the individual set details (1 blue)
                    let match = numberColorRegEx.Match(resultItem)
                    let number = match.Groups["number"].Value
                    let color = match.Groups["color"].Value

                    // we're looking for any result where the current number
                    // of the set is greater than the allowed max cube for the colored cube

                    // SUGGEST: we should'nt need to convert to uint32, we should be able to just parse to int
                    where Convert.ToUInt32(number) > colors[color]

                    // we only want to game id
                    // (will be used later to exclude the invalid ids from our Sum())
                    select id
                );
            }

            // our result object will contain all our invalid game sets, so we
            // check whether the current game id exists, and if not we add it to our sum (we want to count all the valid game ids)
            if (!result.Contains(id))
                sum += id;
        }

        Console.WriteLine(sum);
    }

    private static int ExtractId(string line)
    {
        // we make use of our custom regex to get the game id
        var match = gameIdRegEx.Match(line);
        return Convert.ToInt32(match.Groups["Game"].Value);
    }
}
