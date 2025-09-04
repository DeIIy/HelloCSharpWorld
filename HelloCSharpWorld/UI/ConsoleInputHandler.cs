using System;
using System.Collections.Generic;
using System.Globalization;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.UI
{
    /// <summary>
    ///     TR: Konsol üzerinden kullanıcıdan veri almak için giriş işleyici 
    ///     sınıfı.
    ///     EN: Input handler class for retrieving user input from the console.
    /// </summary>
    public sealed class ConsoleInputHandler : IInputHandler
    {
        /// <summary>
        ///     TR: Hata işleyici arayüz.
        ///     EN: Error handler interface.
        /// </summary>
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        ///     TR: Konsol giriş arayüzünü, belirtilen hata arayüzüyle başlatır.
        ///     EN: Initializes the console input handler with the specified
        ///         error handler.
        /// </summary>
        /// <param name="errorHandler">
        ///     
        ///     TR: Hataları yönetmek için kullanılan hata arayüzü değişkeni.
        ///     EN: Error handler variable used to manage errors.
        /// </param>
        public ConsoleInputHandler(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        /// <summary>
        ///     TR: Kullanıcıdan geçerli bir tam sayı alır.
        ///     EN: Retrieves a valid integer from the user.
        /// </summary>
        /// <param name="message">
        ///     TR: Konsola yazdırılacak yönlendirici mesaj.
        ///     EN: The prompt message to display in the console.
        /// </param>
        /// <returns>
        ///     TR: Kullanıcıdan alınan geçerli tam sayı.
        ///     EN: The valid integer entered by the user.
        /// </returns>
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

        /// <summary>
        ///     TR: Kullanıcıdan yalnızca belirli seçeneklerden biri olacak şekilde bir seçim alır. 
        ///     EN: Retrieves a choice from the user, restricted to the allowed options.         
        /// </summary>
        /// <param name="message">
        ///     TR: Konsola yazılacak yönlendirici mesaj.
        ///     EN: The prompt message to display in the console.
        /// </param>
        /// <param name="allowedChoices">
        ///     TR: Kullanıcıya sunulan geçerli seçenekler.
        ///     EN: The set of allowed choices for the user.
        /// </param>
        /// <returns>
        ///     TR: Kullanıcının yaptığı geçerli seçim.
        ///     EN: The valid choice entered by the user.
        /// </returns>
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

        /// <summary>
        ///     TR: Kullanıcıdan iki tam sayı alır. 
        ///     EN: Retrieves two integers from the user.
        /// </summary>
        /// <param name="firstMessage">
        ///     TR: İlk sayı için mesaj.  
        ///     EN: Prompt message for the first number.
        /// </param>
        /// <param name="secondMessage">
        ///     TR: İkinci sayı için mesaj.  
        ///     EN: Prompt message for the second number.
        /// </param>
        /// <returns>
        ///     TR: Kullanıcıdan alınan iki sayı (x, y).
        ///     EN: A tuple containing the two integers (x, y).
        /// </returns>
        public (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage)
        {
            var x = GetInteger(firstMessage);
            var y = GetInteger(secondMessage);
            return (x, y);
        }
    }
}
