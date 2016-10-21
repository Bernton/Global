namespace DecisionDealer.Model
{
    public class PokerPlayer
    {
        public Card[] HoleCards { get; set; }
        public bool RevealedCards { get; set; }

        public PokerPlayer()
        {
            HoleCards = new Card[2];
        }

        public PokerPlayer(Card firstCard, Card secondCard)
        {
            HoleCards = new Card[]
            {
                firstCard,
                secondCard
            };
        }
    }
}
