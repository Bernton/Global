using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPokerServer.Poker
{
    public class Straight : IHandValue
    {
        public CardValue Highest { get; private set; }

        public Straight(CardValue highest)
        {
            Highest = highest;
        }

        public int GetRank()
        {
            return 5;
        }

        public override string ToString()
        {
            if(Highest == CardValue.Ace)
            {
                return "Broadway";
            }

            return string.Format("{0} High Straight", Highest.ToString());
        }
    }
}
