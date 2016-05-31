using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
{
    public class DoublePair : IHandValue
    {
        public CardValue HigherPair { get; private set; }
        public CardValue LowerPair { get; private set; }
        public CardValue Kicker { get; private set; }

        public DoublePair(CardValue pair1, CardValue pair2, CardValue kicker)
        {
            if ((int)pair1 > (int)pair2)
            {
                HigherPair = pair1;
                LowerPair = pair2;
            }
            else
            {
                HigherPair = pair2;
                LowerPair = pair1;
            }

            Kicker = kicker;
        }

        public int GetRank()
        {
            return 7;
        }

        public override string ToString()
        {
            string pluralHigher;
            string pluralLower;

            if (HigherPair == (CardValue)4)
            {
                pluralHigher = "es";
            }
            else
            {
                pluralHigher = "s";
            }

            if (LowerPair == (CardValue)4)
            {
                pluralLower = "es";
            }
            else
            {
                pluralLower = "s";
            }

            return string.Format("Double Pair, {0}{1} Up With {2}{3}", HigherPair.ToString(), pluralHigher, LowerPair.ToString(), pluralLower);
        }
    }
}
