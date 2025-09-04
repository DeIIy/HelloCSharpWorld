using System;
using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    public sealed class CalculationRunner : ICalculationRunner
    {
        private readonly IErrorHandler _errorHandler;
        private readonly IInputHandler _input;
        private readonly IOutputHandler _output;
        private readonly IValidator _validator;
        private readonly ICalculatorFactory _factory;

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

        private int Compute(IGcdCalculator calculator, (int? x, int? y) validated)
        {
            var vx = validated.x;
            var vy = validated.y;

            if (vy == null) return vx.GetValueOrDefault();
            return calculator.CalculateGcd(vx.GetValueOrDefault(), vy.GetValueOrDefault());
        }

        private void PrintResult(int gcd)
        {
            _output.PrintSeparator();
            _output.PrintLine(Messages.ResultPrefix + gcd);
            _output.PrintSeparator();
        }
    }
}
