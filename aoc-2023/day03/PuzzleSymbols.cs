namespace aoc_2023.day03;

public static class PuzzleSymbols
{
    public static (char[] symbols, string[] lines) Prep()
    {
        var symbols = new List<string>();
        var lines = File.ReadAllLines(@"day03\input.txt");

        for (int i = 0; i < lines.Length; i++)
        {
            for (int k = 0; k < lines[i].Length; k++)
            {
                var c = lines[i][k];

                // we don't care about the dots(.), so we continue until we find not a dot
                if (c == '.')
                    continue;

                // we also don't care about any numbers here, we're just looking for symbols
                if (!char.IsDigit(c))
                {
                    symbols.Add(c.ToString());
                }
            }
        }

        // we return the unique symbols found as well as all the lines from the file
        return (symbols.SelectMany(z => z.ToCharArray()).Distinct().ToArray(), lines);
    }
}
