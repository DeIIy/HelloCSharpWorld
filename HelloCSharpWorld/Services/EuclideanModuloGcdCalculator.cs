using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;

namespace HelloCSharpWorld.Services
{
    public sealed class EuclideanModuloGcdCalculator : IGcdCalculator
    {
        public string Name { get { return "Euclidean Modulo GCD Calculator"; } }

        public int CalculateGcd(int x, int y)
        {
            ITableBuilder builder = new EuclideanTableBuilder();
            ITablePrinter printer = new EuclideanTablePrinter();

            builder.CreateNew();

            int step = 1;

            while (y != 0)
            {
                int remainder = x % y;

                var s = GcdStep.Start(step, x, y, y, $"{x} % {y} = {remainder}")
                    .WithAfter(remainder, y, $"{x} % {y} = {remainder}", false, remainder);

                builder.AddStep(s);

                x = y;
                y = remainder;
                step++;
            }
            printer.Print(builder.GetSteps());
            return x;
        }
    }
}
