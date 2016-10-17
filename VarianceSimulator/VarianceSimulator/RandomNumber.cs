using System;
using System.Security.Cryptography;

namespace VarianceSimulator
{
    public static class RandomNumber
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
 
        public static double GetNextDouble()
        {
            byte[] randomNumber = new byte[1];
 
            _generator.GetBytes(randomNumber);
 
            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
 
            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            return Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
        }
    }
}