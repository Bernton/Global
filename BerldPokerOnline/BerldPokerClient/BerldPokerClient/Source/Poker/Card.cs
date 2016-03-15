namespace BerldPokerClient.Poker
{
    public class Card
    {
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set ;}

        public Card()
        {

        }

        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Value.ToString()} of {Suit.ToString()}s";
        }
    }
}
