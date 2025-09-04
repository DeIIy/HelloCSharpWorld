using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    public sealed class GcdCalculatorFactory : ICalculatorFactory
    {
        public IGcdCalculator Create(string choice)
        {
            switch (choice)
            {
                case "1": return new PrimeFactorizationGcdCalculator();
                case "2": return new EuclideanModuloGcdCalculator();
                default: return new PrimeFactorizationGcdCalculator();
            }
        }
    }
}
