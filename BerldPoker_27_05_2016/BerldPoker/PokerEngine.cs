using System;
using System.Collections.Generic;
using System.Linq;

namespace BerldPoker
{
    public static class PokerEngine
    {
        public static int Total
        {
            get
            {
                return StraightFlush + FourOfAKind + FullHouse + Flush + Straight + TreeOfAKind + DoublePair + Pair + HighCard;
            }
        }

        public static int RoyalFlush { get; private set; }
        public static int StraightFlush { get; private set; }
        public static int FourOfAKind { get; private set; }
        public static int FullHouse { get; private set; }
        public static int Flush { get; private set; }
        public static int Straight { get; private set; }
        public static int TreeOfAKind { get; private set; }
        public static int DoublePair { get; private set; }
        public static int Pair { get; private set; }
        public static int HighCard { get; private set; }

        public static IHandValue[] GetWinnerValues(IHandValue[] handValues)
        {
            if (handValues == null || handValues.Length < 2)
            {
                throw new ArgumentException("HandValues may not be null and have to more than 2 to compare");
            }

            IHandValue currentlyLeading = handValues[0];
            List<IHandValue> sameAsLead = new List<IHandValue>();

            for (int i = 1; i < handValues.Length; i++)
            {
                if (IsCompetitorStronger(currentlyLeading, handValues[i], sameAsLead))
                {
                    currentlyLeading = handValues[i];
                    sameAsLead.Clear();
                }
            }

            sameAsLead.Add(currentlyLeading);
            return sameAsLead.ToArray();
        }

        public static bool IsCompetitorStronger(IHandValue currentlyBest, IHandValue competitor, List<IHandValue> toAddSame)
        {
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
                        toAddSame.Add(competitor);
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
                            toAddSame.Add(competitor);
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
                            toAddSame.Add(competitor);
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

                    toAddSame.Add(competitor);
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
                        toAddSame.Add(competitor);
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
                                toAddSame.Add(competitor);
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
                                toAddSame.Add(competitor);
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

                        toAddSame.Add(competitor);
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

                    toAddSame.Add(competitor);
                    return false;
                }
            }
        }

        public static IHandValue GetHandValue(Card[] cards)
        {
            IHandValue handValue = null;

            if (IsStraightFlush(cards, ref handValue))
            {
                if(((StraightFlush)handValue).Highest == CardValue.Ace)
                {
                    RoyalFlush++;
                }

                StraightFlush++;
                return handValue;
            }

            if (IsFourOfAKind(cards, ref handValue))
            {
                FourOfAKind++;
                return handValue;
            }

            if (IsFullHouse(cards, ref handValue))
            {
                FullHouse++;
                return handValue;
            }

            if (IsFlush(cards, ref handValue))
            {
                Flush++;
                return handValue;
            }

            if (IsStraight(cards, ref handValue))
            {
                Straight++;
                return handValue;
            }

            if (IsTreeOfAKind(cards, ref handValue))
            {
                TreeOfAKind++;
                return handValue;
            }

            if (IsDoublePair(cards, ref handValue))
            {
                DoublePair++;
                return handValue;
            }

            if (IsPair(cards, ref handValue))
            {
                Pair++;
                return handValue;
            }


            HighCard++;
            Card[] sorted = cards.OrderBy(c => c.Value).ToArray();
            CardValue[] best5 = new CardValue[]
            {
                sorted[6].Value,
                sorted[5].Value,
                sorted[4].Value,
                sorted[3].Value,
                sorted[2].Value
            };

            return new HighCard(best5);
        }

        private static bool IsStraightFlush(Card[] cards, ref IHandValue value)
        {
            for (int i = 0; i < 4; i++)
            {
                Card[] result = cards.Where(c => c.Suit == ((CardSuit)i)).OrderBy(c => c.Value).ToArray();

                if (result.Length >= 5 && IsStraight(result, ref value))
                {
                    value = new StraightFlush(((Straight)value).Highest);
                    return true;
                }
            }

            return false;
        }

        private static bool IsFourOfAKind(Card[] cards, ref IHandValue value)
        {
            for (int i = 0; i < 13; i++)
            {
                if (cards.Where(c => c.Value == ((CardValue)i)).ToArray().Length == 4)
                {
                    CardValue kicker = cards.Where(c => c.Value != (CardValue)i).Max(c => c.Value);

                    value = new FourOfAKind((CardValue)i, kicker);
                    return true;
                }
            }

            return false;
        }

        private static bool IsFullHouse(Card[] cards, ref IHandValue value)
        {
            bool trips = false;
            int tripIndex = 0;

            for (int i = 12; i >= 0; i--)
            {
                if (cards.Where(c => c.Value == ((CardValue)i)).ToArray().Length == 3)
                {
                    trips = true;
                    tripIndex = i;
                    break;
                }
            }

            if (!trips)
            {
                return false;
            }

            for (int i = 12; i >= 0; i--)
            {
                if (i == tripIndex)
                {
                    continue;
                }

                if (cards.Where(c => c.Value == ((CardValue)i)).ToArray().Length >= 2)
                {
                    value = new FullHouse((CardValue)tripIndex, (CardValue)i);
                    return true;
                }
            }

            return false;
        }

        private static bool IsFlush(Card[] cards, ref IHandValue value)
        {
            for (int i = 0; i < 4; i++)
            {
                Card[] result = cards.Where(c => c.Suit == ((CardSuit)i)).OrderBy(c => Math.Abs((int)c.Value - 12)).ToArray();

                if (result.Length >= 5)
                {
                    value = new Flush(new CardValue[]
                    {
                        result[0].Value,
                        result[1].Value,
                        result[2].Value,
                        result[3].Value,
                        result[4].Value
                    });

                    return true;
                }
            }

            return false;
        }

        private static bool IsStraight(Card[] cards, ref IHandValue value)
        {
            List<Card> listCards = cards.ToList();
            listCards = listCards.OrderBy(c => c.Value).ToList();

            int consec = 1;

            if (listCards[listCards.Count - 1].Value == CardValue.Ace && listCards[0].Value == CardValue.Deuce)
            {
                consec = 2;
            }

            for (int i = 1; i < listCards.Count; i++)
            {
                if (listCards[i].Value == listCards[i - 1].Value)
                {
                    continue;
                }

                if ((int)(listCards[i].Value - 1) == (int)listCards[i - 1].Value)
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }

                if (consec >= 5)
                {
                    if (i != listCards.Count - 1 && listCards[i].Value + 1 == listCards[i + 1].Value)
                    {
                        continue;
                    }

                    value = new Straight(listCards[i].Value);
                    return true;
                }
            }

            return false;
        }

        private static bool IsTreeOfAKind(Card[] cards, ref IHandValue value)
        {
            for (int i = 0; i < 13; i++)
            {
                if (cards.Where(c => c.Value == ((CardValue)i)).ToArray().Length == 3)
                {
                    Card[] result = cards.Where(c => c.Value != ((CardValue)i)).OrderBy(c => Math.Abs((int)c.Value - 12)).ToArray();

                    value = new TreeOfAKind((CardValue)i, new CardValue[]
                    {
                        result[0].Value,
                        result[1].Value
                    });

                    return true;
                }
            }

            return false;
        }

        private static bool IsDoublePair(Card[] cards, ref IHandValue value)
        {
            int countOfPairs = 0;
            int higherPair = 0;
            int lowerPair = 0;

            for (int i = 0; i < 13; i++)
            {
                if (cards.Where(c => c.Value == ((CardValue)i)).ToArray().Length == 2)
                {
                    countOfPairs++;
                    lowerPair = higherPair;
                    higherPair = i;
                }
            }

            if (countOfPairs >= 2)
            {
                CardValue kicker = cards.Where(c => (int)c.Value != higherPair && (int)c.Value != lowerPair).Max(c => c.Value);

                value = new DoublePair((CardValue)higherPair, (CardValue)lowerPair, kicker);
                return true;
            }

            return false;
        }

        private static bool IsPair(Card[] cards, ref IHandValue value)
        {
            for (int i = 0; i < 13; i++)
            {
                Card[] result = cards.Where(c => c.Value == ((CardValue)i)).ToArray();

                if (result.Length == 2)
                {
                    Card[] sorted = cards.Where(c => c.Value != ((CardValue)i)).OrderBy(c => Math.Abs((int)c.Value - 12)).ToArray();

                    value = new Pair((CardValue)i, new CardValue[]
                    {
                        sorted[0].Value,
                        sorted[1].Value,
                        sorted[2].Value
                    });

                    return true;
                }
            }

            return false;
        }
    }
}
