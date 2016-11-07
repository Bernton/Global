using System;
using System.Drawing;

namespace SVGCardRender
{
    public class CardImageProvider
    {
        int cardWidth;
        int cardHeight;

        public Bitmap SpriteSheet { get; set; }


        public CardImageProvider(Bitmap spriteSheet)
        {
            SpriteSheet = spriteSheet;
            cardWidth = Round(SpriteSheet.Width / 14.0);
            cardHeight = Round(SpriteSheet.Height / 4.0);
        }


        public Bitmap GetCard(int valueIndex, int suitIndex)
        {
            Rectangle cropArea = new Rectangle(valueIndex * cardWidth, suitIndex * cardHeight, cardWidth, cardHeight);
            return Crop(SpriteSheet, cropArea);
        }

        private Bitmap Crop(Bitmap bitmap, Rectangle cropArea)
        {
            return bitmap.Clone(cropArea, bitmap.PixelFormat);
        }

        private int Round(double number)
        {
            return (int)Math.Round(number, 0);
        }
    }
}
