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
        public int GetInteger(string message) { return 0; }
    }
    //Kullanıcı output sınıfı
    public class ConsoleOutputHandler : IOutputHandler
    {
        public void PrintSeparator()
        {
            // Doldurulacak
        }
        public void PrintIntroMessage()
        {
            // Doldurulacak
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
            // Main
        }
    }
}
