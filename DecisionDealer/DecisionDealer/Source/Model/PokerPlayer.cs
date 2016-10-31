namespace DecisionDealer.Model
{
    public class PokerPlayer
    {
        #region Properties

        public int SeatNumber { get; set; }
        public Card[] HoleCards { get; set; }

        #endregion

        #region Constructors

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

        #endregion
    }
}
