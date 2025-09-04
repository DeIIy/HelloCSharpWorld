using System;
using HelloCSharpWorld.Interfaces;
using HelloCSharpWorld.Core;

namespace HelloCSharpWorld.UI
{
    /// <summary>
    ///     TR: Kullanıcı bilgilendirme mesajı verme ve ayraç çizgilerini
    ///         konsola yazdırma görevlerini üstlenen arayüz. 
    ///     EN: Interface responsible for displaying user information messages
    ///         and printing separator lines to the console.
    /// </summary>
    public sealed class ConsoleOutputHandler : IOutputHandler
    {
        /// <summary>
        ///     TR: Konsola ayraç çizgisi yazdırır.
        ///     EN: Prints a separator line to the console.
        /// </summary>
        public void PrintSeparator()
        {
            Console.WriteLine(Messages.Separator);
        }

        /// <summary>
        ///     TR: Konsolu temizleyerek giriş mesajını ve mevcut EBOB hesaplama
        ///         tekniklerini kullanıcıya gösterir.
        ///     EN: Clears the console and displays the introduction message along
        ///         with the available GCD calculation techniques.
        /// </summary>
        public void PrintIntroMessage()
        {
            Console.Clear();
            PrintSeparator();
            Console.WriteLine(Messages.AppTitle);
            PrintSeparator();
            Console.WriteLine(Messages.Intro);
            PrintSeparator();
            Console.WriteLine(Messages.Tip);
            PrintSeparator();
        }

        public void PrintLine(string message)
        {
            Console.WriteLine(message ?? string.Empty);
        }
    }
}
