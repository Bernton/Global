using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
{
    public class Pair : IHandValue
    {
        public CardValue Value { get; private set; }
        public CardValue[] Kickers { get; private set; }

        public Pair(CardValue value, CardValue[] kickers)
        {
            if (kickers == null || kickers.Length != 3)
            {
                throw new ArgumentException("Values may not be null and must be length 3.");
            }

            Value = value;
            Kickers = kickers;
        }

        public int GetRank()
        {
            return 8;
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

           return string.Format("Pair Of {0}{1}", Value.ToString(), plural);
        }
    }
}
