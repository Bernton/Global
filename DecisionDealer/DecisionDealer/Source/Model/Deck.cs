﻿using System;
using System.Security.Cryptography;

namespace DecisionDealer.Model
{
    public class Deck
    {
        #region Fields

        public const int CardCount = 52;
        private readonly RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();

        #endregion

        #region Properties

        public Card[] Cards { get; private set; }

        #endregion

        #region Constructors

        public Deck()
        {
            Cards = GetSortedDeck();
        }

        #endregion

        #region Methods

        public void Shuffle()
        {
            Card[] shuffledCards = new Card[CardCount];

            for (int i = CardCount - 1; i >= 0; i--)
            {
                int result = GetRandomNumber(i);
                int count = -1;

                for (int cardIndex = 0; cardIndex < CardCount; cardIndex++)
                {
                    if (shuffledCards[cardIndex] == null)
                    {
                        count++;
                    }

                    if (count == result)
                    {
                        int index = 0;

                        for (int cardI = 0; cardI < Cards.Length; cardI++)
                        {
                            if(Cards[cardI] != null)
                            {
                                index = cardI;
                            }
                        }

                        shuffledCards[cardIndex] = Cards[index];
                        Cards[index] = null;
                        break;
                    }
                }
            }

            Cards = shuffledCards;
        }

        private int GetRandomNumber(int maxValue)
        {
            byte[] randomNumber = new byte[1];

            _rngProvider.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            int range = maxValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)randomValueInRange;
        }

        private Card[] GetSortedDeck()
        {
            Card[] sortedDeck = new Card[CardCount];

            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < 13; valueIndex++)
                {
                    sortedDeck[suitIndex * 13 + valueIndex] = new Card((CardValue)valueIndex, (CardSuit)suitIndex);
                }
            }

            return sortedDeck;
        }

        #endregion
    }
}
