namespace aoc_2023.day05;

public class RangeGroup(SeedRange[] ranges)
{
    private readonly SeedRange[]? ranges = ranges;

    public long Map(long seed)
    {
        if (ranges != null)
        {
            foreach (var range in ranges)
            {
                if (range.IsInRange(seed))
                {
                    return range.MapSeedRange(seed);
                }
            }
        }

        return seed;
    }
}
