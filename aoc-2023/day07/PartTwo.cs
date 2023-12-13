namespace aoc_2023.day07;

public static class PartTwo
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

    public class Hand : IComparable<Hand>
    {
        private readonly char[] Ranks =
        [
            'A',
            'K',
            'Q',
            'T',
            '9',
            '8',
            '7',
            '6',
            '5',
            '4',
            '3',
            '2',
            'J'
        ];
        private readonly Dictionary<char, int> cardCounts = [];
        private readonly string cards;
        private readonly int bid;
        public int Bid => bid;
        public int Joker { get; private set; }

        public Hand(string cards, int bid)
        {
            this.cards = cards;
            this.bid = bid;

            foreach (var rank in Ranks)
            {
                // the dictionary needs to be initialised with starting ranks (so we set the count for each card to 0)
                cardCounts.Add(rank, 0);
            }

            for (int i = 0; i < cards.Length; i++)
            {
                // we need to check if the card is a joker, if it is we increment the joker count
                // and continue because we want to exclude the joker from the card count
                if (cards[i] == 'J')
                {
                    Joker++;
                    continue;
                }

                // else we increment the card count
                cardCounts[cards[i]]++;
            }
        }

        public int GetHandStrength()
        {
            // https://blog.stackademic.com/building-a-simple-poker-hand-evaluator-in-c-1bb81676c25c#:~:text=8.-,IsTwoPair,with%20the%20same%20face%20value.

            // we have 5 cards, so we return 7 (i.e. Five of a Kind)
            if (cardCounts.Any(c => c.Value == (5 - Joker))) // i.e. 4 cards with the same rank + 1 joker => 5 of a kind
                return 7;

            // we have 4 cards, so we return 6 (i.e. Four of a Kind)
            if (cardCounts.Any(c => c.Value == (4 - Joker))) // i.e. 2 cards with the same rank + 2 jokers => 4 of a kind
                return 6;

            // we have 5 cards, so we return 5 (i.e. Full House)
            if (
                (cardCounts.Any(c => c.Value == 3) && cardCounts.Any(c => c.Value == 2)) // i.e. 3 cards with the same rank + 2 cards with the same rank
                || (Joker == 1 && cardCounts.Count(c => c.Value == 2) == 2) // i.e. 4 cards (2 pairs) + 1 joker
                || (Joker >= 2 && cardCounts.Any(c => c.Value == 2)) // i.e. 3 cards (1 pair) + 2 jokers
            )
                return 5;

            // we have 3 cards, so we return 3 (i.e. Three of a Kind)
            if (cardCounts.Any(c => c.Value == (3 - Joker))) // i.e. 1 card with the same rank + 2 jokers => 3 of a kind
                return 4;

            // we have 4 cards, so we return 2 (i.e. Two Pairs)
            if (
                cardCounts.Count(c => c.Value == 2) == 2 // i.e. 4 cards (2 pairs)
                || (Joker == 1 && cardCounts.Any(c => c.Value == 2)) // i.e. 3 cards (1 pair) + 1 joker
                || Joker == 2 // i.e. 2 cards (1 pair) + 2 jokers
            )
                return 3;

            // we have 2 cards, so we return 2 (i.e. One Pair)
            if (cardCounts.Any(c => c.Value == (2 - Joker))) // i.e. 1 card with the same rank + 1 joker => 2 of a kind
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
}
