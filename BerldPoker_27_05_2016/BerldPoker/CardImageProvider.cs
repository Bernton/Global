using BerldPoker.Properties;
using System.Drawing;

namespace BerldPoker
{
    public static class CardImageProvider
    {
        private static Bitmap[] _cardImages;

        static CardImageProvider()
        {
            _cardImages = new Bitmap[52];

            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < 13; valueIndex++)
                {
                    _cardImages[suitIndex * 13 + valueIndex] = CropImage(Resources.cardsprite, new Rectangle(valueIndex * 79, (int)(suitIndex * 123), 79, 123));
                }
            }
        }

        public static Bitmap GetImage(Card card)
        {
            return _cardImages[(int)card.Suit * 13 + ((int)card.Value)];
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
