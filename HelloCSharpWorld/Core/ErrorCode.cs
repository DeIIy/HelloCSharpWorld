namespace HelloCSharpWorld.Core
{
    /// <summary>
    ///     TR: Uygulama boyunca kullanılacak: 'ArgumentOutOfRangeException,
    ///         ArgumentNullException, FormatException, OverflowException, 
    ///         NullReferenceExpection, Exception' hataları temsil eden enum
    ///         sabitleri.
    ///     EN: Exceptions to be used throughout the application: 'OverflowException,
    ///         ArgumentOutOfRangeException, ArgumentNullException, FormatException,
    ///         NullReferenceException, Exception' enumerated constants.
    /// </summary>
    public enum ErrorCode
    {
        InvalidChoice,
        EmptyInput,
        InvalidFormat,
        OutOfRange,
        NullDependency,
        Unexpected
    }
}
