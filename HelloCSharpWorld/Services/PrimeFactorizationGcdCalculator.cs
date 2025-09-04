using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    public sealed class PrimeFactorizationGcdCalculator : IGcdCalculator
    {
        public string Name { get { return "Prime Factorization GCD Calculator"; } }

        public int CalculateGcd(int x, int y)
        {
            IPrimeProvider primeProvider = new SimplePrimeProvider();
            ITableBuilder builder = new PrimeFactorizationTableBuilder();
            ITablePrinter printer = new PrimeFactorizationTablePrinter();

            builder.CreateNew();

            int gcd = 1;
            int factor = 2;
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
