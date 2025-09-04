using System.Collections.Generic;
using HelloCSharpWorld.Core;

namespace HelloCSharpWorld.Interfaces
{
    public interface ITablePrinter
    {
        void Print(IReadOnlyList<GcdStep> steps);
    }
}
