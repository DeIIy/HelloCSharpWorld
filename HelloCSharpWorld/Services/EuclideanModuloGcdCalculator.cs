using HelloCSharpWorld.Builders;
using HelloCSharpWorld.Core;
using HelloCSharpWorld.Interfaces;
using HelloCSharpWorld.Printers;

namespace HelloCSharpWorld.Services
{
    /// <summary>
    ///     TR: Mod alma yöntemiyle Öklid algoritmasını kullanarak EBOB 
    ///         hesaplayan sınıf.
    ///     EN: Class that calculates the GCD using the Euclidean algorithm
    ///         (modulo method).
    /// </summary>
    public sealed class EuclideanModuloGcdCalculator : IGcdCalculator
    {
        /// <summary>
        ///     TR: Hesaplayıcıya ait görünen adı.
        ///     EN: Display name of the calculator.
        /// </summary>
        public string Name { get { return "Euclidean Modulo GCD Calculator"; } }

        /// <summary>
        ///     TR: İki sayının Öklid algoritması (mod yöntemi) ile EBOB'unu hesaplar.
        ///     EN: Calculates the GCD of two numbers using the Euclidean algorithm 
        ///     (modulo method).
        /// </summary>
        /// <param name="x">
        ///     TR: İlk sayı değişkeni.
        ///     EN: The first number variable.
        /// </param>
        /// <param name="y">
        ///     TR: İkinci sayı değişkeni.
        ///     EN: The second number variable. 
        /// </param>
        /// <returns>
        ///     TR: Hesaplanan EBOB değeri.
        ///     EN: The calculated GCD value.
        /// </returns>
        public int CalculateGcd(int x, int y)
        {
            ITableBuilder builder = new BaseTableBuilder();
            ITablePrinter printer = new EuclideanTablePrinter();

            builder.CreateNew();

            // İşlem numaralandırma sayacı
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
