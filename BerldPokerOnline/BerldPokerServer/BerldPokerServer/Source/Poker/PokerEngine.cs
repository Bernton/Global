using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BerldPokerServer.Poker
{
    public static class PokerEngine
    {
        public static List<List<IHandValue>> GetRanking(IHandValue[] handValues)
        {
            List<List<IHandValue>> ranking = new List<List<IHandValue>>();

            List<IHandValue> first = new List<IHandValue>();
            first.Add(handValues[0]);
            ranking.Add(first);

            for (int valueIndex = 1; valueIndex < handValues.Length; valueIndex++)
            {
                bool isLast = true;

                for (int i = 0; i < ranking.Count; i++)
                {
                    if (IsCompetitorStronger(ranking[i][0], handValues[valueIndex], ranking[i]))
                    {
                        List<IHandValue> better = new List<IHandValue>();
                        better.Add(handValues[valueIndex]);
                        ranking.Insert(i, better);
                        isLast = false;
                        break;
                    }
                }

                if(isLast)
                {
                    List<IHandValue> last = new List<IHandValue>();
                    last.Add(handValues[valueIndex]);
                    ranking.Add(last);
                }
            }

            return ranking;
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
                return handValue;
            }

            if (IsFourOfAKind(cards, ref handValue))
            {
                return handValue;
            }

            if (IsFullHouse(cards, ref handValue))
            {
                return handValue;
            }

            if (IsFlush(cards, ref handValue))
            {
                return handValue;
            }

            if (IsStraight(cards, ref handValue))
            {
                return handValue;
            }

            if (IsTreeOfAKind(cards, ref handValue))
            {
                return handValue;
            }

            if (IsDoublePair(cards, ref handValue))
            {
                return handValue;
            }

            if (IsPair(cards, ref handValue))
            {
                return handValue;
            }

            Card[] sorted = cards.OrderBy(c => c.Value).ToArray();
            CardValue[] highestFive = new CardValue[]
            {
                sorted[6].Value,
                sorted[5].Value,
                sorted[4].Value,
                sorted[3].Value,
                sorted[2].Value
            };

            return new HighCard(highestFive);
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
