using System;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.UI
{
    public sealed class ConsoleOutputHandler : IOutputHandler
    {
        public void PrintSeparator()
        {
            Console.WriteLine(Messages.Separator);
        }

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
