using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    /// <summary>
    ///     TR: Kullanıcının seçimine göre uygun EBOB hesaplayıcısının
    ///         oluşturulmasında kullanılan sınıf.
    ///     EN: Class in which an appropriate GCD calculator is created based
    ///         on user selection
    /// </summary>
    public sealed class GcdCalculatorFactory : ICalculatorFactory
    {
        /// <summary>        
        ///     TR: Kullanıcıdan alınan seçime göre EBOB hesaplayıcısının 
        ///         oluşturulduğu fonksiyon.
        ///     EN: Function in which a GCD calculator is instantiated according
        ///         to the given user choice.
        /// </summary>
        /// <param name="choice"> 
        ///     TR: Kullanıcının seçimini temsil eden değer. (1: Asal çarpanlara
        ///         ayırma algoritması, 2: Öklid algoritması)
        ///     EN: The value representing the user's choice. (1: Prime 
        ///         factorization algorithm, 2: Euclidean algorithm)
        /// </param>
        /// <returns>        
        ///     TR: Seçime uygun EBOB hesaplayıcısı döndürülür. Geçersiz seçim
        ///         durumunda varsayılan olarak asal çarpanlara ayırma algoritması
        ///         döndürülür.
        ///     EN: Returns the GCD calculator corresponding to the choice. If the
        ///         choice. If the choice is invalid, the prime factorization 
        ///         algorithm is returned by default.
        /// </returns>
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
