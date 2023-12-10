namespace aoc_2023.day01;

public class PartOne
{
    public static void Process()
    {
        var result = 0;
        var lines = Extractor.Extract(@"day01\input.txt");

        foreach (var line in lines)
        {
            /*
                OPTIMIZE
                - we can use a regex to extract the first and last number from the string,
                - this will allow us to remove the Where() and ToArray() calls.

                SUGGEST
                However, if we're fixed on using LINQ, we can use the following:
                - var digits = line.Where(c => c >= '0' && c <= '9').ToArray();

                or simply just replace 'Char' with 'char' call:
                - var digits = line.Where(char.IsDigit).ToArray();
             */

            var digits = line.Where(Char.IsDigit).ToArray();
            if (digits.Length > 0)
            {
                /*
                    We need to convert the char array to a string, then parse it to an int
                    we can also use the following:

                    SUGGEST
                    - result += int.Parse($"{digits[0]}{digits[^1]}");
                 */
                result += int.Parse($"{digits.First()}{digits.Last()}");
            }
        }

        Console.WriteLine(result);
    }
}
