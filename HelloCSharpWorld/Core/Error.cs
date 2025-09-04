namespace HelloCSharpWorld.Core
{
    /// <summary>
    ///     TR: Hata bilgilerini kapsülleyen hata kodu ve açıklayıcı hata 
    ///         mesajı dönen sınıf.
    ///     EN: A class that encapsulates error information and returns an 
    ///         error code with a descriptive error message.
    /// </summary>
    public sealed class Error
    {
        public ErrorCode Code { get; }
        public string Message { get; }

        public Error(ErrorCode code, string message)
        {
            Code = code;
            Message = message ?? string.Empty;
        }

        public override string ToString()
        {
            return $"[Error][{Code}]: {Message}";
        }
    }
}
