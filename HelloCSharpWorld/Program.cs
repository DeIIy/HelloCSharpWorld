using System;
using HelloCSharpWorld.Interfaces;
using HelloCSharpWorld.Services;
using HelloCSharpWorld.UI;

namespace HelloCSharpWorld
{
    /// <summary>
    ///     TR: Programın giriş noktasını temsil eden sınıf.
    ///     EN: Class representing the entry point of the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     TR: Programın başlangıç metodudur; gerekli bileşenleri başlatır
        ///         ve hesaplama sürecini çalıştırır.
        ///     EN: Main entry method of the program; initializes required 
        ///         components and runs the calculation process.
        /// </summary>
        /// <param name="args">
        ///     TR: Komut satırından alınan parametreler.
        ///     EN: Parameters received from the command line.
        /// </param>
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
