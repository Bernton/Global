using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionDealer.Model
{
    public class PokerTable
    {
        #region Fields

        private int _freeSeats = 10;
        private Random _rnd = new Random();
        private Deck _deck = new Deck();

        #endregion

        #region Properties

        public int ShowFrequency { get; set; }
        public PokerPlayer[] Players { get; private set; }
        public List<Card> CommunityCards { get; private set; }

        #endregion

        #region Constructors

        public PokerTable()
        {
            ShowFrequency = 50;
            ResetTable();
        }

        #endregion

        #region Methods

        public void SeatPlayer(PokerPlayer player)
        {
            if (_freeSeats == 0)
            {
                throw new InvalidOperationException("Table is already full.");
            }

            int seatIndex = _rnd.Next(_freeSeats) + 1;

            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] == null)
                {
                    seatIndex--;
                }

                if (seatIndex <= 0)
                {
                    SeatPlayer(player, i);
                    return;
                }
            }
        }

        public void SetCommunityCards(int amount)
        {
            CommunityCards.Clear();

            for (int i = 0; i < amount; i++)
            {
                CommunityCards.Add(_deck.Cards[Players.Count(c => c != null) * 2 + i]);
            }
        }

        public void DealHoleCards()
        {
            _deck.Shuffle();

            int count = 0;

            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] != null)
                {

                    if (count == 0)
                    {
                        Players[i].HoleCards[0] = _deck.Cards[count * 2];
                        Players[i].HoleCards[1] = _deck.Cards[count * 2 + 1];
                    }
                    else
                    {
                        if (_rnd.Next(100) < ShowFrequency - 1)
                        {
                            Players[i].HoleCards[0] = _deck.Cards[count * 2];
                        }
                        else
                        {
                            Players[i].HoleCards[0] = null;
                        }

                        if (_rnd.Next(100) < ShowFrequency - 1)
                        {
                            Players[i].HoleCards[1] = _deck.Cards[count * 2 + 1];
                        }
                        else
                        {
                            Players[i].HoleCards[1] = null;
                        }

                    }

                    count++;
                }
            }
        }

        public void SeatPlayer(PokerPlayer player, int seatIndex)
        {
            if (Players[seatIndex] == null)
            {
                Players[seatIndex] = player;
                _freeSeats -= 1;
            }
            else
            {
                throw new ArgumentException(string.Format("Seat with number {0} was already taken.", seatIndex));
            }
        }

        public void ResetTable()
        {
            CommunityCards = new List<Card>();
            Players = new PokerPlayer[10];
            _deck = new Deck();

            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = null;
            }

            _freeSeats = 10;
        }

        public int[] Playout()
        {
            SetCommunityCards(5);

            List<int> indexes = new List<int>();
            IHandValue[] handValues = new IHandValue[10];

            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] != null)
                {
                    Card[] cards = new Card[7];

                    for (int i2 = 0; i2 < 5; i2++)
                    {
                        cards[i2] = CommunityCards[i2];
                    }

                    cards[5] = Players[i].HoleCards[0];
                    cards[6] = Players[i].HoleCards[1];

                    handValues[i] = PokerEngine.GetHandValue(cards);
                    indexes.Add(i);
                }
            }

            return PokerEngine.GetWinnerValueIndexes(handValues.ToArray());
        }

        public bool IsTableFull()
        {
            if (_freeSeats == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsSeatFree(int seatNumber)
        {
            if (Players[seatNumber] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
