using System.Text.RegularExpressions;

namespace aoc_2023.day03;

public static partial class PartOne
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex MyRegex();

    private static char[]? Symbols;

    public static void Process()
    {
        var sum = 0;

        // initial prework to get all lines from file as well as all unique symbols in file
        var prepResult = PuzzleSymbols.Prep();
        var lines = prepResult.lines; // this returns all the lines from our input file
        Symbols = prepResult.symbols; // this returns all the unique symbols from the input file

        for (int i = 0; i < lines.Length; i++)
        {
            for (int k = 0; k < lines[i].Length; k++)
            {
                // get the first characater in the
                // current row for the current column
                var character = lines[i][k];

                // we want to find the symbol so we can start the
                // process of looking around for any numbers so,
                // if the current character is a dot(.) we continue looping
                if (character == '.')
                    continue;

                // we start the process once we've found a symbol
                if (Symbols.Contains(character))
                {
                    // Console.WriteLine($"Character ({lines[i][k]}) found on line {i} column {k}");
                    var result = 0;
                    /*
                    NOTE
                        Now we need to perform a check on the grid to see whether there are any numbers around the symbol we found.
                        We need to check the current position outward 1 position at a time, this includes diagonal checks.

                        1) One way is to keep some sort of array with coordinates spanning 1 position outward in every direction -
                            (Left-Top, Left, Left-Bottom, Center, Center-Top, Center-Bottom, Right-Top, Right, Right-Bottom)
                    SUGGEST
                        2) Another is to check 3 lines of the grid at time i.e. Previous, Current and Next -
                            this should allow for diagonal checks (Left-Top, Left-Bottom, Right-Top, Right-Bottom)

                        3) Another way is to possibly make use of jagged arrays, that way we dont have to loop
                           through the result in the end to get the sum: - https://www.geeksforgeeks.org/c-sharp-jagged-arrays/
                           
                           int[][] numbers =
                            [
                                [GetNumber(k, lines[i - 1])],
                                [GetNumber(k, lines[i])],
                                [GetNumber(k, lines[i + 1])]
                            ];

                            var num1 = numbers[0][0];
                            num1 += numbers[1][0];
                            num1 += numbers[2][0];
                    */

                    var numbers = new int[,]
                    {
                        { GetNumber(k, lines[i - 1]) },
                        { GetNumber(k, lines[i]) },
                        { GetNumber(k, lines[i + 1]) }
                    };

                    for (int n = 0; n <= numbers.GetUpperBound(0); n++)
                    {
                        result += numbers[n, 0];
                    }

                    sum += result;
                }
            }
        }

        Console.WriteLine(sum);
    }

    private static int GetNumber(int pos, string line)
    {
        /*
            467..114..
            ...*......
            ..35..633.
            ......#...
            617*......
            .....+.58.
            ..592.....
            ......755.
            ...$.*....
            .664.598..
                
         */

        var position = new int[] { pos - 1, pos, pos + 1 };

        /*
           To help with checking diagonally around the symbol we can store the position of the symbol in the grid.
           We do this by keeping track of the symbol's current position [k],
           the position before the symbol [k - 1] and the position after the symbol [k + 1]
        */

        var count = 0;

        // we're going to make use of Regex to get the number
        // (makes it much easier to get the whole number in the end - doesnt require any additional loop etc.)
        var matches = MyRegex().Matches(line);
        foreach (var match in matches.Cast<Match>())
        {
            var number = match.Value;
            var length = match.Length;
            // gives us the index of where the number starts in the current row
            var idx = match.Index;
            var loopCount = idx + length;

            /*
                now that we've found a number, we can start to check whether its within the range of the symbol.
                we do this by looping through the number's indexes (i.e. starting index until the last index)
            */
            for (int l = idx; l < loopCount; l++)
            {
                // if current index (l), of the number matches PrevPos, CurPos or NextPos
                // then we've successfully matched a number around the symbol
                if (position.Contains(l))
                {
                    // we add the number found to our overall count
                    count += int.Parse(number);
                    break;
                }
            }

            continue;
        }

        return count;
    }
}
