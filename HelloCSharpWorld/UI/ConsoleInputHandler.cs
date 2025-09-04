using System;
using System.Collections.Generic;
using System.Globalization;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.UI
{
    public sealed class ConsoleInputHandler : IInputHandler
    {
        private readonly IErrorHandler _errorHandler;

        public ConsoleInputHandler(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public int GetInteger(string message)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        _errorHandler.HandleError(new Core.Error(ErrorCode.EmptyInput, Messages.ErrorEmptyInput));
                        continue;
                    }

                    input = input.Trim();
                    int value;
                    long big;

                    if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    {
                        return value;
                    }

                    if (long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out big))
                    {
                        _errorHandler.HandleError(new Error(ErrorCode.OutOfRange, Messages.ErrorOutOfRange));
                    }
                    else
                    {
                        _errorHandler.HandleError(new Error(ErrorCode.InvalidFormat, string.Format(Messages.ErrorInvalidInteger, input)));
                    }
                }
                catch
                {
                    _errorHandler.HandleError(new Error(ErrorCode.Unexpected, Messages.ErrorUnexpected));
                }
            }
        }

        public string GetChoice(string message, params string[] allowedChoices)
        {
            var allowed = new HashSet<string>(allowedChoices, StringComparer.OrdinalIgnoreCase);

            while (true)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var trimmed = input.Trim();
                    if (allowed.Contains(trimmed)) return trimmed;
                }

                _errorHandler.HandleError(new Error(ErrorCode.InvalidChoice,
                    string.Format(Messages.ErrorInvalidChoice, string.Join(", ", allowedChoices))));
            }
        }

        public (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage)
        {
            var x = GetInteger(firstMessage);
            var y = GetInteger(secondMessage);
            return (x, y);
        }
    }
}
