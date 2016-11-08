using DecisionDealer.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DecisionDealer
{
    public static class Extensions
    {
        #region Extensions methods

        public static int[] QuickSort(this int[] values)
        {
            int MSD = 0;
            int iPattern = 0x8000;

            for (int i = 0; i < values.Length; i++)
            {
                MSD |= values[i];
            }

            while (iPattern != 0)
            {
                if ((MSD & iPattern) == iPattern)
                {
                    return values.MSDSort(iPattern, 0, values.Length);
                }

                iPattern = iPattern >> 1;
            }

            return values;
        }

        private static int[] MSDSort(this int[] values, int pattern, int startIndex, int endIndex)
        {
            int[] originalIndexes = new int[endIndex - startIndex];
            int iNewPattern = pattern >> 1;
            int iZeroesCount = 0;

            if (startIndex == endIndex)
            {
                return values;
            }

            Array.Copy(values, startIndex, originalIndexes, 0, (endIndex - startIndex));

            for (int i = 0; i < originalIndexes.Length; i++)
            {
                if ((originalIndexes[i] & pattern) == pattern)
                {
                    values[endIndex - (i - iZeroesCount + 1)] = originalIndexes[i];
                }
                else
                {
                    values[startIndex + iZeroesCount] = originalIndexes[i];
                    iZeroesCount++;
                }
            }

            if (iNewPattern == 0)
            {
                return values;
            }

            values = MSDSort(values, iNewPattern, startIndex, startIndex + iZeroesCount);
            values = MSDSort(values, iNewPattern, startIndex + iZeroesCount, endIndex);

            return values;
        }

        public static Card[] GetSubArray(this Card[] cards, int index, int length)
        {
            Card[] result = new Card[length];
            Array.Copy(cards, index, result, 0, length);
            return result;
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        #endregion
    }
}
