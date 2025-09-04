using System;
using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    /// <summary>
    ///     TR: EBOB hesaplamalarının yürütülmesinde kullanılan sınıf.
    ///     EN: Class in which the execution of GCD calculations is handled.
    /// </summary>
    public sealed class CalculationRunner : ICalculationRunner
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IInputHandler _input;
        private readonly IOutputHandler _output;
        private readonly IValidator _validator;
        private readonly ICalculatorFactory _factory;

        /// <summary>       
        ///     TR: Gerekli bağımlılıkların doğrulandığı ve sınıfa atandığı 
        ///         kurucu metot.
        ///     EN: Constructor in which required dependencies are validated and
        ///         assigned to the class.
        /// </summary>
        /// <param name="input">        
        ///     TR: Kullanıcıdan giriş alınmasında kullanılan arayüz
        ///     EN: Interface used for receiving user input.
        /// </param>
        /// <param name="output">        
        ///     TR: Kullanıcı girişlerinin doğrulanmasında kullanılan arayüz.
        ///     EN: Interface used for validating user inputs.
        /// </param>
        /// <param name="validator">        
        ///     TR: Kullanıcı girişlerinin doğrulanmasında kullanılan arayüz.
        ///     EN: Interface used for validating user inputs.
        /// </param>
        /// <param name="calculatorFactory">        
        ///     TR: Seçime göre EBOB hesaplayıcısının üretilmesinde kullanılan
        ///         arayüz.
        ///     EN: Interface used for creating a GCD calculator based on 
        ///         selection.
        /// </param>
        /// <param name="errorHandler">       
        ///     TR: Hataların yönetilmesinde kullanılan arayüz.
        ///     EN: Interface used for handling errors.
        /// </param>
        /// <exception cref="InvalidOperationException">       
        ///     TR: Herhangi bir bağımlılık null olduğunda fırlatılır.
        ///     EN: Thrown when any dependency is null.
        /// </exception>
        public CalculationRunner(
            IInputHandler input,
            IOutputHandler output,
            IValidator validator,
            ICalculatorFactory factory,
            IErrorHandler errorHandler)
        {
            var deps = new Dictionary<string, object>
            {
                { nameof(input), input },
                { nameof(output), output },
                { nameof(validator), validator },
                { nameof(factory), factory },
                { nameof(errorHandler), errorHandler }
            };

            foreach (var kv in deps)
            {
                if (kv.Value == null)
                {
                    errorHandler.HandleError(new Core.Error(ErrorCode.NullDependency, string.Format(Messages.NullDependency, kv.Key)));
                    throw new InvalidOperationException(string.Format(Messages.CannotStart, kv.Key));
                }
            }

            _input = input;
            _output = output;
            _validator = validator;
            _factory = factory;
            _errorHandler = errorHandler;
        }

        /// <summary>
        ///     TR: Kullanıcıdan giriş alınıp doğrulandıktan sonra seçilen 
        ///         algoritmayla EBOB'un hesaplandığı fonksiyon.
        ///     EN: Function in which user inputs are gathered, validated, and 
        ///         the GCD calculated using the chosen algorithm.
        /// </summary>
        /// <remarks>
        ///     TR: Fonksiyon çalışırken hatalar ele alınır ve kullanıcı yeniden
        ///         giriş yapmaya yönlendirilir.
        ///     EN: While executing, errors are handled and the user is prompted
        ///         to re-enter inputs.
        /// </remarks>
        public void Run()
        {
            PrintIntro();

            string choice = _input.GetChoice(Messages.ChoicePrompt, "1", "2");
            IGcdCalculator calc = _factory.Create(choice);

            var (x, y) = _input.GetNumberFromUser(Messages.FirstNumberPrompt, Messages.SecondNumberPrompt);
            var validated = ValidateLoop(x, y);

            var result = Compute(calc, validated);
            PrintResult(result);
        }

        /// <summary>
        ///     TR: Uygulama başlatılırken kullanıcıya gösterilen başlık ve bilgi mesajlarını yazdırır.
        ///     EN: Prints the application title, introduction, and tips when starting the app.
        /// </summary>
        private void PrintIntro()
        {
            _output.PrintSeparator();
            _output.PrintLine(Messages.AppTitle);
            _output.PrintSeparator();
            _output.PrintLine(Messages.Intro);
            _output.PrintSeparator();
            _output.PrintLine(Messages.Tip);
            _output.PrintSeparator();
        }

        /// <summary>
        ///     TR: Kullanıcıdan alınan sayıları doğrular, hata varsa tekrar girdi alınmasını sağlar.
        ///     EN: Validates user-provided numbers and prompts for re-entry if an error occurs.
        /// </summary>
        private (int? x, int? y) ValidateLoop(int x, int y)
        {
            while (true)
            {
                try
                {
                    return _validator.EnsureValidInputs(x, y);
                }
                catch (ValidationException ve)
                {
                    _errorHandler.HandleError(new Error(ve.Code, ve.Message));
                }
                catch (Exception e)
                {
                    _errorHandler.HandleError(new Error(ErrorCode.Unexpected, e.Message));
                }

                var retry = _input.GetNumberFromUser(Messages.RetryFirstNumber, Messages.RetrySecondNumber);
                x = retry.x;
                y = retry.y;
            }
        }

        /// <summary>
        ///     TR: Doğrulanmış sayılarla seçilen algoritmayı kullanarak EBOB'u hesaplar.
        ///     EN: Computes the GCD using the chosen algorithm and validated numbers.
        /// </summary>
        /// <param name="calculator">
        ///     TR: EBOB hesaplamasında kullanılacak algoritmayı sağlayan nesne.
        ///     EN: The object providing the algorithm to calculate the GCD.
        /// </param>
        /// <param name="validated">
        ///     TR: Doğrulanmış kullanıcı sayıları.
        ///     EN: The validated user numbers.
        /// </param>
        /// <returns>
        ///     TR: Hesaplanan EBOB değeri.
        ///     EN: The calculated GCD value.
        /// </returns>
        private int Compute(IGcdCalculator calculator, (int? x, int? y) validated)
        {
            var vx = validated.x;
            var vy = validated.y;

            if (vy == null) return vx.GetValueOrDefault();
            return calculator.CalculateGcd(vx.GetValueOrDefault(), vy.GetValueOrDefault());
        }

        /// <summary>
        ///     TR: Hesaplanan EBOB sonucunu kullanıcıya yazdırır.
        ///     EN: Prints the calculated GCD result to the user.
        /// </summary>
        /// <param name="gcd">
        ///     TR: Yazdırılacak EBOB değeri.
        ///     EN: The GCD value to print.
        /// </param>
        private void PrintResult(int gcd)
        {
            _output.PrintSeparator();
            _output.PrintLine(Messages.ResultPrefix + gcd);
            _output.PrintSeparator();
        }
    }
}
