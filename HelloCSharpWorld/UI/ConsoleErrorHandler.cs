using System;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.UI
{
    /// <summary>
    ///     TR: Konsola kırmızı renkle hata mesajlarını yazdırma sorumluluğunu
    ///         üstlenen sınıf.
    ///     EN: Class responsible for displaying error messages in red on the
    ///         console.
    /// </summary>
    public class ConsoleErrorHandler : IErrorHandler
    {
        /// <summary>
        ///     TR: Verilen hatayı kırmızı renkte konsola yazdırır ve ardından
        ///         rengi sıfırlar.
        ///     EN: Prints the given error to the console in red and then resets
        ///         the color.
        /// </summary>
        public void HandleError(Error error)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error != null ? error.ToString() : "[Error]: <null>");
            Console.ForegroundColor = previous;
        }
    }
}
