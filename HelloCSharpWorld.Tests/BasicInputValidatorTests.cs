using HelloCSharpWorld.Core;
using HelloCSharpWorld.Services;
using HelloCSharpWorld.UI;
using Xunit;

namespace HelloCSharpWorld.Tests
{
    /// <summary>
    ///     TR: <see cref="BasicInputValidator"/> s�n�f� i�in birim testlerini i�erir.
    ///     EN: Contains unit tests for the <see cref="BasicInputValidator"/> class.
    /// </summary>
    public class BasicInputValidatorTests
    {
        /// <summary>
        ///     TR: Ge�erli say� giri�lerinin do�ruland���n� test eder.
        ///     EN: Tests that valid number inputs are accepted.
        /// </summary>
        [Fact]
        public void EnsureValidInputs_AllowsNormalValues()
        {
            var validator = new BasicInputValidator(new ConsoleErrorHandler());
            var result = validator.EnsureValidInputs(12, 18);
            Assert.Equal(12, result.x);
            Assert.Equal(18, result.y);
        }

        /// <summary>
        ///     TR: Her iki giri�in de s�f�r olmas� durumunda hata f�rlat�ld���n� test eder.
        ///     EN: Tests that an exception is thrown when both inputs are zero.
        /// </summary>
        [Fact]
        public void EnsureValidInputs_BothZero_Throws()
        {
            var validator = new BasicInputValidator(new ConsoleErrorHandler());
            Assert.Throws<ValidationException>(() => validator.EnsureValidInputs(0, 0));
        }

        /// <summary>
        ///     TR: �ok b�y�k de�er girildi�inde hata f�rlat�ld���n� test eder.
        ///     EN: Tests that an exception is thrown when input values are too large.
        /// </summary>
        [Fact]
        public void EnsureValidInputs_TooLarge_Throws()
        {
            var validator = new BasicInputValidator(new ConsoleErrorHandler());
            Assert.Throws<ValidationException>(() => validator.EnsureValidInputs(20_000_000, 1));
        }
    }
}