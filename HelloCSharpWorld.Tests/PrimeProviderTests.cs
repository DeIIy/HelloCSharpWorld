using HelloCSharpWorld.Services;
using Xunit;

namespace HelloCSharpWorld.Tests
{
    public class PrimeProviderTests
    {
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

        [Fact]
        public void GetNextPrime_FindsNextCorrectly()
        {
            var provider = new SimplePrimeProvider();
            var next = provider.GetNextPrime(3);
            Assert.Equal(5, next);
        }
    }
}
