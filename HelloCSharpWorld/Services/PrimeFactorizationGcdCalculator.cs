using HelloCSharpWorld.Builders;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;
using HelloCSharpWorld.Printers;

namespace HelloCSharpWorld.Services
{
    /// <summary>
    ///     TR: Asal çarpanlara ayırma yöntemiyle EBOB hesaplamasını yapan
    ///         sınıf.
    ///     EN: Class that calculates the GCD using the prime factorization
    ///         method.
    /// </summary>
    public sealed class PrimeFactorizationGcdCalculator : IGcdCalculator
    {
        /// <summary>
        ///     TR: Hesaplayıcıya ait görünen isim.
        ///     EN: Display name of the calculator.
        /// </summary>
        public string Name { get { return "Prime Factorization GCD Calculator"; } }

        /// <summary>
        ///     TR: İki sayının asal çarpanlara ayırma algoritmasıyla EBOB'unu 
        ///         hesaplar.
        ///     EN: Calculates the GCD of two numbers using the prime
        ///         factorization algorithm.
        /// </summary>
        /// <param name="x">
        ///     TR: İlk sayı değişkeni.
        ///     EN: The first number variable.
        /// </param>
        /// <param name="y">
        ///     TR: İkinci sayı değişkeni.
        ///     EN: The second number variable.
        /// </param>
        /// <returns>
        ///     TR: Hesaplanan EBOB değeri.
        ///     EN: The calculated GCD value.
        /// </returns>
        public int CalculateGcd(int x, int y)
        {
            IPrimeProvider primeProvider = new SimplePrimeProvider();
            ITableBuilder builder = new BaseTableBuilder();
            ITablePrinter printer = new PrimeFactorizationTablePrinter();

            builder.CreateNew();

            // EBOB başlangıç değişkeni
            int gcd = 1;
            // Aday asal bölen
            int factor = 2;
            // İşlem numaralandırma sayacı
            int step = 1;

            while (x != 1 || y != 1)
            {
                var s = GcdStep.Start(step, x, y, factor);

                if (x % factor == 0 && y % factor == 0)
                {
                    gcd *= factor;
                    x /= factor;
                    y /= factor;
                    s = s.WithAfter(x, y, $"Both divided by {factor} (common factor)", true, null);
                }
                else if (x % factor == 0 && y % factor != 0)
                {
                    x /= factor;
                    s = s.WithAfter(x, y, $"Only first divided by {factor}", false, null);
                }
                else if (x % factor != 0 && y % factor == 0)
                {
                    y /= factor;
                    s = s.WithAfter(x, y, $"Only second by {factor}", false, null);
                }
                else
                {
                    int next = primeProvider.GetNextPrime(factor);
                    s = s.WithAfter(x, y, $"{factor} divides none -> next prime: {next}", false, null);
                    factor = next;
                }
                builder.AddStep(s);
                step++;
            }
            printer.Print(builder.GetSteps());
            return gcd;
        }
    }
}
