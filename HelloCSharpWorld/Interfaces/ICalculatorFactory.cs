namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: En büyük ortak bölen bulma algoritmasının seçiminde 
    ///         yararlanılan arayüz.
    ///     EN: The interface used in selecting the greatest common divisor
    ///         (GCD) algortihm.
    /// </summary>
    public interface ICalculatorFactory
    {
        IGcdCalculator Create(string choice);
    }
}
