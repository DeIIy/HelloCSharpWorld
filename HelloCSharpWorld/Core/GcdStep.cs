namespace HelloCSharpWorld.Core
{
    /// <summary>
    ///     TR: EBOB hesaplamasındaki (asal çarpan ve öklid hesaplamalarıyla)
    ///         işlem değerlerini (basamak numarası, önceki sayı değerleri, 
    ///         sonraki sayı değerleri, kısa açıklamalar, asal çarpan veya
    ///         öklide özel değerler) saklayan işlem basamakları sınıfı.
    ///     EN: A class that stores the computation steps of the GCD 
    ///         calculation (using prime factorization and Euclidean methods),
    ///         including step number, previous values, next values, short
    ///         descriptions, and special values specific to prime factorization
    ///         or the Euclidean algorithm.
    /// </summary>
    public sealed class GcdStep
    {
        public int StepNumber { get; }
        public int XBefore { get; }
        public int YBefore { get; }
        public int XAfter { get; }
        public int YAfter { get; }
        public int? TestedDivisor { get; }
        public bool? IsCommonFactor { get; }
        public int? Remainder { get; }
        public string Operation { get; }

        public GcdStep(
            int stepNumber,
            int xBefore,
            int yBefore,
            int xAfter,
            int yAfter,
            int? testedDivisor,
            bool? ısCommonFactor,
            int? remainder,
            string operation)
        {
            StepNumber = stepNumber;
            XBefore = xBefore;
            YBefore = yBefore;
            XAfter = xAfter;
            YAfter = yAfter;
            TestedDivisor = testedDivisor;
            IsCommonFactor = ısCommonFactor;
            Remainder = remainder;
            Operation = operation ?? string.Empty;
        }

        public GcdStep WithAfter(
            int xAfter,
            int yAfter,
            string operation,
            bool? isCommon,
            int? remainder)
        {
            return new GcdStep(
                StepNumber,
                XBefore,
                YBefore,
                xAfter,
                yAfter,
                TestedDivisor,
                isCommon,
                remainder,
                operation);
        }

        public static GcdStep Start(
            int stepNumber,
            int x,
            int y,
            int? divisor,
            string operation = null)
        {
            return new GcdStep(
                stepNumber,
                x,
                y,
                x,
                y,
                divisor,
                null,
                null,
                operation ?? string.Empty);
        }
    }
}
