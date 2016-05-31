using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
{
    public class FourOfAKind : IHandValue
    {
        public CardValue Value { get; private set; }
        public CardValue Kicker { get; private set; }

        public FourOfAKind(CardValue value, CardValue kicker)
        {
            Value = value;
            Kicker = kicker;
        }

        public int GetRank()
        {
            return 2;
        }

        public override string ToString()
        {
            string plural;

            if (Value == (CardValue)4)
            {
                plural = "es";
            }
            else
            {
                plural = "s";
            }

            return string.Format("Four Of A Kind With {0}{1}", Value.ToString(), plural);
        }
    }
}
