using System;

namespace HelloCSharpWorld.Core
{
    public sealed class ValidationException : Exception
    {
        public ErrorCode Code { get; }

        public ValidationException(ErrorCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
