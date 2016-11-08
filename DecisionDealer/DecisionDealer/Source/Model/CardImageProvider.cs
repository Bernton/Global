using DecisionDealer.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DecisionDealer.Model
{
    public static class CardImageProvider
    {
        #region Fields

        private static Bitmap _spriteSheet = Resources.CardSprite_SpecialColor_HighRes;

        private static Bitmap[] _cardImages;
        private static Bitmap _cardBack;

        public static int CardWidth { get; private set; }
        public static int CardHeight { get; private set; }

        public static int ResizeCardWidth { get; set; }
        public static int ResizeCardHeight { get; set; }

        #endregion

        #region Constructors

        static CardImageProvider()
        {
            CardWidth = (int)(_spriteSheet.Width / 14.0);
            CardHeight = (int)(_spriteSheet.Height / 4.0);
        }

        #endregion

        #region Methods

        public static void Rescale()
        {
            Rescale(ResizeCardWidth, ResizeCardHeight);
        }

        public static void Rescale(int cardWidth, int cardHeight)
        {
            ResizeCardWidth = cardWidth;
            ResizeCardHeight = cardHeight;

            if (_cardImages != null)
            {
                for (int i = 0; i < _cardImages.Length; i++)
                {
                    _cardImages[i]?.Dispose();
                }
            }

            _cardBack?.Dispose();
            _cardBack = null;

            _cardImages = null;
            _cardImages = new Bitmap[52];

            for (int suitIndex = 0; suitIndex < 4; suitIndex++)
            {
                for (int valueIndex = 0; valueIndex < 12; valueIndex++)
                {
                    _cardImages[suitIndex * 13 + valueIndex] = CropAndResizeBitmap(_spriteSheet, new Rectangle((valueIndex + 1) * CardWidth, suitIndex * CardHeight, CardWidth, CardHeight), new Size(ResizeCardWidth, ResizeCardHeight));
                }

                _cardImages[suitIndex * 13 + 12] = CropAndResizeBitmap(_spriteSheet, new Rectangle(0, CardHeight * suitIndex, CardWidth, CardHeight), new Size(ResizeCardWidth, ResizeCardHeight));
            }

            _cardBack = CropAndResizeBitmap(_spriteSheet, new Rectangle(CardWidth * 13, CardHeight * 3, CardWidth, CardHeight), new Size(ResizeCardWidth, ResizeCardHeight));
        }

        public static Bitmap GetCard(Card card)
        {
            if (card == null)
            {
                return _cardBack;
            }

            return _cardImages[(int)card.Suit * 13 + ((int)card.Value)];
        }

        public static Bitmap GetCardBack()
        {
            return _cardBack;
        }

        private static Bitmap CropAndResizeBitmap(Bitmap source, Rectangle cropSection, Size newSize)
        {
            Bitmap result = new Bitmap(newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(result);
            Rectangle destRect = new Rectangle(0, 0, newSize.Width, newSize.Height);

            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawImage(source, destRect, cropSection.X, cropSection.Y, cropSection.Width, cropSection.Height, GraphicsUnit.Pixel);

            return result;
        }

        #endregion
    }
}
