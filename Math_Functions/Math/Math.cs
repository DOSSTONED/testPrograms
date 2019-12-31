using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    public static class Math
    {
        

        public static double Pow(double x, uint n)
        {
            double ret = 1;
            while (n > 0)
            {
                if (n % 2 == 0)
                {
                    x = x * x;
                    n = n / 2;
                }
                else
                {
                    ret = ret * x;
                    n--;
                }
            }
            return ret;
        }

        public static double Pow_Normal(double x, uint n)
        {
            double ret = 1;
            while (n > 0)
            {
                ret = ret * x;
                n--;
            }
            return ret;
        }

        public static UInt64 Factorial(UInt16 n)
        {
            UInt64 ret = 1;
            while (n > 0)
            {
                ret *= n;
                n--;
            }
            return ret;
        }

        public static int[] GivePrimes(int n)
        {
            int[] prime = new int[n + 1];
            for (int i = 2; i < prime.Length; i++)
            {
                prime[i] = 1;
            }
            List<int> primes = new List<int>();
            for (int i = 2; i < prime.Length; i++)
            {
                primes.Add(i);
                int j = 0;
                while (j * i <= n)
                {
                    prime[j++ * i] = 0;
                }
            }

            return primes.ToArray();

        }

        public static UInt64 Factorial_Normal(UInt16 n, int[] primes)
        {
            UInt64 ret = 1;
            int index=0;
            foreach (int prime in primes)
            {
                index=0;
                while (n / System.Math.Pow(prime, index) >= 1)
                {
                    ret = ret * (UInt64)System.Math.Pow(prime, index);
                    index++;
                }
            }

            return ret;
        }
    }
}
