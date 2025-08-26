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
    // ENUM LAYER - SABİT BÖLÜMÜ
    // ------------------------------
    public enum ErrorCode
    {
        InvalidChoice,
        EmptyInput,
        InvalidFormat,
        OutOfRange,
        Unexpected
    }
    // ------------------------------
    // DOMAIN LAYER - DOMAIN BÖLÜMÜ
    // ------------------------------
    public class Error
    {
        public ErrorCode Code { get; }
        public string Message { get; }

        public Error(ErrorCode code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"[Error][{Code}]: {Message}";
        }
    }
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
    // INTERFACE LAYER - ARAYÜZ BÖLÜMÜ
    // ------------------------------

    // User Input Interface - Kullanıcı girdi arayüzü
    public interface IInputHandler
    {
        // Takes integer inputs for GCD - EBOB için tam sayı girdileri alır
        int GetInteger(string message);
        string GetChoice(string message, params string[] allowedChoices);
        (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage);
    }
    // User Output Interface - Kullanıcı çıktısı arayüzü
    public interface IOutputHandler
    {
        void PrintSeparator();
        void PrintIntroMessage();
    }

    // GCD Calculation Interface - EBOB hesaplama arayüzü
    public interface IGCDCalculator
    {
        // Variable holding selection name - Seçimin adını tutan değişken
        string Name { get; }
        // GCD Calculator Function - EBOB hesaplayıcı fonksiyon
        int CalculateGCD(int x, int y);
        // Logger - Kayıt fonksiyonu
        List<GcdStep> GetCalculateSteps(int x, int y);
    }
    // Factory Interface - Fabrika arayüzü
    public interface ICalculatorFactory
    {
        IGCDCalculator Create(string choice);
    }
    // Runner Interface - Çalıştırıcı arayüz
    public interface ICalculationRunner
    {
        void Run();
    }
    // Console Table Output - Tabloyu Konsola basan arayüz
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
        (int? x, int? y) EnsureValidInputs(int x, int y);
    }
    //
    public interface IErrorHandler
    {
        void HandleError(Error error);
    }

    // ------------------------------
    // CLASS LAYER - SINIF BÖLÜMÜ
    // ------------------------------

    // User Input Class - Kullanıcı girdi sınıfı
    public class ConsoleInputHandler : IInputHandler
    {
        private readonly IErrorHandler _errorHandler;
        public ConsoleInputHandler(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }
        // User Input Function - Kullanıcı girdi fonksiyonu
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
                    _errorHandler.HandleError(new Error(ErrorCode.InvalidFormat, "Invalid number, please try again"));
                }
                catch (OverflowException)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.OutOfRange, "Value out of range, enter a smaller or larger number"));
                }
                catch (ArgumentException)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.EmptyInput, "Input was empty, please enter a number."));
                }
                catch (Exception)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.Unexpected, "An unexpected error occurred, please try again."));
                }
            }
        }
        //
        public string GetChoice(string message, params string[] allowedChoices)
        {
            var allowed = new HashSet<string>(allowedChoices, StringComparer.OrdinalIgnoreCase);
            while (true)
            {
                Console.WriteLine(message);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && allowed.Contains(input.Trim())) return input.Trim();

                _errorHandler.HandleError(new Error(ErrorCode.InvalidChoice, $"Invalid choice. Options: {string.Join(", ", allowedChoices)}"));
            }
        }
        public (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage)
        {
            var x = GetInteger(firstMessage);
            var y = GetInteger(secondMessage);
            return (x, y);
        }
    }
    // User Output Class - Kullanıcı çıktı sınıfı
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
            /*
             =========================================
             EBOB Hesaplayıcı
             =========================================
             Hoş geldiniz! Bu araç, sayıların
             En Büyük Ortak Bölenini (EBOB) bulmanıza yardımcı olur.

            Mevcut Hesaplama Yöntemleri:
            1) Asal Çarpanlara Ayırma
            2) Öklid Algoritması
            =========================================
            İpucu: Seçiminizi dikkatli yapın — her iki yol da
                         EBOB’a ulaşır, ama farklı tarzlarda!
            =========================================
             */
        }
    }
    // Prime Factor GCD Class - Asal çarpanlarla EBOB hesaplayan sınıf
    public class PrimeFactorizationGCDCalculator : IGCDCalculator
    {
        // Variable holding selection name - Seçimin adını tutan değişken
        public string Name { get; } = "Prime Factorization GCD Calculator"; // Asal Çarpanlarla EBOB Hesaplayıcı

        // GCD by Prime Factors Function - Asal çarpanlarla EBOB hesaplayan fonksiyon
        public int CalculateGCD(int x, int y)
        {
            IPrimeProvider primeProvider = new SimplePrimeProvider();

            int gcdResult = 1;
            int factorCandidate = 2;
            while (x != 1 || y != 1)
            {
                if (x % factorCandidate == 0 && y % factorCandidate == 0)
                {
                    gcdResult *= factorCandidate;
                    x /= factorCandidate;
                    y /= factorCandidate;
                }
                else if (x % factorCandidate == 0 && y % factorCandidate != 0)
                {
                    x /= factorCandidate;
                }
                else if (x % factorCandidate != 0 && y % factorCandidate == 0)
                {
                    y /= factorCandidate;
                }
                else
                {
                    factorCandidate = primeProvider.GetNextPrime(factorCandidate);
                }
            }
            return gcdResult;
        }


        // Logger - Kayıt fonksiyonu
        public List<GcdStep> GetCalculateSteps(int x, int y) { return new List<GcdStep>(); }
    }
    // Euclidean GCD Class - Öklidle EBOB hesaplayan sınıf
    public class EuclideanModuloGCDCalculator : IGCDCalculator
    {
        // Variable holding selection name - Seçimin adını tutan değişken
        public string Name { get; } = "Euclidean Modulo GCD Calculator"; // Öklid Modülü EBOB Hesaplayıcısı

        // GCD by Euclidean Function - Öklidle EBOB hesaplayan fonksiyon
        public int CalculateGCD(int x, int y)
        {
            while (y != 0)
            {
                int temp = y;
                y = x % y;
                x = temp;
            }
            return x;
        }

        // Logger - Kayıt fonksiyonu
        public List<GcdStep> GetCalculateSteps(int x, int y) { return new List<GcdStep>(); }
    }
    // Console Table Printer Class - Tabloyu Konsola basan sınıf
    public class ConsoleTablePrinter : ITablePrinter
    {
        public void Print(List<GcdStep> steps, int gcd, string algorithmName, int x, int y)
        {
            // Print function to be implemented - Print fonksiyonu doldurulacak
        }
    }

    // Simple Operations Function - Basit işlemler fonksiyonu
    public class SimplePrimeProvider : IPrimeProvider
    {
        // Checks if number is prime - Asallık durumunu kontrol eder
        public bool IsPrime(int candidatePrime)
        {
            if (candidatePrime < 2) return false;
            for (int i = 2; i < candidatePrime; i++)
            {
                if (candidatePrime % i == 0) return false;
            }
            return true;
        }
        // Finds the next prime number - Sonraki asal sayıyı bulur
        public int GetNextPrime(int currentPrime)
        {
            do
            {
                currentPrime++;
                bool isPrime = IsPrime(currentPrime);
                if (isPrime) break;
            } while (true);
            return currentPrime;
        }
    }

    // Validate Inputs Function - Girdileri doğrulama fonksiyonu
    public class BasicInputValidator : IValidator
    {
        private const int MaxThreshold = 10000000;
        public (int? x, int? y) EnsureValidInputs(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            if (x == 0 && y == 0)
            {
                throw new ArgumentException("GCD(0,0) is undefined"); // GCD(0,0) tanımsızdır. 
            }
            else if (x == 0 && y != 0) return (y, null);
            else if (x != 0 && y == 0) return (x, null);
            if (x > MaxThreshold || y > MaxThreshold)
            {
                throw new OverflowException("Input too large for chosen GCD algorithm"); // Seçilen EBOB algoritması için girdi çok büyük 
            }
            return (x, y);
        }
    }
    // Creates calculator based on choice - Seçime göre hesaplayıcı oluşturur
    public class GcdCalculatorFactory : ICalculatorFactory
    {
        public IGCDCalculator Create(string choice)
        {
            switch (choice)
            {
                case "1":
                    return new PrimeFactorizationGCDCalculator(); // Option 1: Prime Factorization Method - Seçenek 1: Asal Çarpanlara Ayırma Yöntemi
                case "2":
                    return new EuclideanModuloGCDCalculator(); // Option 2: Euclidean Algorithm - Seçenek 2: Öklid Algoritması
                default:
                    return new PrimeFactorizationGCDCalculator(); // Default Option: Falls back to Prime Factorization - Varsayılan Seçenek: Asal Çarpanlara Ayırma Yöntemi kullanılır
            }
        }
    }
    //
    public class CalculationRunner : ICalculationRunner
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IInputHandler _input;
        private readonly IOutputHandler _output;
        private readonly IValidator _validator;
        private readonly ICalculatorFactory _calculatorFactory;

        public CalculationRunner(IInputHandler input, IOutputHandler output, IValidator validator, ICalculatorFactory calculatorFactory, IErrorHandler errorHandler)
        {
            _input = input;
            _output = output;
            _validator = validator;
            _calculatorFactory = calculatorFactory;
            _errorHandler = errorHandler;
        }
        // Ruling Class - Yönetici Sınıf
        public void Run()
        {
            _output.PrintIntroMessage();

            var choice = _input.GetChoice("Which option would you like to use(1 / 2): ", "1", "2"); // Hangi seçeneği kullanmak istersiniz (1/2):
            IGCDCalculator calculator = _calculatorFactory.Create(choice);

            var (x, y) = _input.GetNumberFromUser("Enter the first number to calculate the GCD...", "Enter the second number to calculate the GCD..."); // EBOB hesaplamak için ilk sayıyı girin... EBOB hesaplamak için ikinci sayıyı girin...

            (int? validX, int? validY) validated;

            while (true)
            {
                try
                {
                    validated = _validator.EnsureValidInputs(x, y);
                    break;
                }
                catch (ArgumentException e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.InvalidFormat, e.Message));
                    (x, y) = _input.GetNumberFromUser("Enter the first number again:", "Enter the second number again:");
                }
                catch (OverflowException e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.OutOfRange, e.Message));
                    (x, y) = _input.GetNumberFromUser("Enter the first number again:", "Enter the second number again:");
                }
                catch (Exception e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.Unexpected, e.Message));
                    (x, y) = _input.GetNumberFromUser("Enter the first number again", "Enter the second number again:");
                }
            }

            var validX = validated.Item1;
            var validY = validated.Item2;

            int gcdResult;
            if (validY == null) gcdResult = (int)validX;
            else gcdResult = calculator.CalculateGCD(validX.Value, validY.Value);

            _output.PrintSeparator();
            Console.WriteLine("The GCD result is: " + gcdResult);
            _output.PrintSeparator();
        }
    }
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void HandleError(Error error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error.ToString());
            Console.ResetColor();
        }
    }

    // ------------------------------
    // MAIN LAYER - ANA BÖLÜM
    // ------------------------------

    internal class Program
    {
        static void Main(string[] args)
        {
            IErrorHandler errorHandler = new ConsoleErrorHandler();
            IOutputHandler output = new ConsoleOutputHandler();
            IInputHandler input = new ConsoleInputHandler(errorHandler);
            IValidator validator = new BasicInputValidator();
            ICalculatorFactory calculatorFactory = new GcdCalculatorFactory();

            ICalculationRunner runner = new CalculationRunner(input, output, validator, calculatorFactory, errorHandler);
            runner.Run();

            Console.ReadLine();
        }
    }
}
