using System;
using HelloCSharpWorld.Interfaces;
using HelloCSharpWorld.Services;
using HelloCSharpWorld.UI;

namespace HelloCSharpWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IErrorHandler errorHandler = new ConsoleErrorHandler();
            IOutputHandler output = new ConsoleOutputHandler();
            IInputHandler input = new ConsoleInputHandler(errorHandler);
            IValidator validator = new BasicInputValidator();
            ICalculatorFactory factory = new GcdCalculatorFactory();
            ICalculationRunner runner = new CalculationRunner(input, output, validator, factory, errorHandler);

            runner.Run();
            Console.ReadLine();
        }
    }
}
