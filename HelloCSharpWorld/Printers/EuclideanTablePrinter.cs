using System;
using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Printers
{
    /// <summary>
    ///     TR: Öklid algoritması adımlarını tablo formatında yazdırmak için kullanılan sınıf.
    ///     EN: Class used to print the steps of the Euclidean algorithm in a table format.
    /// </summary>
    public sealed class EuclideanTablePrinter : ITablePrinter
    {
        /// <summary>
        ///     TR: Verilen EBOB adımlarını tablo olarak yazdırır.
        ///     EN: Prints the provided GCD steps as a table.
        /// </summary>
        /// <param name="steps">
        ///     TR: Yazdırılacak EBOB adımlarının okunabilir listesi.
        ///     EN: Read-only list of GCD steps to print.
        /// </param>
        public void Print(IReadOnlyList<GcdStep> steps)
        {
            IOutputHandler output = new UI.ConsoleOutputHandler();
            output.PrintSeparator();

            if (steps == null || steps.Count == 0)
            {
                output.PrintLine("(No steps were recorded)");
                output.PrintSeparator();
                return;
            }

            foreach (var s in steps)
            {
                Console.WriteLine(
                    s.XBefore.ToString().PadLeft(6) + " " +
                    s.YBefore.ToString().PadLeft(6) + " | remainder = " +
                    (s.Remainder.HasValue ? s.Remainder.Value.ToString() : "-"));
            }
            output.PrintSeparator();
        }
    }
}
