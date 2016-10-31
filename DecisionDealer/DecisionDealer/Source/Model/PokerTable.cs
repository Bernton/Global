using System;
using System.Collections.Generic;

namespace DecisionDealer.Model
{
    public class PokerTable
    {
        #region Fields

        private Deck _deck = new Deck();
        private Random _random = new Random();
        private List<int> _freeSeats = new List<int>();

        #endregion

        #region Properties

        public int ShowFrequency { get; set; }
        public List<PokerPlayer> Players { get; private set; }
        public List<Card> CommunityCards { get; private set; }

        #endregion

        #region Constructors

        public PokerTable()
        {
            Players = new List<PokerPlayer>();
            CommunityCards = new List<Card>();

            ShowFrequency = 50;
            ResetTable();
        }

        #endregion

        #region Methods

        public void SeatPlayer(PokerPlayer player)
        {
            if (Players.Count >= 9)
            {
                throw new InvalidOperationException("Table is already full.");
            }

            SeatPlayer(player, _freeSeats[_random.Next(_freeSeats.Count)]);
            return;
        }

        public void SeatPlayer(PokerPlayer player, int seatNumber)
        {
            if (!_freeSeats.Contains(seatNumber))
            {
                throw new InvalidOperationException("Seat is already taken.");
            }

            player.SeatNumber = seatNumber;
            Players.Add(player);
            _freeSeats.Remove(seatNumber);
        }

        public void DealHoleCards()
        {
            _deck.Shuffle();

            for (int i = 0; i < Players.Count; i++)
            {
                if (i == 0)
                {
                    Players[i].HoleCards[0] = _deck.Cards[i * 2];
                    Players[i].HoleCards[1] = _deck.Cards[i * 2 + 1];
                }
                else
                {
                    if (_random.Next(101) < ShowFrequency)
                    {
                        Players[i].HoleCards[0] = _deck.Cards[i * 2];
                    }
                    else
                    {
                        Players[i].HoleCards[0] = null;
                    }

                    if (_random.Next(101) < ShowFrequency)
                    {
                        Players[i].HoleCards[1] = _deck.Cards[i * 2 + 1];
                    }
                    else
                    {
                        Players[i].HoleCards[1] = null;
                    }
                }
            }
        }

        public void ResetTable()
        {
            _freeSeats.Clear();
            CommunityCards.Clear();
            Players.Clear();

            for (int i = 1; i < 10; i++)
            {
                _freeSeats.Add(i);
            }
        }

        public void RevealCommunityCards(int amount)
        {
            CommunityCards.Clear();

            for (int i = 0; i < amount; i++)
            {
                CommunityCards.Add(_deck.Cards[Players.Count * 2 + i]);
            }
        }

        #endregion
    }
}
