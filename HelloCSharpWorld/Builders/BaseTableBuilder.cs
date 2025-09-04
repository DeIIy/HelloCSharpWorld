using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Builders
{
    public sealed class BaseTableBuilder : ITableBuilder
    {
        private readonly List<GcdStep> _steps = new List<GcdStep>();

        public void CreateNew()
        {
            _steps.Clear();
        }

        public void AddStep(GcdStep step)
        {
            _steps.Add(step);
        }

        public IReadOnlyList<GcdStep> GetSteps()
        {
            return _steps.AsReadOnly();
        }
    }
}
