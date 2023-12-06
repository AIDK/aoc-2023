using System.Data;
using System.Text.RegularExpressions;

namespace aoc_2023.day03;

public static partial class PartOne
{
    #region Regext

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();

    private static readonly Regex numberRegext = NumberRegex();

    #endregion

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

    private static readonly char[] Symbols = ['@', '#', '$', '%', '&', '*', '/', '+', '-', '='];

    public static void Process()
    {
        int count = 0;
        var lines = File.ReadAllLines(@"day03\input.txt");

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                // gets the current characater
                var c = lines[row][col];

                // if the current character is a dot we continue looping
                if (IsDot(c))
                    continue;

                // the above check passed so we're not a Dot so we have to check whether we're a
                // symbol and if so, start process of getting any numbers around the symbol
                if (Symbols.Contains(c))
                {
                    /*
                        In order to perform the diagonal check we need to check 3 rows at a time (previous, current and next),
                        this will allow us to find any numbers that are near to a symbol -
                        
                        (within 1 position of our current location: left-up, left, left-down, up, center, down, right-up, right, right-down). See Coords.png
                    */

                    // at this point we've located a symbol on the grid, so we now have to start looking
                    // for any adjacent numbers (remembering to keep the diagonal check in mind)
                    var result = Get(lines[row - 1], col);

                    // now we perform the same check but for the current row we're on
                    result += Get(lines[row], col);

                    // now we check the upcoming row for any numbers
                    result += Get(lines[row + 1], col);

                    count += result;
                }
            }
        }

        Console.WriteLine(count);
    }

    private static int Get(string line, int col)
    {
        /*
            We've located a symbol in the grid and thus we have to start checking for any numbers
            but we need to know where to check so we create a list of indexes on the grid to check around the symbol's position

            We know the current location of the symble (col),
            we need to check the position before the current (col - 1) and,
            we need to check the position after the current (col + 1)
        */

        int count = 0;
        // left position (col - 1) / current position (col) / right position (col + 1)
        var indexes = new List<int>() { col - 1, col, col + 1 };

        // once again we make use of Regex to pull the the numbers found per line
        var matches = numberRegext.Matches(line);
        foreach (var match in matches.Cast<Match>())
        {
            // get the numerical value (i.e. 416 or 667 etc)
            var number = match.Value;

            /*
                now we have to perform a check to determine whether any part of the number we've found exists within our "placeholder indexes"
                i.e. is the number within 1 position of our symbol?

                ....
                467.
                ...*
                
                OR
                ....
                633.
                #...
                
            */

            // we start by passing the current placeholder indexes,
            // the index of the matched number (regex allows us to gain this information)
            // and the length of the number found (will be used as part of our loop in the next method)
            if (CheckNumberIndex(indexes, match.Index, match.Length))
                // if the number we found is within our placeholder index range we add the number to our counter
                count += int.Parse(number);
        }

        return count;
    }

    private static bool CheckNumberIndex(List<int> idx, int startIndex, int length)
    {
        // now we loop through the number's indexes to determine whether its in range
        for (int i = startIndex; i < startIndex + length; i++)
        {
            // the number's index falls within our placeholder range so we can return True
            if (idx.Contains(i))
                return true;
        }

        // no match found (number is outside our placeholder range)
        return false;
    }

    private static bool IsDot(char c) => c == '.';
}
