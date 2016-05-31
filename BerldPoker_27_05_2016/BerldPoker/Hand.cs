namespace BerldPoker
{
    public class Hand
    {
        public int Count { get; set; }
        public int Won { get; set; }
        public bool IsSuited { get; }

        public double RatioPercent
        {
            get
            {
                return (double)Won / (double)Count * 100.0;
            }
        }

        public CardValue CardValue1 { get; }
        public CardValue CardValue2 { get; }

        public Hand(int count, bool isSuited, CardValue cardValue1, CardValue cardValue2)
        {
            Count = count;
            IsSuited = isSuited;
            CardValue1 = cardValue1;
            CardValue2 = cardValue2;
        }

        public override string ToString()
        {
            if ((int)CardValue1 == (int)CardValue2)
            {
                string plural;

                if (CardValue1 == (CardValue)4)
                {
                    plural = "es";
                }
                else
                {
                    plural = "s";
                }

                return string.Format("Pocket {0}{1}", CardValue1, plural);
            }

            string afterText;

            if(IsSuited)
            {
                afterText = "Suited";
            }
            else
            {
                afterText = "Offsuit";
            }

            return string.Format("{0} {1} {2}", CardValue1, CardValue2, afterText);
        }
    }
}
