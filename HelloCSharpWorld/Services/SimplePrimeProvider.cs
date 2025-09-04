using System;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    /// <summary>
    ///     TR: Asallık kontrolü ve bir sonraki asal sayının elde edilmesi
    ///         işlevlerini sağlayan sınıf.
    ///     EN: Class in which primality checks and retrieval of the next 
    ///         prime number are provided.
    /// </summary>
    public class SimplePrimeProvider : IPrimeProvider
    {
        private const int MaxSearch = 1_000_000;

        /// <summary>
        ///     TR: Verilen sayının asal olup olmadığının kontrol edildiği fonksiyon.
        ///     EN: Function in which a given number is checked for primality.
        /// </summary>
        /// <param name="candidatePrime">
        ///     TR: Asal olup olmadığı kontrol edilecek sayı.
        ///     EN: The number whose primality is to be checked.
        /// </param>
        /// <returns>
        ///     TR: Sayı asal ise true, değilse false döndürülür.
        ///     EN: Returns true if the number is prime; otherwise, false.
        /// </returns>
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

        /// <summary>        
        ///     TR: Verilen bir sayıyı takip eden sonraki asal sayının bulunduğu
        ///         fonksiyon.
        ///     EN: Function in which the next prime number after a given value
        ///         is obtained.
        /// </summary>
        /// <param name="currentPrime">
        ///     TR: Kendinden sonraki asal sayının bulunacağı sayı.
        ///     EN: The number after which the next prime is to be found.
        /// </param>
        /// <returns>
        ///     TR: Kendinden sonraki asal sayı döndürülür.
        ///     EN: Returns the next prime number after the given value.
        /// </returns>
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
