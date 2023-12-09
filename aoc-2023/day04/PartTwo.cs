using System.Text.RegularExpressions;

namespace aoc_2023.day04;

public static partial class PartTwo
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
        var cardCount = Array.Empty<int>();

        /*
             We want to get a list of all the cards we have to start with,
             so we can initialize our 'cardCount' array and set each card index to 1

            e.g. cardCount[0] = 1, cardCount[1] = 1, cardCount[2] = 1, etc.
            
            An very easy way to do this is to just use a standard for loop, but i wanted to try out Aggregate :)
        */

        // https://www.codingame.com/playgrounds/213/using-c-linq---a-practical-overview/aggregate
        _ = lines.Aggregate(
            0, // seed value (starting point)
            (a, _) => // function to apply to each element
            {
                Array.Resize(ref cardCount, cardCount.Length + 1); // resize the array
                cardCount[a] = 1; // set the current index to 1
                return a + 1; // return the next index
            }
        );

        for (int i = 0; i < lines.Length; i++)
        {
            // get the current card id
            var currentId = i;

            // get the numbers from the current line
            var numbers = lines[i].Split(":", StringSplitOptions.TrimEntries)[1];

            // get the total number of matches for the current line
            var matches = GetTotalMatches(numbers.Split("|", StringSplitOptions.TrimEntries));

            // Console.WriteLine($"Card {i + 1} has {matches} matches");
            for (int k = 0; k < matches; k++)
            {
                // get the id of the matching card
                var matchingId = k;

                /*
                    We need to update our 'cardCount' array to reflect the number of matches for each card.
                    In order to do this, we need to add the current count to the next count.

                    We start by getting the current relevant index in our 'cardCount' array by using 'i' (which is the current card),
                    and then we add 1 to get the next card and then we add 'k' which is the the index of the corresponding matching card.

                    we then increase the count of that card by the number of copies of the current card
                */

                cardCount[currentId + matchingId + 1] += cardCount[currentId];
            }
        }

        Console.WriteLine(cardCount.Sum());
    }

    private static int GetTotalMatches(string[] numbers)
    {
        // get the winning numbers and my numbers
        var winningNumbers = ExtractNumbers(numbers[0]);
        var myNumbers = ExtractNumbers(numbers[1]);

        // we return the count of the intersection of the winning numbers and my numbers
        // i.e. the total number of matches
        return winningNumbers.Intersect(myNumbers).Count();
    }
}
