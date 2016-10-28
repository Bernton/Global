using DecisionDealer.Properties;
using System.Drawing;

namespace DecisionDealer.Model
{
    public static class CardImageProvider
    {
        #region Fields

        private static Bitmap[] _cardImages;
        private static Bitmap _cardBack;

        private static int _cardWidth = 125;
        private static int _cardHeight = 181;

        #endregion

        #region Constructors

        static CardImageProvider()
        {
            _cardImages = new Bitmap[52];

            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < 13; valueIndex++)
                {
                    _cardImages[suitIndex * 13 + valueIndex] = CropImage(Resources.CardSprite, new Rectangle(valueIndex * _cardWidth, suitIndex * _cardHeight, _cardWidth, _cardHeight));
                }
            }

            _cardBack = CropImage(Resources.CardSprite, new Rectangle(0, 4 * _cardHeight, _cardWidth, _cardHeight));
        }

        #endregion

        #region Methods

        public static Bitmap GetCard(Card card)
        {
            if(card == null)
            {
                return _cardBack;
            }

            return _cardImages[(int)card.Suit * 13 + ((int)card.Value)];
        }

        public static Bitmap GetCardBack()
        {
            return _cardBack;
        }

        private static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bitmap = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bitmap;
        }

        #endregion
    }
}
