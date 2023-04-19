using System;
using System.Security.Cryptography;

namespace VarianceSimulator
{
    public class RandomGenerator
    {
        readonly RNGCryptoServiceProvider _cryptoServiceProvider = new RNGCryptoServiceProvider();

        public double Next()
        {
            byte[] bytes = new byte[8];
            _cryptoServiceProvider.GetBytes(bytes);

            // Step 2: bit-shift 11 and 53 based on double's mantissa bits
            ulong ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            double d = ul / (double)(1UL << 53);

            return d;
        }
    }
}