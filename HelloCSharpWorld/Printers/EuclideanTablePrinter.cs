using System;
using System.Collections.Generic;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Printers
{
    public sealed class EuclideanTablePrinter : ITablePrinter
    {
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
