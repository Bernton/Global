using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DecisionDealer.Model
{
    public class PokerSimulationEngine
    {
        private Random _rnd = new Random();
        public int Iterations { get; set; }


        public PokerSimulationEngine()
        {
            Iterations = 600000;
        }

        public PokerSimulationEngine(int iterations)
        {
            Iterations = iterations;
        }


        public HandStatistic[] Simulate(Card[][] holeCards)
        {
            HandStatistic[] results = new HandStatistic[holeCards.Length];
            Card[] deck = GenerateDeck(holeCards);

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new HandStatistic();
                results[i].SampleSize = Iterations;
            }

            List<int[]> rndIndex = new List<int[]>();

            for (int i = 0; i < holeCards.Length; i++)
            {
                for (int i2 = 0; i2 < holeCards[i].Length; i2++)
                {
                    if (holeCards[i][i2] == null)
                    {
                        rndIndex.Add(new int[] { i, i2 });
                    }
                }
            }

            Card[] rndCards;


            for (int i = 0; i < Iterations; i++)
            {
                rndCards = GetRandom(deck, 5 + rndIndex.Count);

                for (int i2 = 0; i2 < rndIndex.Count; i2++)
                {
                    holeCards[rndIndex[i2][0]][rndIndex[i2][1]] = rndCards[Math.Abs(i2 - 5 - rndIndex.Count + 1)];
                }


                int[] winners = PokerEngine.GetWinnerValueIndexes(holeCards, new Card[] { rndCards[0], rndCards[1], rndCards[2], rndCards[3], rndCards[4] });

                if (winners.Length == 1)
                {
                    results[winners[0]].Wins++;
                }
                else
                {
                    for (int tieI = 0; tieI < winners.Length; tieI++)
                    {
                        results[winners[tieI]].Ties++;

                        if (results[winners[tieI]].TieSplit == 1)
                        {
                            results[winners[tieI]].TieSplit = winners.Length;
                        }
                        else
                        {
                            results[winners[tieI]].TieSplit += (winners.Length- results[winners[tieI]].TieSplit) / results[winners[tieI]].Ties;
                        }
                    }
                }

            }

            return results;
        }

        //public double[] Simulate(Card[][] holeCards, Card[] communityCards)
        //{

        //}

        private Card[] GetRandom(Card[] deck, int amount)
        {
            List<Card> cards = deck.ToList();
            Card[] result = new Card[amount];

            for (int i = 0; i < result.Length; i++)
            {
                int randomNumber = _rnd.Next(cards.Count);
                result[i] = cards[randomNumber];
                cards.RemoveAt(randomNumber);
            }

            return result;
        }

        private Card[] GenerateDeck(Card[][] deadCards)
        {
            List<Card> cards = new List<Card>();

            for (int valueI = 0; valueI < 13; valueI++)
            {
                for (int suitI = 0; suitI < 4; suitI++)
                {
                    cards.Add(new Card((CardValue)valueI, (CardSuit)suitI));
                }
            }

            for (int i = 0; i < deadCards.Length; i++)
            {
                for (int i2 = 0; i2 < deadCards[i].Length; i2++)
                {
                    if (deadCards[i][i2] != null)
                    {
                        cards.Remove(cards.First(c => (int)c.Suit * 13 + (int)c.Value == (int)deadCards[i][i2].Suit * 13 + (int)deadCards[i][i2].Value));
                    }
                }
            }

            return cards.ToArray();
        }
    }
}
