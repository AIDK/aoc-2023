using System.Text.RegularExpressions;

namespace aoc_2023.day04;

public static partial class PartOne
{
    [GeneratedRegex(@"(\d+)")]
    private static partial Regex MyRegex();

    private static int[] ExtractNumbers(string numbers) =>
        MyRegex().Matches(numbers).Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();

    /*
    Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
    */
    public static void Process()
    {
        var lines = Extractor.Extract(@"day04/input.txt");
        int sum = 0;

        // extract the card numbers
        var cardNumbers = lines
            .Select(games => games.Split(":", StringSplitOptions.TrimEntries)[1])
            .Select(numbers => numbers.Split("|", StringSplitOptions.TrimEntries))
            .ToArray();

        foreach (var numberSets in cardNumbers)
        {
            // extract the winning numbers and my numbers
            var winningNumbers = ExtractNumbers(numberSets[0]);
            var myNumbers = ExtractNumbers(numberSets[1]);

            // compare the two sets of numbers and count the total matches
            var totalMatches = winningNumbers.Intersect(myNumbers).Count();

            /*
            NOTE
                Now we need to calculate the total number of matches and multiply 2 by itself the number of times equal to the total matches,
                e.g. if there are 3 matches, we do 2 * 2 * 2

                We have to make sure to check whether there is only 1 match or whether this is the first interation of the loop, in which case
                we set the count to 1, otherwise we multiply the count by 2

                OPTIMIZE: the for loop be simplified by implementing left-bit shift operator (<<)
                https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/left-shift-operator
               
                e.g count = 1 << 3; is the same as count = 1 * 2 * 2 * 2;
                e.g.count = 1 << 2; is the same as count = 1 * 2 * 2;
                e,g.count = 1 << 1; is the same as count = 1 * 2;
            */

            var count = 0;
            for (int i = 0; i < totalMatches; i++)
            {
                count = i == 0 ? 1 : count * 2;
            }

            sum += count;
        }

        Console.WriteLine(sum);
    }
}
