using System;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.UI
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void HandleError(Error error)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error != null ? error.ToString() : "[Error]: <null>");
            Console.ForegroundColor = previous;
        }
    }
}
