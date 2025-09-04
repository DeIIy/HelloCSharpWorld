using System.Collections.Generic;
using HelloCSharpWorld.Core;

namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: EBOB hesaplama adımlarını tablo formatında yazdırmak için kullanılan arayüz.
    ///     EN: Interface used to print GCD calculation steps in a table format.
    /// </summary>
    public interface ITablePrinter
    {
        void Print(IReadOnlyList<GcdStep> steps);
    }
}
