﻿namespace DecisionDealer.Model
{
    public class StraightFlush : IHandValue
    {
        public CardValue Highest { get; private set; }

        public StraightFlush(CardValue highest)
        {
            Highest = highest;
        }

        public int Rank
        {
            get
            {
                return 1;
            }
        }

        public override string ToString()
        {
            if(Highest == CardValue.Ace)
            {
                return "Royal Flush";
            }

            return string.Format("{0} High Straight Flush", Highest.ToString());
        }
    }
}
