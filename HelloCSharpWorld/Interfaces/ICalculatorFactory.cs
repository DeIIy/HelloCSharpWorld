namespace HelloCSharpWorld.Interfaces
{
    public interface ICalculatorFactory
    {
        IGcdCalculator Create(string choice);
    }
}
