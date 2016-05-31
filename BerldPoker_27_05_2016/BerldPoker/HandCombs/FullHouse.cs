using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
{
    public class FullHouse : IHandValue
    {
        public CardValue TreeOfAKind { get; private set; }
        public CardValue Pair { get; private set; }

        public FullHouse(CardValue set, CardValue pair)
        {
            TreeOfAKind = set;
            Pair = pair;
        }

        public int GetRank()
        {
            return 3;
        }

        public override string ToString()
        {
            string pluralSet;
            string pluralPair;

            if (TreeOfAKind == (CardValue)4)
            {
                pluralSet = "es";
            }
            else
            {
                pluralSet = "s";
            }

            if (Pair == (CardValue)4)
            {
                pluralPair = "es";
            }
            else
            {
                pluralPair = "s";
            }

            return string.Format("Full House, {0}{1} Full Of {2}{3}", TreeOfAKind.ToString(), pluralSet, Pair.ToString(), pluralPair);
        }
    }
}
