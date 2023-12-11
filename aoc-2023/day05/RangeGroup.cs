namespace aoc_2023.day05;

public class RangeGroup()
{
    private readonly SeedRange[] ranges;

    public RangeGroup(SeedRange[] ranges)
        : this()
    {
        this.ranges = ranges;
    }

    public long Map(long value)
    {
        foreach (var range in ranges)
        {
            if (range.IsInRange(value))
            {
                return range.MapSeedRange(value);
            }
        }

        return value;
    }
}
