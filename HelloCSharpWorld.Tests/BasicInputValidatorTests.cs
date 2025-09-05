using HelloCSharpWorld.Core;
using HelloCSharpWorld.Services;
using HelloCSharpWorld.UI;
using Xunit;

namespace HelloCSharpWorld.Tests
{
    public class BasicInputValidatorTests
    {
        [Fact]
        public void EnsureValidInputs_AllowsNormalValues()
        {
            var validator = new BasicInputValidator(new ConsoleErrorHandler());
            var result = validator.EnsureValidInputs(12, 18);
            Assert.Equal(12, result.x);
            Assert.Equal(18, result.y);
        }

        [Fact]
        public void EnsureValidInputs_BothZero_Throws()
        {
            var validator = new BasicInputValidator(new ConsoleErrorHandler());
            Assert.Throws<ValidationException>(() => validator.EnsureValidInputs(0, 0));
        }

        [Fact]
        public void EnsureValidInputs_TooLarge_Throws()
        {
            var validator = new BasicInputValidator(new ConsoleErrorHandler());
            Assert.Throws<ValidationException>(() => validator.EnsureValidInputs(20_000_000, 1));
        }
    }
}