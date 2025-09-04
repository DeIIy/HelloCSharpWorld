using System;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    public class SimplePrimeProvider : IPrimeProvider
    {
        private const int MaxSearch = 1_000_000;

        public bool IsPrime(int candidate)
        {
            if (candidate < 2) return false;
            if (candidate == 2) return true;
            if (candidate % 2 == 0) return false;

            var limit = (int)Math.Floor(Math.Sqrt(candidate));

            for (int i = 3; i <= limit; i += 2)
                if (candidate % i == 0) return false;
            return true;
        }

        public int GetNextPrime(int currentPrime)
        {
            int attempts = 0;
            int p = currentPrime;

            while (attempts < MaxSearch)
            {
                attempts++;
                checked { p++; }
                if (IsPrime(p)) return p;
            }

            throw new InvalidOperationException("Prime search limit exceeded.");
        }
    }
}
