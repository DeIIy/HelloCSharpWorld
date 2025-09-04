namespace HelloCSharpWorld.Interfaces
{
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
}
