using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerldPoker
{
    public class HighCard : IHandValue
    {
        public CardValue[] Values { get; private set; }

        public HighCard(CardValue[] values)
        {
            if (values == null || values.Length != 5)
            {
                throw new ArgumentException("Values may not be null and must be length 5.");
            }

            Values = values;
        }

        public int GetRank()
        {
            return 9;
        }

        public override string ToString()
        {
            return string.Format("High Card {0}", Values[0].ToString());
        }
    }
}
