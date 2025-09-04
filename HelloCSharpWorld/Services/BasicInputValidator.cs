using System;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    public sealed class BasicInputValidator : IValidator
    {
        private const int MaxThreshold = 10_000_000;
        private readonly IErrorHandler _errorHandler;

        public BasicInputValidator() : this(new UI.ConsoleErrorHandler()) { }

        public BasicInputValidator(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

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
