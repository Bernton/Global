using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionDealer.Model
{
    public class PokerSimulationEngine
    {
        #region Fields

        private Random _random = new Random();

        #endregion

        #region Properties & events

        public int Iterations { get; set; }

        public event PercentStepComplete PercentComplete;

        #endregion

        #region Constructors

        public PokerSimulationEngine(int iterations)
        {
            Iterations = iterations;
        }

        #endregion

        #region Methods

        public HandStatistic[] Simulate(Card[,] holeCards)
        {
            List<int> randomHandIndexes = new List<int>();

            for (int i = 0; i < holeCards.GetLength(0); i++)
            {
                if (holeCards[i, 0] == null && holeCards[i, 1] == null)
                {
                    randomHandIndexes.Add(i);
                }
            }

            HandStatistic[] results = new HandStatistic[holeCards.GetLength(0)];
            Card[] deck = GenerateDeck(holeCards);

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new HandStatistic();
                results[i].SampleSize = Iterations;
            }

            Card[] randomCards;
            List<int[]> randomCardIndexes = new List<int[]>();

            for (int playerI = 0; playerI < holeCards.GetLength(0); playerI++)
            {
                for (int cardI = 0; cardI < holeCards.GetLength(1); cardI++)
                {
                    if (holeCards[playerI, cardI] == null)
                    {
                        randomCardIndexes.Add(new int[] { playerI, cardI });
                    }
                }
            }

            for (int i = 0; i < Iterations; i++)
            {
                randomCards = GetRandomCards(deck, randomCardIndexes.Count + 5);

                for (int playerI = 0; playerI < randomCardIndexes.Count; playerI++)
                {
                    holeCards[randomCardIndexes[playerI][0], randomCardIndexes[playerI][1]] = randomCards[Math.Abs(playerI - randomCardIndexes.Count) + 4];
                }

                int[] winners = PokerEngine.GetWinnerValueIndexes(holeCards, randomCards.GetSubArray(0, 5));

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
                            results[winners[tieI]].TieSplit += (winners.Length - results[winners[tieI]].TieSplit) / results[winners[tieI]].Ties;
                        }
                    }
                }

                if (i != 0)
                {
                    if (i % (int)(Iterations / 100.0) == 0)
                    {
                        PercentComplete?.Invoke((int)(i / (double)Iterations * 100.0), i);
                    }
                }
            }

            if (randomHandIndexes.Count > 1)
            {
                float totalWins = 0;
                float totalTies = 0;
                double totalTieSplit = 0;

                for (int i = 0; i < randomHandIndexes.Count; i++)
                {
                    totalWins += results[randomHandIndexes[i]].Wins;
                    totalTies += results[randomHandIndexes[i]].Ties;
                    totalTieSplit += results[randomHandIndexes[i]].TieSplit;
                }

                double averageWins = totalWins / (double)randomHandIndexes.Count;
                double averageTies = totalTies / (double)randomHandIndexes.Count;
                double averageTieSplit = totalTieSplit / randomHandIndexes.Count;

                for (int i = 0; i < randomHandIndexes.Count; i++)
                {
                    results[randomHandIndexes[i]].Wins = (int)averageWins;
                    results[randomHandIndexes[i]].Ties = (int)averageTies;
                    results[randomHandIndexes[i]].TieSplit = (int)averageTieSplit;
                }
            }

            return results;
        }

        private Card[] GenerateDeck(Card[,] deadCards)
        {
            List<Card> cards = new List<Card>();

            for (int valueI = 0; valueI < 13; valueI++)
            {
                for (int suitI = 0; suitI < 4; suitI++)
                {
                    cards.Add(new Card((CardValue)valueI, (CardSuit)suitI));
                }
            }

            for (int playerI = 0; playerI < deadCards.GetLength(0); playerI++)
            {
                for (int cardI = 0; cardI < deadCards.GetLength(1); cardI++)
                {
                    if (deadCards[playerI, cardI] != null)
                    {
                        cards.Remove(cards.First(c => (int)c.Suit * 13 + (int)c.Value == (int)deadCards[playerI, cardI].Suit * 13 + (int)deadCards[playerI, cardI].Value));
                    }
                }
            }

            return cards.ToArray();
        }

        private Card[] GetRandomCards(Card[] deck, int amount)
        {
            List<Card> cards = deck.ToList();
            Card[] result = new Card[amount];

            for (int i = 0; i < result.Length; i++)
            {
                int randomCardIndex = _random.Next(cards.Count);
                result[i] = cards[randomCardIndex];
                cards.RemoveAt(randomCardIndex);
            }

            return result;
        }

        #endregion
    }
}
