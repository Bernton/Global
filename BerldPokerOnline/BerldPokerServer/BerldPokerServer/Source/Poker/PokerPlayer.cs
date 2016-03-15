using System;

namespace BerldPokerServer.Poker
{
    [Serializable]
    public class PokerPlayer
    {
        [NonSerialized]
        private bool _hasCashed = false;

        public bool HasCashed
        {
            get
            {
                return _hasCashed;
            }
            set
            {
                _hasCashed = value;
            }
        }

        public string Name { get; set; }

        public int Chips { get; set; }
        public int ChipsInPot { get; set; }

        public Card Card1 { get; set; }
        public Card Card2 { get; set; }

        public string ValueText { get; set; }
        public bool IsWinner { get; set; } = false;
        public bool IsFolded { get; set; } = false;

        public int TotalChips
        {
            get
            {
                return Chips + ChipsInPot;
            }
        }

        public PokerPlayer() { }

        public PokerPlayer(string name)
        {
            Name = name;
        }
    }
}
