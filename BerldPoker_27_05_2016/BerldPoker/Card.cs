namespace BerldPoker
{
    public class Card
    {
        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public CardValue Value { get; private set; }
        public CardSuit Suit { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} of {1}s", Value.ToString(), Suit.ToString());
        }
    }
}
