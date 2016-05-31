namespace BerldPoker
{
    public class StraightFlush : IHandValue
    {
        public CardValue Highest { get; private set; }

        public StraightFlush(CardValue highest)
        {
            Highest = highest;
        }

        public int GetRank()
        {
            return 1;
        }

        public override string ToString()
        {
            if(Highest == CardValue.Ace)
            {
                return "Royal Flush";
            }

            return string.Format("{0} High Straight Flush", Highest.ToString());
        }
    }
}
