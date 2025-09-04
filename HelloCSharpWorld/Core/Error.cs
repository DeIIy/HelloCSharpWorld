namespace HelloCSharpWorld.Core
{
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
