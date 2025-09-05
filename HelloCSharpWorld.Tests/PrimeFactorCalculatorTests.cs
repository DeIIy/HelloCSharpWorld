using HelloCSharpWorld.Services;
using Xunit;

namespace HelloCSharpWorld.Tests
{
    public class PrimeFactorCalculatorTests
    {
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
