using System;

namespace DecisionDealer.Model
{
    public class Card : ICloneable, IComparable
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

        public object Clone()
        {
            return new Card(Value, Suit);
        }

        public int CompareTo(object obj)
        {
            Card card = (Card)obj;

            if(card.Value > Value)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
