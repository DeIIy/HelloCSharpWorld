using HelloCSharpWorld.Services;
using Xunit;

namespace HelloCSharpWorld.Tests
{
    /// <summary>
    ///     TR: <see cref="SimplePrimeProvider"/> sınıfı için birim testlerini içerir.
    ///     EN: Contains unit tests for the <see cref="SimplePrimeProvider"/> class.
    /// </summary>
    public class PrimeProviderTests
    {
        /// <summary>
        ///     TR: Bir sayının asal olup olmadığının doğru şekilde belirlendiğini test eder.
        ///     EN: Tests that prime numbers are correctly identified.
        /// </summary>
        /// <param name="value">
        ///     TR: Test edilecek sayı.
        ///     EN: The number to be tested.
        /// </param>
        /// <param name="expected">
        ///     TR: Beklenen sonuç (asal ise true, değilse false).
        ///     EN: Expected result (true if prime, false otherwise).
        /// </param>
        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(17, true)]
        [InlineData(18, false)]
        public void IsPrime_Works(int value, bool expected)
        {
            var provider = new SimplePrimeProvider();
            var result = provider.IsPrime(value);
            Assert.Equal(expected, result);
        }

        /// <summary>
        ///     TR: Verilen bir sayının ardından gelen doğru asal sayının bulunduğunu test eder.
        ///     EN: Tests that the correct next prime number is found after a given number.
        /// </summary>
        [Fact]
        public void GetNextPrime_FindsNextCorrectly()
        {
            var provider = new SimplePrimeProvider();
            var next = provider.GetNextPrime(3);
            Assert.Equal(5, next);
        }
    }
}
