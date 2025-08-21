using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharpWorld

{
    // ------------------------------
    // DOMAIN BÖLÜMÜ
    // ------------------------------
    public class GcdStep
    {
        public int StepNumber { get; set; }
        public int XBefore { get; set; }
        public int YBefore { get; set; }
        public string Operation { get; set; } = string.Empty;
        public int? TestedDivisor { get; set; }
        public bool? IsCommonFactor { get; set; }
        public int XAfter { get; set; }
        public int YAfter { get; set; }
        public int? Remainder { get; set; }
    }

    public class GcdResult
    {
        public int InputX { get; set; }
        public int InputY { get; set; }
        public string AlgorithmName { get; set; } = string.Empty;
        public List<GcdStep> Steps { get; set; }
    }
        
    // ------------------------------
    // ARAYÜZ BÖLÜMÜ
    // ------------------------------

    // Kullanıcı girdi arayüzü
    public interface IInputHandler
    {
        // EBOB için tam sayı girdileri alır
        int GetInteger(string message);
        string GetChoice(string message, params string[] allowedChoices);
    }

    public interface IOutputHandler
    {
        void PrintSeparator();
        void PrintIntroMessage();
    }

    // EBOB hesaplama arayüzü
    public interface IGCDCalculator
    {
        // Seçimi hatırlatan değişken
        string Name { get; }
        //EBOB hesaplayıcı fonksiyon
        int CalculateGCD(int x, int y);
        // Log fonksiyonu
        List<GcdStep> GetCalculateSteps(int x, int y);
    }
    // Tabloyu Konsola basan arayüz
    public interface ITablePrinter
    {
        void Print(List<GcdStep> steps, int gcd, string algorithmName, int x, int y);
    }
    //
    public interface IPrimeProvider
    {
        bool IsPrime(int n);
        int GetNextPrime(int currentPrime);
    }

    //
    public interface IValidator
    {
        void EnsureValidInputs(int x, int y);
    }



    // ------------------------------
    // SINIF BÖLÜMÜ
    // ------------------------------

    // Kullanıcı input sınıfı
    public class ConsoleInputHandler : IInputHandler
    {
        // Kullanıcı girdi fonksiyonu
        public int GetInteger(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();
                try
                {
                    if (input is null) throw new ArgumentException(nameof(input));
                    return int.Parse(input, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid number, please try again.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Value out of range, enter a smaller or larger number.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input was empty, please enter a number.");
                }
                catch (Exception)
                {
                    Console.WriteLine("An unexpected error occurred, please try again.");
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
                if (!string.IsNullOrEmpty(input) && allowed.Contains(input.Trim())) return input.Trim();
                Console.WriteLine($"Invalid choice. Options: {string.Join(", ", allowedChoices)}");
            }
        }
    }
    //Kullanıcı output sınıfı
    public class ConsoleOutputHandler : IOutputHandler
    {
        public void PrintSeparator()
        {
            Console.WriteLine("=========================================");
        }
        public void PrintIntroMessage()
        {
            Console.Clear();
            PrintSeparator();
            Console.WriteLine("GCD Calculator");
            PrintSeparator();
            Console.WriteLine("Welcome! This tool helps you find the\n" +
                "Greatest Common Divisor (GCD) of numbers.\n\n" +
                "Available Calculation Techniques:\n" +
                " 1) Prime Factorization \n" +
                " 2) Euclidean Algorithm ");
            PrintSeparator();
            Console.WriteLine("Tip: Choose wisely — both roads lead\n" +
                "to the GCD, but with different styles!");
            PrintSeparator();
        }
    }
    // Asal çarpanlarla EBOB hesaplayan sınıf
    public class PrimeFactorizationGCDCalculator : IGCDCalculator
    {
        // Seçimi hatırlatan değişken
        public string Name = "Prime Factorization GCD Calculator";

        string IGCDCalculator.Name => throw new NotImplementedException();

        // Asal çarpanlarla EBOB hesaplayan fonksiyon
        public int CalculateGCD(int x, int y) { return 0; }

        public List<GcdStep> GetCalculateSteps(int x, int y)
        {
            throw new NotImplementedException();
        }

        // Log fonksiyonu
        public List<GcdStep> GetCalculationSteps(int x, int y) { return new List<GcdStep>(); }
    }
    // Öklidle EBOB hesaplayan sınıf
    public class EuclideanModuloGCDCalculator : IGCDCalculator
    {
        // Seçimi hatırlatan değişken
        public string Name = "Euclidean Modulo GCD Calculator";
        string IGCDCalculator.Name => throw new NotImplementedException();

        // Öklidle EBOB hesaplayan sınıf
        public int CalculateGCD(int x, int y) { return 0; }

        public List<GcdStep> GetCalculateSteps(int x, int y)
        {
            throw new NotImplementedException();
        }

        //Log fonksiyonu
        public List<GcdStep> GetCalculationSteps() { return new List<GcdStep>(); }
    }
    // Tabloyu Konsola basan sınıf
    public class ConsoleTablePrinter : ITablePrinter
    {
        public void Print(List<GcdStep> steps, int gcd, string algorithmName, int x, int y)
        {
            //Print fonksiyonu doldurulacak
        }
    }

    //
    public class SimplePrimeProvider : IPrimeProvider
    {
        bool IsPrime(int n) { return false; }
        int GetNextPrime(int currentPrime) { return 0; }

        bool IPrimeProvider.IsPrime(int n)
        {
            return IsPrime(n);
        }

        int IPrimeProvider.GetNextPrime(int currentPrime)
        {
            return GetNextPrime(currentPrime);
        }
    }

    //
    public class BasicInputValidator : IValidator
    {
        void EnsureValidInputs(int x, int y) { }

        void IValidator.EnsureValidInputs(int x, int y)
        {
            EnsureValidInputs(x, y);
        }
    }

    // ------------------------------
    // MAIN BÖLÜMÜ
    // ------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            IOutputHandler output = new ConsoleOutputHandler();
            IInputHandler input = new ConsoleInputHandler();

            output.PrintIntroMessage();
            var choice = input.GetChoice("Which option would you like to use (1/2): ", "1", "2");

            var x = input.GetInteger("Enter the first number to calculate the GCD...");
            var y = input.GetInteger("Enter the second number to calculate the GCD...");
            Console.ReadLine();
        }
    }
}
