using System;
using System.Numerics;

namespace Probability.Model
{
    public static class Prob
    {
        public static BigInteger Combination(int n, int r, bool repitition)
        {
            if(repitition)
            {
                return Factorial(n + r - 1) / (Factorial(r) * Factorial(n - 1));
            }
            else
            {
                return Factorial(n) / (Factorial(r) * Factorial(n - r));
            }
        }

        public static BigInteger Permutation(int n, int r, bool repitition)
        {
            if(repitition)
            {
                BigInteger result = 1;

                for (int i = 0; i < r; i++)
                {
                    result *= n;
                }

                return result;
            }
            else
            {
                return Factorial(n) / Factorial(n - r);
            }
        }

        public static BigInteger Factorial(int x)
        {
            BigInteger result = 1;

            for (int i = 2; i <= x; i++)
            {
                result *= i;
            }

            return result;
        }

        public static BigInteger Factorial(int n, int r)
        {
            BigInteger result = 1;

            for (int i = 0; i < r; i++)
            {
                result *= n - i;
            }

            return result;
        }
    }
}
