namespace aoc_2023.day05;

public class SeedRange()
{
    private readonly long destinationStart;
    private readonly long sourceStart;
    private readonly long rangeLength;

    public SeedRange(long destinationStart, long sourceStart, long rangeLength)
        : this()
    {
        this.destinationStart = destinationStart;
        this.sourceStart = sourceStart;
        this.rangeLength = rangeLength;
    }

    public bool IsInRange(long value) =>
        value >= sourceStart && value < (sourceStart + rangeLength);

    public long MapSeedRange(long value) => destinationStart + (value - sourceStart);
}
