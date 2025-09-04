using System.Collections.Generic;
using HelloCSharpWorld.Core;

namespace HelloCSharpWorld.Interfaces
{
    public interface ITableBuilder
    {
        void CreateNew();
        void AddStep(GcdStep step);
        IReadOnlyList<GcdStep> GetSteps();
    }
}
