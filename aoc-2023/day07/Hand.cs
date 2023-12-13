namespace aoc_2023;

public class Hand : IComparable<Hand>
{
    private readonly char[] Ranks =
    [
        'A',
        'K',
        'Q',
        'J',
        'T',
        '9',
        '8',
        '7',
        '6',
        '5',
        '4',
        '3',
        '2'
    ];
    private readonly Dictionary<char, int> cardCounts = [];
    private readonly string cards;
    private readonly int bid;
    public int Bid => bid;

    public Hand(string cards, int bid)
    {
        this.cards = cards;
        this.bid = bid;

        // the dictionary needs to be initialised with starting ranks (so we set the count for each card to 0)
        foreach (var rank in Ranks)
        {
            cardCounts.Add(rank, 0);
        }

        // now for each of the cards in the hand we increment the count for that card by 1
        for (int i = 0; i < cards.Length; i++)
        {
            cardCounts[cards[i]]++;
        }
    }

    public int GetHandStrength()
    {
        // https://blog.stackademic.com/building-a-simple-poker-hand-evaluator-in-c-1bb81676c25c#:~:text=8.-,IsTwoPair,with%20the%20same%20face%20value.

        // we have 5 cards, so we return 7 (i.e. Five of a Kind)
        if (cardCounts.Any(c => c.Value == 5))
            return 7;

        // we have 4 cards, so we return 6 (i.e. Four of a Kind)
        if (cardCounts.Any(c => c.Value == 4))
            return 6;

        // we have 5 cards, so we return 5 (i.e. Full House)
        if (cardCounts.Any(c => c.Value == 3) && cardCounts.Any(c => c.Value == 2))
            return 5;

        // we have 3 cards, so we return 3 (i.e. Three of a Kind)
        if (cardCounts.Any(c => c.Value == 3))
            return 4;

        // we have 2 cards, so we return 2 (i.e. Two Pairs)
        if (cardCounts.Count(c => c.Value == 2) == 2)
            return 3;

        // we have 2 cards, so we return 2 (i.e. One Pair)
        if (cardCounts.Any(c => c.Value == 2))
            return 2;

        // high card
        return 1;
    }

    public int CompareTo(Hand? other)
    {
        // we need to check if the other hand is null
        ArgumentNullException.ThrowIfNull(other);

        // we need to perform some kind of comparison between two hands
        var cardStrength = GetHandStrength().CompareTo(other.GetHandStrength());

        // if the card is not equal we just return the card strength,
        // otherwise we need to loop through each card and compare their ranks
        if (cardStrength != 0)
        {
            return cardStrength;
        }
        else
        {
            if (cards != null && other.cards != null)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    /*
                    NOTE
                        We've ran into a scenario where we have two hands with the same card(s). So we need to compare the cards to see which one is higher.
                        This can be done by comparing the indexes of the cards in the array.

                        For example, if we have two hands with the same card(s) and the cards are:
                        e.g TA834Q and TK1233

                        Both the starting cards (T) are the same so we move to the next card index (A and K). We compare the indexes of these cards and because they are ordered the A is higher than the K, and we return the higher card.

                        https://learn.microsoft.com/en-us/dotnet/api/system.array.indexof?view=net-8.0

                     */

                    var compare = Array
                        .IndexOf(Ranks, other.cards[i])
                        .CompareTo(Array.IndexOf(Ranks, cards[i]));

                    // Console.WriteLine($"{other.cards[i]} vs {cards[i]}: {compare}");

                    if (compare != 0)
                        return compare;
                }
            }
        }

        // if we get here then the hands are equal
        return 0;
    }
}
