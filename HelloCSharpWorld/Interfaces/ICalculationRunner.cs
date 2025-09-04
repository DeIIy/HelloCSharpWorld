namespace HelloCSharpWorld.Interfaces
{
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
}
