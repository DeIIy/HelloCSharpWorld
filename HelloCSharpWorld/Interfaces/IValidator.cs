namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: En büyük ortak bölen (EBOB) hesaplamalarında özel durumların
    ///         ele alındığı arayüz.
    ///     EN: An interface in which special cases are processed in the
    ///         computation of the greatest common divisor (GCD).
    /// </summary>
    public interface IValidator
    {
        (int? x, int? y) EnsureValidInputs(int x, int y);
    }
}
