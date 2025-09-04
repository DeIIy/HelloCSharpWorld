namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: En büyük ortak bölenin iki farklı yöntem kullanılarak 
    ///         hesaplandığı bir arayüz: Asal çarpanlara ayırma algoritması 
    ///         veya Öklid algoritması.
    ///     EN: An interface where the greatest common divisor (GCD) is 
    ///         calculated using two different methods: Prime factorization
    ///         algorithm or the Euclidean algorithm.
    /// </summary>
    public interface IGcdCalculator
    {
        string Name { get; }
        int CalculateGcd(int x, int y);
    }
}
