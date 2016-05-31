using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
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
                return "Broadway (Ace Hight Straight)";
            }
            else if(Highest == CardValue.Five)
            {
                return "Wheel (Five High Straight)";
            }

            return string.Format("{0} High Straight", Highest.ToString());
        }
    }
}
