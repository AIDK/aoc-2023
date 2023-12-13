namespace aoc_2023.day07;

public static class PartOne
{
    public static void Process()
    {
        /*
        NOTE
            Thanks to @mzikmunddev for the help with this one:
            https://twitter.com/mzikmunddev
        */

        var lines = Extractor.Extract(@"day07/input.txt");
        var hands = new List<Hand>();
        var total = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            // we get the hand and the bid
            var detail = lines[i].Split(
                " ",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            );

            var hand = detail[0];
            var bid = int.Parse(detail[1]);
            // Console.WriteLine($"{hand} : {bid}");

            // we now pass the hand and the bid to our class to create a new hand
            hands.Add(new Hand(hand, bid));
        }

        // performing the default sort will kick off the IComparable<T> process
        hands.Sort();

        for (int i = 0; i < hands.Count; i++)
        {
            // we need to multiply the bid of the hand by its rank
            total += hands[i].Bid * (i + 1);
        }

        Console.WriteLine(total);
    }
}
