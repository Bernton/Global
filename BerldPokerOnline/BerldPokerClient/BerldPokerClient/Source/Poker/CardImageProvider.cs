using BerldPokerClient.Properties;
using System.Drawing;

namespace BerldPokerClient.Poker
{
    public static class CardImageProvider
    {
        private static Bitmap[] _cardImages;

        static CardImageProvider()
        {
            _cardImages = new Bitmap[53];

            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < 13; valueIndex++)
                {
                    _cardImages[suitIndex * 13 + valueIndex] = CropImage(Resources.cardsprite, new Rectangle(valueIndex * 81, (int)(suitIndex * 117.5), 81, 118));
                }
            }

            _cardImages[52] = CropImage(Resources.cardsprite, new Rectangle(0, 469, 81, 118));
        }

        public static Bitmap GetImageFromCard(Card card)
        {
            return _cardImages[(int)card.Suit * 13 + ((int)card.Value)];
        }

        public static Bitmap GetCardBack()
        {
            return _cardImages[52];
        }

        private static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            Bitmap bitmap = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bitmap;
        }
    }
}
