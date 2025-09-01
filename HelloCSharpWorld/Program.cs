using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Temeller
{
    /// <summary>
    ///     TR: Uygulama boyunca kullanılacak: 'ArgumentOutOfRangeException,
    ///         ArgumentNullException, FormatException, OverflowException, 
    ///         NullReferenceExpection, Exception' hataları temsil eden enum
    ///         sabitleri.
    ///     EN: Exceptions to be used throughout the application: 'OverflowException,
    ///         ArgumentOutOfRangeException, ArgumentNullException, FormatException,
    ///         NullReferenceException, Exception' enumerated constants.
    /// </summary>
    public enum ErrorCode
    {
        InvalidChoice,
        EmptyInput,
        InvalidFormat,
        OutOfRange,
        NullDependency,
        Unexpected
    }
    /// <summary>
    ///     TR: EBOB hesaplamasındaki (asal çarpan ve öklid hesaplamalarıyla)
    ///         işlem değerlerini (basamak numarası, önceki sayı değerleri, 
    ///         sonraki sayı değerleri, kısa açıklamalar, asal çarpan veya
    ///         öklide özel değerler) saklayan işlem basamakları sınıfı.
    ///     EN: A class that stores the computation steps of the GCD 
    ///         calculation (using prime factorization and Euclidean methods),
    ///         including step number, previous values, next values, short
    ///         descriptions, and special values specific to prime factorization
    ///         or the Euclidean algorithm.
    /// </summary>
    public class GcdStep
    {
        public int StepNumber { get; set; }
        public int XBefore { get; set; }
        public int YAfter { get; set; }
        public string Operation { get; set; } = string.Empty;
        public int? TestedDivisor { get; set; }
        public bool? IsCommonFactor { get; set; }
        public int XAfter { get; set; }
        public int YBefore { get; set; }
        public int? Remainder { get; set; }
    }
    /// <summary>
    ///     TR: Hata bilgilerini kapsülleyen hata kodu ve açıklayıcı hata 
    ///         mesajı dönen sınıf.
    ///     EN: A class that encapsulates error information and returns an 
    ///         error code with a descriptive error message.
    /// </summary>
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
    /// <summary>
    ///     TR: Konsol üzerinden kullanıcı girişi alan arayüz.
    ///     EN: Interface that receives user input via console
    /// </summary>
    public interface IInputHandler
    {
        int GetInteger(string message);
        string GetChoice(string message, params string[] allowedChoices);
        (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage);
    }
    /// <summary>
    ///     TR: Konsol üzerinden kullanıcı bilgilendirmesi veren arayüz.
    ///     EN: Interface that provides user information via console.
    /// </summary>
    public interface IOutputHandler
    {
        void PrintSeparator();
        void PrintIntroMessage();
    }
    /// <summary>
    ///     TR: En büyük ortak bölenin iki farklı yöntem kullanılarak 
    ///         hesaplandığı bir arayüz: Asal çarpanlara ayırma algoritması 
    ///         veya Öklid algoritması.
    ///     EN: An interface where the greatest common divisor (GCD) is 
    ///         calculated using two different methods: Prime factorization
    ///         algorithm or the Euclidean algorithm.
    /// </summary>
    public interface IGCDCalculator
    {
        string Name { get; }
        int CalculateGCD(int x, int y);
    }
    /// <summary>
    ///     TR: En büyük ortak bölen bulma algoritmasının seçiminde 
    ///         yararlanılan arayüz.
    ///     EN: The interface used in selecting the greatest common divisor
    ///         (GCD) algortihm.
    /// </summary>
    public interface ICalculatorFactory
    {
        IGCDCalculator Create(string choice);
    }
    /// <summary>
    ///     TR: Ana fonksiyonun çalıştırma işlemlerinin bünyesinde toplanarak
    ///         yükünün hafifletilmesinde kullanılan arayüz.
    ///     EN: An interface in which the execution tasks of the main 
    ///         function are aggregated, facilitating the reduction of its 
    ///         workload.
    /// </summary>
    public interface ICalculationRunner
    {
        void Run();
    }
    /// <summary>
    ///     TR: Asal çarpanlara ayırma algoritmasında kullanılmak üzere, bir
    ///         sonraki asal çarpanın bulunması ve asal çarpan kontrolü
    ///         işlevlerini içeren arayüz.
    ///     EN: An interface in which the functionalities of finding the next
    ///         prime factor and checking whether a number is a prime factor
    ///         are included for use in the prime factorization algorithm.
    /// </summary>
    public interface IPrimeProvider
    {
        bool IsPrime(int n);
        int GetNextPrime(int currentPrime);
    }
    public interface IValidator
    {
        (int? x, int? y) EnsureValidInputs(int x, int y);
    }
    public interface IErrorHandler
    {
        void HandleError(Error error);
    }
    public interface ITableBuilder
    {
        void CreateNewTable();
        void ClearTable();
        GcdStep StartStep(int stepNumber, int x, int y, int divisor);
        void CompleteStep(GcdStep step, int xAfter, int yAfter, string operation, bool isCommon);
        void GetSteps();
        void PrintSchoolTable();
    }
    public class SimplePrimeProvider : IPrimeProvider
    {
        public bool IsPrime(int candidatePrime)
        {
            if (candidatePrime < 2) return false;
            if (candidatePrime == 2) return true;
            if (candidatePrime % 2 == 0) return false;

            int limit = (int)Math.Floor(Math.Sqrt(candidatePrime));

            for (int i = 3; i <= limit; i += 2)
            {
                if (candidatePrime % i == 0) return false;
            }
            return true;
        }
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
    public class BasicInputValidator : IValidator
    {
        IErrorHandler errorHandler = new ConsoleErrorHandler();
        private const int MaxThreshold = 10000000;
        public (int? x, int? y) EnsureValidInputs(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);

            if (x == 0 && y == 0)
            {
                errorHandler.HandleError(new Error(ErrorCode.InvalidFormat, "Both numbers cannot be zero. GCD(0,0) is mathematically undefined."));

                throw new ArgumentException("GCD(0,0) is undefined");
            }
            else if (x == 0 && y != 0) return (y, null);
            else if (x != 0 && y == 0) return (x, null);
            if (x > MaxThreshold || y > MaxThreshold)
            {
                errorHandler.HandleError(new Error(ErrorCode.OutOfRange, $"One or both numbers exceed the allowed maximum ({MaxThreshold}). Please try with smaller numbers."));

                throw new OverflowException("Input too large for chosen GCD algorithm");
            }
            return (x, y);
        }
    }
    public class GcdCalculatorFactory : ICalculatorFactory
    {
        public IGCDCalculator Create(string choice)
        {
            switch (choice)
            {
                case "1":
                    return new PrimeFactorizationGCDCalculator();
                case "2":
                    return new EuclideanModuloGCDCalculator();
                default:
                    return new PrimeFactorizationGCDCalculator();
            }
        }
    }
    public class CalculationRunner : ICalculationRunner
    {
        private const string RetryFirstMessage = "Enter the first number again";
        private const string RetrySecondMessage = "Enter the second number again";
        private const string ChoiceMessage = "Which option would you like to use(1 / 2): ";
        private const string FirstNumberMessage = "Enter the first number to calculate the GCD...";
        private const string SecondNumberMessage = "Enter the second number to calculate the GCD...";
        private const string ResultMessage = "The GCD result is: ";

        private readonly IErrorHandler _errorHandler;
        private readonly IInputHandler _input;
        private readonly IOutputHandler _output;
        private readonly IValidator _validator;
        private readonly ICalculatorFactory _calculatorFactory;

        public CalculationRunner(IInputHandler input, IOutputHandler output, IValidator validator, ICalculatorFactory calculatorFactory, IErrorHandler errorHandler)
        {

            var dependencies = new Dictionary<string, object>
            {
                { nameof(input), input },
                { nameof(output), output },
                { nameof(validator), validator },
                { nameof(calculatorFactory), calculatorFactory },
                { nameof(errorHandler), errorHandler }
            };

            foreach (var dependency in dependencies)
            {
                if (dependency.Value == null)
                {
                    errorHandler.HandleError(new Error(ErrorCode.NullDependency, $"{dependency.Key} cannot be null."));

                    throw new InvalidOperationException($"Cannot start Calculation without {dependency.Key}");
                }
            }

            _input = input;
            _output = output;
            _validator = validator;
            _calculatorFactory = calculatorFactory;
            _errorHandler = errorHandler;
        }

        public void Run()
        {
            _output.PrintIntroMessage();

            var choice = _input.GetChoice(ChoiceMessage, "1", "2");
            IGCDCalculator calculator = _calculatorFactory.Create(choice);
            var (x, y) = _input.GetNumberFromUser(FirstNumberMessage, SecondNumberMessage);
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
                    (x, y) = _input.GetNumberFromUser(RetryFirstMessage, RetrySecondMessage);
                }
                catch (OverflowException e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.OutOfRange, e.Message));
                    (x, y) = _input.GetNumberFromUser(RetryFirstMessage, RetrySecondMessage);
                }
                catch (Exception e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.Unexpected, e.Message));
                    (x, y) = _input.GetNumberFromUser(RetryFirstMessage, RetrySecondMessage);
                }
            }

            var validX = validated.Item1;
            var validY = validated.Item2;

            int gcdResult;
            if (validY == null) gcdResult = (int)validX;
            else gcdResult = calculator.CalculateGCD(validX.Value, validY.Value);

            _output.PrintSeparator();
            Console.WriteLine(ResultMessage + gcdResult);
            _output.PrintSeparator();
        }
    }
    public class PrimeFactorizationGCDCalculator : IGCDCalculator
    {
        public string Name { get; } = "Prime Factorization GCD Calculator";
        public int CalculateGCD(int x, int y)
        {
            IPrimeProvider primeProvider = new SimplePrimeProvider();
            ITableBuilder tableBuilder = new PrimeFactorizationTableBuilder();

            tableBuilder.CreateNewTable();

            int gcdResult = 1;
            int factorCandidate = 2;
            int stepCounter = 1;
            while (x != 1 || y != 1)
            {
                var step = tableBuilder.StartStep(stepCounter, x, y, factorCandidate);

                if (x % factorCandidate == 0 && y % factorCandidate == 0)
                {
                    gcdResult *= factorCandidate;
                    x /= factorCandidate;
                    y /= factorCandidate;
                    tableBuilder.CompleteStep(step, x, y, $"Her iki sayı {factorCandidate} ile bölündü (ortak bölen)", true);
                }
                else if (x % factorCandidate == 0 && y % factorCandidate != 0)
                {
                    x /= factorCandidate;
                    tableBuilder.CompleteStep(step, x, y, $"Sadece ilk sayı {factorCandidate} ile bölündü", false);
                }
                else if (x % factorCandidate != 0 && y % factorCandidate == 0)
                {
                    y /= factorCandidate;
                    tableBuilder.CompleteStep(step, x, y, $"Sadece ikinci sayı {factorCandidate} ile bölündü", false);
                }
                else
                {
                    int nextPrime = primeProvider.GetNextPrime(factorCandidate);
                    tableBuilder.CompleteStep(step, x, y, $"{factorCandidate} asal sayısı hiçbirini bölemiyor -> sonraki asal denenecek: {nextPrime}", false);
                    factorCandidate = nextPrime;
                }
                stepCounter++;
            }

            tableBuilder.PrintSchoolTable();

            return gcdResult;
        }
    }
    public class EuclideanModuloGCDCalculator : IGCDCalculator
    {
        public string Name { get; } = "Euclidean Modulo GCD Calculator";
        public int CalculateGCD(int x, int y)
        {
            ITableBuilder tableBuilder = new EuclideanTableBuilder();
            tableBuilder.CreateNewTable();

            int stepCounter = 1;

            while (y != 0)
            {
                int remainder = x % y;

                var step = tableBuilder.StartStep(stepCounter, x, y, y);
                tableBuilder.CompleteStep(step, remainder, y, $"{x} % {y} = {remainder}", false);

                x = y;
                y = remainder;
                stepCounter++;
            }

            tableBuilder.PrintSchoolTable();

            return x;
        }
    }
    public class ConsoleInputHandler : IInputHandler
    {
        private readonly IErrorHandler _errorHandler;
        public ConsoleInputHandler(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }
        public int GetInteger(string message)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        _errorHandler.HandleError(new Error(ErrorCode.EmptyInput, "Input was empty, please enter a number."));
                        continue;
                    }

                    input = input.Trim();
                    int value;

                    if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    {
                        return value;
                    }
                    if (long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
                    {
                        _errorHandler.HandleError(new Error(ErrorCode.OutOfRange, "Value out of range, enter a smaller or larger number"));
                    }
                    else
                    {
                        _errorHandler.HandleError(new Error(ErrorCode.InvalidFormat, $"'{input}' is not a valid integer"));
                    }
                }
                catch (Exception e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.Unexpected, "An unexpected error occurred, please try again."));
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
    public class ConsoleOutputHandler : IOutputHandler
    {
        private const string Separator = "=========================================";
        public void PrintSeparator() => Console.WriteLine(Separator);
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
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void HandleError(Error error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error.ToString());
            Console.ResetColor();
        }
    }
    public class PrimeFactorizationTableBuilder : ITableBuilder
    {
        private List<GcdStep> _steps;
        private int _stepCounter;

        public void CreateNewTable()
        {
            _steps = new List<GcdStep>();
            _stepCounter = 1;
        }
        public void ClearTable()
        {
            if (_steps != null)
            {
                _steps.Clear();
            }
            _stepCounter = 1;
        }
        public GcdStep StartStep(int stepNumber, int x, int y, int divisor)
        {
            return new GcdStep
            {
                StepNumber = stepNumber,
                XBefore = x,
                YBefore = y,
                TestedDivisor = divisor
            };
        }
        public void CompleteStep(GcdStep step, int xAfter, int yAfter, string operation, bool isCommonFactor)
        {
            step.XAfter = xAfter;
            step.YAfter = yAfter;
            step.Operation = operation;
            step.IsCommonFactor = isCommonFactor;
            step.Remainder = null;

            _steps.Add(step);
            _stepCounter++;
        }
        public void GetSteps()
        {
            foreach (var step in _steps)
            {
                Console.WriteLine($"{step.StepNumber}. Adım: {step.Operation} | " +
                                  $"X: {step.XBefore}->{step.XAfter}, " +
                                  $"Y: {step.YBefore}->{step.YAfter}, " +
                                  $"Bölen: {step.TestedDivisor}, Ortak mı? {step.IsCommonFactor}");
            }
        }
        public void PrintSchoolTable()
        {
            IOutputHandler _output = new ConsoleOutputHandler();
            _output.PrintSeparator();

            if (_steps == null || !_steps.Any())
            {
                Console.WriteLine("(No steps were recorded)");
                _output.PrintSeparator();
                return;
            }

            foreach (var step in _steps)
            {
                bool anyDivided = (step.XBefore != step.XAfter) || (step.YBefore != step.YAfter);

                if (anyDivided)
                {
                    Console.WriteLine(
                        $"{step.XBefore.ToString().PadLeft(4)} " +
                        $"{step.YBefore.ToString().PadLeft(4)} " +
                        $"| {step.TestedDivisor}"
                    );
                }
            }
            var last = _steps.Last();
            Console.WriteLine($"{last.XAfter.ToString().PadLeft(4)} {last.YAfter.ToString().PadLeft(4)}");
            _output.PrintSeparator();
        }
    }
    public class EuclideanTableBuilder : ITableBuilder
    {
        private List<GcdStep> _steps;
        private int _stepCounter;

        public void CreateNewTable()
        {
            _steps = new List<GcdStep>();
            _stepCounter = 1;
        }
        public void ClearTable()
        {
            if (_steps != null)
            {
                _steps.Clear();
            }
            _stepCounter = 1;
        }
        public GcdStep StartStep(int stepNumber, int x, int y, int divisor)
        {
            if(_steps == null)
            {
                _steps = new List<GcdStep>();
            }
            return new GcdStep
            {
                StepNumber = stepNumber,
                XBefore = x,
                YBefore = y,
                TestedDivisor = divisor
            };
        }
        public void CompleteStep(GcdStep step, int xAfter, int yAfter, string operation, bool isCommonFactor)
        {
            if (_steps == null)
            {
                _steps = new List<GcdStep>();
            }
            step.XAfter = xAfter;
            step.YAfter = yAfter;
            step.Operation = operation;
            step.IsCommonFactor = isCommonFactor;
            step.Remainder = xAfter;

            _steps.Add(step);
            _stepCounter++;
        }
        public void GetSteps()
        {
            foreach (var step in _steps)
            {
                Console.WriteLine($"{step.StepNumber}. Adım: {step.Operation} | " +
                                  $"X: {step.XBefore}, " +
                                  $"Y: {step.YBefore}, " +
                                  $"Kalan: {step.Remainder}");
            }
        }
        public void PrintSchoolTable()
        {
            IOutputHandler _output = new ConsoleOutputHandler();
            _output.PrintSeparator();

            if (_steps == null || !_steps.Any())
            {
                Console.WriteLine("(No steps were recorded)");
                _output.PrintSeparator();
                return;
            }

            foreach (var step in _steps)
            {
                Console.WriteLine(
                    $"{step.XBefore.ToString().PadLeft(4)} " +
                    $"{step.YBefore.ToString().PadLeft(4)} " +
                    $"| remainder = {step.Remainder}"
                );
            }
            _output.PrintSeparator();
        }
    }
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
