using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Builders
{
    /// <summary>
    ///     TR: EBOB hesaplamaları sırasında adım adım işlem tablosunu oluşturan sınıf.
    ///     EN: Class responsible for building a step-by-step table during GCD calculations.
    /// </summary>
    public sealed class BaseTableBuilder : ITableBuilder
    {
        private readonly List<GcdStep> _steps = new List<GcdStep>();

        /// <summary>
        ///     TR: Mevcut tabloyu temizler.
        ///     EN: Clears the current table.
        /// </summary>
        public void CreateNew()
        {
            _steps.Clear();
        }

        /// <summary>
        ///     TR: Tabloya yeni bir adım ekler.
        ///     EN: Adds a new step to the table.
        /// </summary>
        /// <param name="step">
        ///     TR: Tabloya eklenecek EBOB hesaplama adımı.
        ///     EN: The GCD calculation step to add to the table.
        /// </param>
        public void AddStep(GcdStep step)
        {
            _steps.Add(step);
        }

        /// <summary>
        ///     TR: Mevcut tablodaki tüm adımları okunabilir bir liste olarak döndürür.
        ///     EN: Returns all steps in the current table as a read-only list.
        /// </summary>
        /// <returns>
        ///     TR: Okunabilir EBOB adımları listesi.
        ///     EN: Read-only list of GCD steps.
        /// </returns>
        public IReadOnlyList<GcdStep> GetSteps()
        {
            return _steps.AsReadOnly();
        }
    }
}
