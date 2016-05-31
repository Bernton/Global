using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
{
    public class TreeOfAKind : IHandValue
    {
        public CardValue Value { get; private set; }
        public CardValue[] Kickers { get; private set; }

        public TreeOfAKind(CardValue value, CardValue[] kickers)
        {
            if (kickers == null || kickers.Length != 2)
            {
                throw new ArgumentException("Values may not be null and must be length 2.");
            }

            Value = value;
            Kickers = kickers;
        }

        public int GetRank()
        {
            return 6;
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

            return string.Format("Tree Of A Kind With {0}{1}", Value.ToString(), plural);
        }
    }
}
