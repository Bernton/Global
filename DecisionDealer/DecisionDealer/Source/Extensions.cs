using System;

namespace DecisionDealer
{
    public static class Extensions
    {
        public static int[] QuickSort(this int[] Values)
        {
            int MSD = 0;
            for (int i = 0; i < Values.Length; i++)
            {
                //bitwise OR so that we can find the largest possible number
                MSD |= Values[i];
            }

            int iPattern = 0x8000;
            while (iPattern != 0)
            {
                if ((MSD & iPattern) == iPattern)
                {
                    //Start here
                    return MSDSort(Values, iPattern, 0, Values.Length);
                }

                iPattern = iPattern >> 1;
            }

            return Values;
        }

        private static int[] MSDSort(int[] Values, int Pattern, int StartIndex, int EndIndex)
        {
            if (StartIndex == EndIndex)
                return Values;
            //Pattern should be a bit string with a 1 followed by all 0's that represents the
            //Most significant digit to start at.

            int[] OriginalIndexes = new int[EndIndex - StartIndex];

            //Copy the array items we will be sorting to the temp array
            Array.Copy(Values, StartIndex, OriginalIndexes, 0, (EndIndex - StartIndex));
            int iZeroesCount = 0;
            for (int i = 0; i < OriginalIndexes.Length; i++)
            {
                //Basic premise here is to throw all AND 1's right and all AND 0's left
                if ((OriginalIndexes[i] & Pattern) == Pattern)
                {
                    //It's an AND 1
                    Values[EndIndex - (i - iZeroesCount + 1)] = OriginalIndexes[i];
                }
                else
                {
                    //It's an AND 0
                    Values[StartIndex + iZeroesCount] = OriginalIndexes[i];
                    iZeroesCount++;
                }
            }

            //Call this function recursively twice - once for the 0's and once for the 1's
            int iNewPattern = Pattern >> 1;
            if (iNewPattern == 0)
                return Values;

            Values = MSDSort(Values, iNewPattern, StartIndex, StartIndex + iZeroesCount); // Sort the 1's
            Values = MSDSort(Values, iNewPattern, StartIndex + iZeroesCount, EndIndex);

            return Values;
        }
    }
}
