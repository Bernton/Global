using System.Collections.Generic;

namespace BerldPoker
{
    public class HandRanking
    {
        public List<Hand> Hands { get; set; } = new List<Hand>();

        public HandRanking()
        {
            for (int i = 12; i >= 0; i--)
            {
                for (int i2 = i; i2 >= 0; i2--)
                {
                    if (i == i2)
                    {
                        Hands.Add(new Hand(0, false, (CardValue)i, (CardValue)i2));
                    }
                    else
                    {
                        Hands.Add(new Hand(0, true, (CardValue)i, (CardValue)i2));
                        Hands.Add(new Hand(0, false, (CardValue)i, (CardValue)i2));
                    }
                }
            }
        }
    }
}
