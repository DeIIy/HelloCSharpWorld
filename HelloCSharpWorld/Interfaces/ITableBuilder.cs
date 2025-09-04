using System.Collections.Generic;
using HelloCSharpWorld.Core;

namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: GcdStep sınıfına EBOB hesaplaması verilerinin kaydedildiği,
    ///         tablo haline getirildiği ve konsol ekranına yazıldığı arayüz.
    ///     EN: An interface in which GcdStep class stores GCD calculation 
    ///         data, organizes it into a table, and outputs the table to 
    ///         the console
    /// </summary>
    public interface ITableBuilder
    {
        void CreateNew();
        void AddStep(GcdStep step);
        IReadOnlyList<GcdStep> GetSteps();
    }
}
