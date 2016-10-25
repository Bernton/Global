using System;
using System.Collections.Generic;

namespace DecisionDealer.Model
{
    public static class PokerEngine
    {
        public static int[] GetWinnerValueIndexes(IHandValue[] handValues)
        {
            bool isSame;

            if (handValues == null || handValues.Length < 2)
            {
                throw new ArgumentException("HandValues may not be null and have to more than 2 to compare");
            }

            int currentlyLeading = 0;
            List<int> sameAsLead = new List<int>();

            for (int i = 1; i < handValues.Length; i++)
            {
                if (handValues[i] == null)
                {
                    continue;
                }

                if (IsCompetitorStronger(handValues[currentlyLeading], handValues[i], out isSame))
                {
                    currentlyLeading = i;

                    if (isSame)
                    {
                        sameAsLead.Add(i);
                    }
                    else
                    {
                        sameAsLead.Clear();
                    }
                }
            }

            sameAsLead.Add(currentlyLeading);

            return sameAsLead.ToArray();
        }

        public static int[] GetWinnerValueIndexes(Card[][] holeCards, Card[] communityCards)
        {
            bool isSame;
            IHandValue[] handValues = new IHandValue[holeCards.Length];

            Card[] cards = new Card[]
            {
                    null,
                    null,
                    communityCards[0],
                    communityCards[1],
                    communityCards[2],
                    communityCards[3],
                    communityCards[4]
            };


            for (int i = 0; i < holeCards.Length; i++)
            {
                cards[0] = holeCards[i][0];
                cards[1] = holeCards[i][1];

                EntryPoint.StartReporting(0);
                handValues[i] = GetHandValue(cards);
                EntryPoint.Put(0, 0);
            }


            if (handValues == null || handValues.Length < 2)
            {
                throw new ArgumentException("HandValues may not be null and have to more than 2 to compare");
            }

            int currentlyLeading = 0;
            List<int> sameAsLead = new List<int>();

            for (int i = 1; i < handValues.Length; i++)
            {
                if (IsCompetitorStronger(handValues[currentlyLeading], handValues[i], out isSame))
                {
                    currentlyLeading = i;
                    sameAsLead.Clear();
                }

                if (isSame)
                {
                    sameAsLead.Add(i);
                }
            }

            sameAsLead.Add(currentlyLeading);

            return sameAsLead.ToArray();
        }

        public static bool IsCompetitorStronger(IHandValue currentlyBest, IHandValue competitor, out bool isSame)
        {
            isSame = false;

            if (currentlyBest == null)
            {
                return true;
            }

            if (currentlyBest.GetRank() < competitor.GetRank())
            {
                return false;
            }
            else if (currentlyBest.GetRank() > competitor.GetRank())
            {
                return true;
            }
            else
            {
                Type type = currentlyBest.GetType();

                if (type == typeof(StraightFlush))
                {
                    if (((StraightFlush)currentlyBest).Highest > ((StraightFlush)competitor).Highest)
                    {
                        return false;
                    }
                    else if (((StraightFlush)currentlyBest).Highest < ((StraightFlush)competitor).Highest)
                    {
                        return true;
                    }
                    else
                    {
                        isSame = true;
                        return false;
                    }
                }
                else if (type == typeof(FourOfAKind))
                {
                    if (((FourOfAKind)currentlyBest).Value > ((FourOfAKind)competitor).Value)
                    {
                        return false;
                    }
                    else if (((FourOfAKind)currentlyBest).Value < ((FourOfAKind)competitor).Value)
                    {
                        return true;
                    }
                    else
                    {
                        if (((FourOfAKind)currentlyBest).Kicker > ((FourOfAKind)competitor).Kicker)
                        {
                            return false;
                        }
                        else if (((FourOfAKind)currentlyBest).Kicker < ((FourOfAKind)competitor).Kicker)
                        {
                            return true;
                        }
                        else
                        {
                            isSame = true;
                            return false;
                        }
                    }
                }
                else if (type == typeof(FullHouse))
                {
                    if (((FullHouse)currentlyBest).TreeOfAKind > ((FullHouse)competitor).TreeOfAKind)
                    {
                        return false;
                    }
                    else if (((FullHouse)currentlyBest).TreeOfAKind < ((FullHouse)competitor).TreeOfAKind)
                    {
                        return true;
                    }
                    else
                    {
                        if (((FullHouse)currentlyBest).Pair > ((FullHouse)competitor).Pair)
                        {
                            return false;
                        }
                        else if (((FullHouse)currentlyBest).Pair < ((FullHouse)competitor).Pair)
                        {
                            return true;
                        }
                        else
                        {
                            isSame = true;
                            return false;
                        }
                    }
                }
                else if (type == typeof(Flush))
                {
                    for (int i = 0; i < ((Flush)currentlyBest).Values.Length; i++)
                    {
                        if (((Flush)currentlyBest).Values[i] > ((Flush)competitor).Values[i])
                        {
                            return false;
                        }
                        else if (((Flush)currentlyBest).Values[i] < ((Flush)competitor).Values[i])
                        {
                            return true;
                        }
                    }

                    isSame = true;
                    return false;
                }
                else if (type == typeof(Straight))
                {
                    if (((Straight)currentlyBest).Highest > ((Straight)competitor).Highest)
                    {
                        return false;
                    }
                    else if (((Straight)currentlyBest).Highest < ((Straight)competitor).Highest)
                    {
                        return true;
                    }
                    else
                    {
                        isSame = true;
                        return false;
                    }
                }
                else if (type == typeof(TreeOfAKind))
                {
                    if (((TreeOfAKind)currentlyBest).Value > ((TreeOfAKind)competitor).Value)
                    {
                        return false;
                    }
                    else if (((TreeOfAKind)currentlyBest).Value < ((TreeOfAKind)competitor).Value)
                    {
                        return true;
                    }
                    else
                    {
                        if (((TreeOfAKind)currentlyBest).Kickers[0] > ((TreeOfAKind)competitor).Kickers[0])
                        {
                            return false;
                        }
                        else if (((TreeOfAKind)currentlyBest).Kickers[0] < ((TreeOfAKind)competitor).Kickers[0])
                        {
                            return true;
                        }
                        else
                        {
                            if (((TreeOfAKind)currentlyBest).Kickers[1] > ((TreeOfAKind)competitor).Kickers[1])
                            {
                                return false;
                            }
                            else if (((TreeOfAKind)currentlyBest).Kickers[1] < ((TreeOfAKind)competitor).Kickers[1])
                            {
                                return true;
                            }
                            else
                            {
                                isSame = true;
                                return false;
                            }
                        }
                    }
                }
                else if (type == typeof(DoublePair))
                {
                    if (((DoublePair)currentlyBest).HigherPair > ((DoublePair)competitor).HigherPair)
                    {
                        return false;
                    }
                    else if (((DoublePair)currentlyBest).HigherPair < ((DoublePair)competitor).HigherPair)
                    {
                        return true;
                    }
                    else
                    {
                        if (((DoublePair)currentlyBest).LowerPair > ((DoublePair)competitor).LowerPair)
                        {
                            return false;
                        }
                        else if (((DoublePair)currentlyBest).LowerPair < ((DoublePair)competitor).LowerPair)
                        {
                            return true;
                        }
                        else
                        {
                            if ((((DoublePair)currentlyBest).Kicker > ((DoublePair)competitor).Kicker))
                            {
                                return false;
                            }
                            else if ((((DoublePair)currentlyBest).Kicker < ((DoublePair)competitor).Kicker))
                            {
                                return true;
                            }
                            else
                            {
                                isSame = true;
                                return false;
                            }
                        }
                    }
                }
                else if (type == typeof(Pair))
                {
                    if (((Pair)currentlyBest).Value > ((Pair)competitor).Value)
                    {
                        return false;
                    }
                    else if (((Pair)currentlyBest).Value < ((Pair)competitor).Value)
                    {
                        return true;
                    }
                    else
                    {
                        for (int i = 0; i < ((Pair)currentlyBest).Kickers.Length; i++)
                        {
                            if (((Pair)currentlyBest).Kickers[i] > ((Pair)competitor).Kickers[i])
                            {
                                return false;
                            }
                            else if (((Pair)currentlyBest).Kickers[i] < ((Pair)competitor).Kickers[i])
                            {
                                return true;
                            }
                        }

                        isSame = true;
                        return false;
                    }
                }
                else
                {
                    for (int i = 0; i < ((HighCard)currentlyBest).Values.Length; i++)
                    {
                        if (((HighCard)currentlyBest).Values[i] > ((HighCard)competitor).Values[i])
                        {
                            return false;
                        }
                        else if (((HighCard)currentlyBest).Values[i] < ((HighCard)competitor).Values[i])
                        {
                            return true;
                        }
                    }

                    isSame = true;
                    return false;
                }
            }


        }

        public static IHandValue GetHandValue(Card[] cards)
        {
            IHandValue handValue = null;

            int[] values = new int[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                values[i] = (int)cards[i].Value;
            }

            if (IsStraightFlush(cards, ref handValue))
            {
                return handValue;
            }

            if (IsFourOfAKind(values, ref handValue))
            {
                return handValue;
            }

            if (IsFullHouse(values, ref handValue))
            {
                return handValue;
            }

            if (IsFlush(cards, ref handValue))
            {
                return handValue;
            }

            if (IsStraight(values, ref handValue))
            {
                return handValue;
            }

            if (IsTreeOfAKind(values, ref handValue))
            {
                return handValue;
            }

            if (IsDoublePair(values, ref handValue))
            {
                return handValue;
            }

            if (IsPair(values, ref handValue))
            {
                return handValue;
            }

            EntryPoint.StartReporting(9);

            values.QuickSort();
            CardValue[] best5 = new CardValue[]
            {
                (CardValue)values[6],
                (CardValue)values[5],
                (CardValue)values[4],
                (CardValue)values[3],
                (CardValue)values[2]
            };

            EntryPoint.Put(9, 9);
            return new HighCard(best5);
        }

        private static bool IsStraightFlush(Card[] cards, ref IHandValue value)
        {
            EntryPoint.StartReporting(1);

            for (int suitI = 0; suitI < 4; suitI++)
            {
                List<Card> suitedCards = new List<Card>();

                for (int cardI = 0; cardI < cards.Length; cardI++)
                {
                    if (cards[cardI].Suit == (CardSuit)suitI)
                    {
                        suitedCards.Add(cards[cardI]);
                    }
                }

                if (suitedCards.Count >= 5 && IsStraight(suitedCards.ToArray(), ref value))
                {
                    value = new StraightFlush(((Straight)value).Highest);
                    EntryPoint.Put(1, 1);
                    return true;
                }
            }

            EntryPoint.Put(1, 1);
            return false;
        }

        private static bool IsFourOfAKind(int[] values, ref IHandValue value)
        {
            EntryPoint.StartReporting(2);

            for (int valueI = 0; valueI < 13; valueI++)
            {
                int count = 0;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] == valueI)
                    {
                        count++;
                    }
                }

                if (count == 4)
                {
                    CardValue kicker = CardValue.Deuce;

                    for (int cardI = 0; cardI < values.Length; cardI++)
                    {
                        if (values[cardI] != valueI && values[cardI] > (int)kicker)
                        {
                            kicker = (CardValue)values[cardI];
                        }
                    }

                    value = new FourOfAKind((CardValue)valueI, kicker);
                    EntryPoint.Put(2, 2);
                    return true;
                }
            }

            EntryPoint.Put(2, 2);
            return false;
        }

        private static bool IsFullHouse(int[] values, ref IHandValue value)
        {
            EntryPoint.StartReporting(3);

            bool trips = false;
            int tripIndex = 0;

            for (int valueI = 12; valueI >= 0; valueI--)
            {
                int count = 0;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] == valueI)
                    {
                        count++;
                    }
                }

                if (count == 3)
                {
                    trips = true;
                    tripIndex = valueI;
                    break;
                }
            }

            if (!trips)
            {
                EntryPoint.Put(3, 3);
                return false;
            }

            for (int valueI = 12; valueI >= 0; valueI--)
            {
                if (valueI == tripIndex)
                {
                    continue;
                }

                int count = 0;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] == valueI)
                    {
                        count++;
                    }
                }

                if (count >= 2)
                {
                    value = new FullHouse((CardValue)tripIndex, (CardValue)valueI);
                    EntryPoint.Put(3, 3);
                    return true;
                }
            }

            EntryPoint.Put(3, 3);
            return false;
        }

        private static bool IsFlush(Card[] cards, ref IHandValue value)
        {
            EntryPoint.StartReporting(4);

            for (int suitI = 0; suitI < 4; suitI++)
            {
                List<Card> suitedCards = new List<Card>();

                for (int cardI = 0; cardI < cards.Length; cardI++)
                {
                    if (cards[cardI].Suit == (CardSuit)suitI)
                    {
                        suitedCards.Add(cards[cardI]);
                    }
                }

                if (suitedCards.Count >= 5)
                {
                    int[] values = new int[suitedCards.Count];

                    for (int i = 0; i < suitedCards.Count; i++)
                    {
                        values[i] = (int)suitedCards[i].Value;
                    }

                    values.QuickSort();

                    value = new Flush(new CardValue[]
                    {
                        (CardValue)values[values.Length - 1],
                        (CardValue)values[values.Length - 2],
                        (CardValue)values[values.Length - 3],
                        (CardValue)values[values.Length - 4],
                        (CardValue)values[values.Length - 5]
                    });

                    EntryPoint.Put(4, 4);
                    return true;
                }
            }

            EntryPoint.Put(4, 4);
            return false;
        }

        private static bool IsStraight(int[] values, ref IHandValue value)
        {
            EntryPoint.StartReporting(5);

            values.QuickSort();

            int consec = 1;

            if (values[values.Length - 1] == 12 && values[0] == 0)
            {
                consec = 2;
            }

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] == values[i - 1])
                {
                    continue;
                }

                if ((int)(values[i] - 1) == (int)values[i - 1])
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }

                if (consec >= 5)
                {
                    if (i != values.Length - 1 && values[i] + 1 == values[i + 1])
                    {
                        continue;
                    }

                    value = new Straight((CardValue)values[i]);
                    return true;
                }
            }

            EntryPoint.Put(5, 5);
            return false;
        }

        private static bool IsStraight(Card[] cards, ref IHandValue value)
        {
            EntryPoint.StartReporting(5);

            int[] values = new int[cards.Length];

            for (int i = 0; i < cards.Length; i++)
            {
                values[i] = (int)cards[i].Value;
            }

            values.QuickSort();

            int consec = 1;

            if (values[values.Length - 1] == 12 && values[0] == 0)
            {
                consec = 2;
            }

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] == values[i - 1])
                {
                    continue;
                }

                if ((int)(values[i] - 1) == (int)values[i - 1])
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }

                if (consec >= 5)
                {
                    if (i != values.Length - 1 && values[i] + 1 == values[i + 1])
                    {
                        continue;
                    }

                    value = new Straight((CardValue)values[i]);
                    return true;
                }
            }

            EntryPoint.Put(5, 5);
            return false;
        }

        private static bool IsTreeOfAKind(int[] values, ref IHandValue value)
        {
            EntryPoint.StartReporting(6);

            for (int valueI = 0; valueI < 13; valueI++)
            {
                int count = 0;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] == valueI)
                    {
                        count++;
                    }
                }

                if (count == 3)
                {
                    CardValue[] kickers = new CardValue[] { CardValue.Tray, CardValue.Deuce };

                    for (int cardI = 0; cardI < values.Length; cardI++)
                    {
                        if (values[cardI] != valueI && values[cardI] > (int)kickers[0])
                        {
                            kickers[0] = (CardValue)values[cardI];
                        }
                        else if (values[cardI] != valueI && values[cardI] > (int)kickers[1])
                        {
                            kickers[1] = (CardValue)values[cardI];
                        }
                    }

                    value = new TreeOfAKind((CardValue)valueI, kickers);
                    EntryPoint.Put(6, 6);
                    return true;
                }
            }

            EntryPoint.Put(6, 6);
            return false;
        }

        private static bool IsDoublePair(int[] values, ref IHandValue value)
        {
            EntryPoint.StartReporting(7);

            int countOfPairs = 0;
            int higherPair = 0;
            int lowerPair = 0;

            for (int valueI = 0; valueI < 13; valueI++)
            {
                int count = 0;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] == valueI)
                    {
                        count++;
                    }
                }

                if (count == 2)
                {
                    countOfPairs++;
                    lowerPair = higherPair;
                    higherPair = valueI;
                }
            }

            if (countOfPairs >= 2)
            {
                CardValue kicker = CardValue.Deuce;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] != higherPair && values[cardI] != lowerPair && values[cardI] > (int)kicker)
                    {
                        kicker = (CardValue)values[cardI];
                    }
                }

                value = new DoublePair((CardValue)higherPair, (CardValue)lowerPair, kicker);
                EntryPoint.Put(7, 7);
                return true;
            }

            EntryPoint.Put(7, 7);
            return false;
        }

        private static bool IsPair(int[] values, ref IHandValue value)
        {
            EntryPoint.StartReporting(8);

            for (int valueI = 0; valueI < 13; valueI++)
            {
                int count = 0;

                for (int cardI = 0; cardI < values.Length; cardI++)
                {
                    if (values[cardI] == valueI)
                    {
                        count++;
                    }
                }

                if (count == 2)
                {
                    CardValue[] kickers = new CardValue[] { CardValue.Four, CardValue.Tray, CardValue.Deuce };

                    for (int cardI = 0; cardI < values.Length; cardI++)
                    {
                        if (values[cardI] != valueI && values[cardI] > (int)kickers[0])
                        {
                            kickers[0] = (CardValue)values[cardI];
                        }
                        else if (values[cardI] != valueI && values[cardI] > (int)kickers[1])
                        {
                            kickers[1] = (CardValue)values[cardI];
                        }
                        else if (values[cardI] != valueI && values[cardI] > (int)kickers[2])
                        {
                            kickers[2] = (CardValue)values[cardI];
                        }
                    }

                    value = new Pair((CardValue)valueI, kickers);
                    EntryPoint.Put(8, 8);
                    return true;
                }
            }

            EntryPoint.Put(8, 8);
            return false;
        }
    }
}
