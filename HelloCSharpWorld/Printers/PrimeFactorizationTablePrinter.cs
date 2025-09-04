using System;
using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Printers
{
    /// <summary>
    ///     TR: Asal çarpan yöntemiyle yapılan EBOB hesaplama adımlarını tablo formatında yazdırmak için kullanılan sınıf.
    ///     EN: Class used to print the steps of GCD calculation using the prime factorization method in a table format.
    /// </summary>
    internal class PrimeFactorizationTablePrinter : ITablePrinter
    {
        /// <summary>
        ///     TR: Verilen EBOB adımlarını tablo olarak yazdırır.
        ///     EN: Prints the provided GCD steps as a table.
        /// </summary>
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
                var anyDiv = (s.XBefore != s.XAfter) || (s.YBefore != s.YAfter);

                if (anyDiv)
                {
                    Console.WriteLine(
                        s.XBefore.ToString().PadLeft(6) + " " +
                        s.YBefore.ToString().PadLeft(6) + " | " +
                        (s.TestedDivisor.HasValue ? s.TestedDivisor.Value.ToString() : "-"));
                }
            }

            var last = steps[steps.Count - 1];
            Console.WriteLine(last.XAfter.ToString().PadLeft(6) + " " + last.YAfter.ToString().PadLeft(6));

            output.PrintSeparator();
        }
    }
}
