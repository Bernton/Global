using DecisionDealer.Model;
using System;

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

        #endregion
    }
}
