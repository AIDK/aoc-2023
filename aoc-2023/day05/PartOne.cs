namespace aoc_2023.day05;

public static class PartOne
{
    public static void Process()
    {
        using var inputStream = Extractor.ExtractUsingFileStream(@"day05\input.txt");
        using var reader = new StreamReader(inputStream);

        /*
        NOTE
            ReadLine should never return null because we know that the file is not empty but the
            compiler doesn't know that so we need to tell it that we know that the result will never be null.
                   
            This can be done using the null-forgiving operator or we just throw an exception in the case that the result is null.
        */

        var line = reader.ReadLine() ?? throw new Exception("Invalid input");

        /*
            SUGGEST: Use range operator
            var seedsLine = reader.ReadLine()["seeds:".Length..];

            reader.ReadLine().Substring("seeds:".Length) will complain because it thinks that the result can be null.
            We can use the null-forgiving operator to tell the compiler that we know that the result will never be null
            
            https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving
            
         */
        var seeds = line.Substring("seeds:".Length)
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(long.Parse)
            .ToArray();

        // we skip the empty line
        reader.ReadLine();

        /*
        NOTE
        Categories:
           seed-to-soil map:
           soil-to-fertilizer map:
           fertilizer-to-water map:
           water-to-light map:
           light-to-temperature map:
           temperature-to-humidity map:
           humidity-to-location map:
         */

        // 7 = number of categories (see above)
        for (int i = 0; i < 7; i++)
        {
            var seedRanges = new List<SeedRange>();

            // skip the category header (e.g. "seed-to-soil map:")
            reader.ReadLine();

            // read the first line (e.g. "1 2 3")
            line = reader.ReadLine();

            // read until we reach an empty line or the end of the file
            while (!string.IsNullOrEmpty(line))
            {
                // e.g. "1 2 3" => [1, 2, 3]
                var range = line.Split(" ").Select(long.Parse).ToArray();
                // Console.WriteLine($"{range[0]} {range[1]} {range[2]}");

                seedRanges.Add(new SeedRange(range[0], range[1], range[2]));

                /*
                    Read the next line (important to avoid infinite loop).
                    NOTE: line can be null (end of file) and if null we will move to the next category
                 */
                line = reader.ReadLine();
            }

            /*
                SUGGEST: use collection expression
                var groups = new RangeGroup([.. seedRanges]);
             */

            var groups = new RangeGroup(seedRanges.ToArray());
            for (int k = 0; k < seeds.Length; k++)
            {
                // we update the seed value with the new value
                // e.g. 1 => 2
                seeds[k] = groups.Map(seeds[k]);
            }
        }

        Console.WriteLine(seeds.Min());
    }
}
