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
    // DOMAIN LAYER - DOMAIN BÖLÜMÜ
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
    // INTERFACE LAYER - ARAYÜZ BÖLÜMÜ
    // ------------------------------

    // User Input Interface - Kullanıcı girdi arayüzü
    public interface IInputHandler
    {
        // Takes integer inputs for GCD - EBOB için tam sayı girdileri alır
        int GetInteger(string message);
        string GetChoice(string message, params string[] allowedChoices);
    }

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



    // ------------------------------
    // CLASS LAYER - SINIF BÖLÜMÜ
    // ------------------------------

    // User Input Class - Kullanıcı girdi sınıfı
    public class ConsoleInputHandler : IInputHandler
    {
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
                    Console.WriteLine("Invalid number, please try again."); // Geçersiz sayı, lütfen tekrar deneyin.
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Value out of range, enter a smaller or larger number."); // Değer aralık dışında, lütfen daha küçük veya daha büyük bir sayı girin.
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input was empty, please enter a number."); // Girdi boş, lütfen bir sayı girin.
                }
                catch (Exception)
                {
                    Console.WriteLine("An unexpected error occurred, please try again."); // Beklenmeyen bir hata oluştu, lütfen tekrar deneyin.
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
                Console.WriteLine($"Invalid choice. Options: {string.Join(", ", allowedChoices)}"); // Geçersiz seçim. Seçenekler: 1, 2
            }
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
            IValidator validator = new BasicInputValidator();

            var (validX, validY) = validator.EnsureValidInputs(x, y);
            if (validY == null) return (int)validX;
            x = (int)validX;
            y = (int)validY;
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
            IValidator validator = new BasicInputValidator();

            var (validX, validY) = validator.EnsureValidInputs(x, y);
            if (validY is null) return (int)validX;
            x = (int)validX;
            y = (int)validY;

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

    // ------------------------------
    // MAIN LAYER - ANA BÖLÜM
    // ------------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            IOutputHandler output = new ConsoleOutputHandler();
            IInputHandler input = new ConsoleInputHandler();

            output.PrintIntroMessage();
            var choice = input.GetChoice("Which option would you like to use (1/2): ", "1", "2"); // Hangi seçeneği kullanmak istersiniz (1/2):

            IGCDCalculator calculator;
            switch (choice)
            {
                case "1":
                    // Option 1: Prime Factorization Method - Seçenek 1: Asal Çarpanlara Ayırma Yöntemi
                    calculator = new PrimeFactorizationGCDCalculator();
                    break;
                case "2":
                    // Option 2: Euclidean Algorithm - Seçenek 2: Öklid Algoritması
                    calculator = new EuclideanModuloGCDCalculator();
                    break;
                default:
                    // Default Option: Falls back to Prime Factorization - Varsayılan Seçenek: Asal Çarpanlara Ayırma Yöntemi kullanılır
                    calculator = new PrimeFactorizationGCDCalculator();
                    break;
            }

            var x = input.GetInteger("Enter the first number to calculate the GCD..."); // EBOB hesaplamak için ilk sayıyı girin...
            var y = input.GetInteger("Enter the second number to calculate the GCD..."); // EBOB hesaplamak için ikinci sayıyı girin...
            Console.ReadLine();
        }
    }
}
