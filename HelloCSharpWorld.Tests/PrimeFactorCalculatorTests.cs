using HelloCSharpWorld.Services;
using Xunit;

namespace HelloCSharpWorld.Tests
{
    /// <summary>
    ///     TR: <see cref="PrimeFactorizationGcdCalculator"/> sınıfı için birim testlerini içerir.
    ///     EN: Contains unit tests for the <see cref="PrimeFactorizationGcdCalculator"/> class.
    /// </summary>
    public class PrimeFactorCalculatorTests
    {
        /// <summary>
        ///     TR: Asal çarpan yöntemi kullanılarak EBOB hesaplamalarının doğru sonuç verdiğini test eder.
        ///     EN: Tests that GCD calculations using the prime factorization method return correct results.
        /// </summary>
        /// <param name="a">
        ///     TR: Test edilecek ilk sayı.
        ///     EN: The first number to test.
        /// </param>
        /// <param name="b">
        ///     TR: Test edilecek ikinci sayı.
        ///     EN: The second number to test.
        /// </param>
        /// <param name="expected">
        ///     TR: Beklenen EBOB sonucu.
        ///     EN: The expected GCD result.
        /// </param>
        [Theory]
        [InlineData(48, 18, 6)]
        [InlineData(54, 24, 6)]
        [InlineData(101, 103, 1)]
        [InlineData(20, 5, 5)]
        public void PrimeFactorization_ComputesGcd(int a, int b, int expected)
        {
            var calc = new PrimeFactorizationGcdCalculator();
            var result = calc.CalculateGcd(a, b);
            Assert.Equal(expected, result);
        }
    }
}
