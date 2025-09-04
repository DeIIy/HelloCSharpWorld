using System;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    /// <summary>
    ///     TR: Kullanıcı girişlerinin doğrulanmasında kullanılan sınıf.
    ///     EN: Class in which user inputs are validated.
    /// </summary>
    public sealed class BasicInputValidator : IValidator
    {
        private const int MaxThreshold = 10_000_000;
        private readonly IErrorHandler _errorHandler;

        public BasicInputValidator() : this(new UI.ConsoleErrorHandler()) { }

        public BasicInputValidator(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        /// <summary>
        ///     TR: Kullanıcıdan alınan iki sayının geçerliliğinin kontrol 
        ///         edildiği fonksiyon
        ///     EN: Function in which the validity of two user-provided numbers
        ///         is checked.
        /// </summary>
        /// <param name="x">
        ///     TR: Geçerliliği kontrol edilecek ilk sayı.
        ///     EN: The first number to be validated.
        /// </param>
        /// <param name="y">
        ///     TR: Geçerliliği kontrol edilecek ikinci sayı.
        ///     EN: The second number to be validated.
        /// </param>
        /// <returns>
        ///     TR: Geçerli sayılar döndürülür. Eğer sayı sıfır ise, null ile
        ///         birlikte dönülür.
        ///     EN: Returns the validated numbers. If a number is zero, it is
        ///         returned along with null.
        /// </returns>
        /// <exception cref="ArgumentException">        
        ///     TR: Her iki sayının da sıfır olması durumunda fırlatılır
        ///         (EBOB(0, 0) tanımsızdır).
        ///     EN: Thrown when both numbers are zero (GCD(0, 0) is mathematically
        ///         undefined).
        /// </exception>
        /// <exception cref="OverflowException">        
        ///     TR: Sayılardan biri veya her ikisi izin verilen maksimum değeri
        ///         aştığında fırlatılır.
        ///     EN: Thrown when one or both numbers exceed the allowed maximum
        ///         value.
        /// </exception>
        public (int? x, int? y) EnsureValidInputs(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            if (x == 0 && y == 0)
            {
                _errorHandler.HandleError(new Core.Error(ErrorCode.InvalidFormat, Messages.ErrorBothZero));
                throw new ValidationException(ErrorCode.InvalidFormat, Messages.ErrorBothZero);
            }
            if (x > MaxThreshold || y > MaxThreshold)
            {
                string msg = string.Format(Messages.ErrorTooLarge, MaxThreshold);
                _errorHandler.HandleError(new Error(ErrorCode.OutOfRange, msg));
                throw new ValidationException(ErrorCode.OutOfRange, msg);
            }
            if (x == 0 && y != 0) return (y, null);
            if (x != 0 && y == 0) return (x, null);
            return (x, y);
        }
    }
}
