using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace aoc_2023.day03;

public static partial class PartTwo
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegEx();

    [GeneratedRegex(@"([^\.\d\n])")]
    private static partial Regex SymbolRegEx();

    #region Records
    private record Number(int Value, Pos Pos);

    private record Symbol(string Value, Pos Pos);

    private record Pos(int Row, int ColStart, int ColEnd)
    {
        public bool IsAdjacent(Number number)
        {
            if (Row < number.Pos.Row - 1 || Row > number.Pos.Row + 1)
                return false;

            return ColStart >= number.Pos.ColStart - 1 && ColEnd <= number.Pos.ColEnd + 1;
        }
    }

    #endregion


    public static void Process()
    {
        var lines = File.ReadAllLines(@"day03\input.txt");
        var numberRegex = NumberRegEx();
        var symbolRegex = SymbolRegEx();
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();
        var sum = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            // we need to start by getting all the numbers and symbols from the input,
            // so we start by looping through any numbers we find using our regex pattern
            foreach (var number in numberRegex.Matches(line).Cast<Match>())
            {
                // we then add the number to our list of numbers, along with the position of the number in the input
                numbers.Add(
                    new Number(
                        int.Parse(number.Value, CultureInfo.InvariantCulture),
                        new Pos(i, number.Index, number.Index + number.Length)
                    )
                );
            }

            // once we've gotten all the numbers we can then get all the symbols from the input
            foreach (var symbol in symbolRegex.Matches(line).Cast<Match>())
            {
                // we only care about the "*" symbol, so we can filter out all the other symbols
                if (symbol.Value != "*")
                    continue;

                // we then add the symbol to our list of symbols, along with the position of the symbol in the input
                symbols.Add(
                    new Symbol(symbol.Value, new Pos(i, symbol.Index, symbol.Index + symbol.Length))
                );
            }
        }

        /*
            We need to calculate the ratio of adjacent numbers multiplied by each other, where the symbol is "*".
            We can do this by finding all the numbers that are adjacent to a symbol, and then multiplying them together.
            We can then sum all the results together to get the final ratio.
        */

        foreach (var item in symbols)
        {
            // then we get the numbers that are adjacent to the symbol
            var adjacentNumbers = numbers.Where(item.Pos.IsAdjacent).ToList();

            // then we multiply all the numbers together where there is more than one number (making use of aggregate function - pretty cool)
            // https://www.codingame.com/playgrounds/213/using-c-linq---a-practical-overview/aggregate
            sum +=
                adjacentNumbers.Count > 1
                    ? adjacentNumbers.Aggregate(1, (acc, n) => acc * n.Value)
                    : 0;
        }

        Console.WriteLine(sum);
    }
}
