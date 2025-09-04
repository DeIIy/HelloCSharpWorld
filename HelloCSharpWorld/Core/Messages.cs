namespace HelloCSharpWorld.Core
{
    public static class Messages
    {
        public const string Separator = "=========================================";
        public const string AppTitle = "GCD Calculator";
        public const string Intro =
            "Welcome! This tool finds the Greatest Common Divisor (GCD).\n\n" +
            "Calculation Techniques:\n" +
            " 1) Prime Factorization\n" +
            " 2) Euclidean Algorithm";

        public const string Tip = "Tip: Both techniques yield the same GCD, via different routes.";
        public const string ChoicePrompt = "Which option would you like to use (1 / 2): ";
        public const string FirstNumberPrompt = "Enter the first number to calculate the GCD:";
        public const string SecondNumberPrompt = "Enter the second number to calculate the GCD:";
        public const string RetryFirstNumber = "Enter the first number again:";
        public const string RetrySecondNumber = "Enter the second number again:";
        public const string ResultPrefix = "The GCD result is: ";

        public const string ErrorEmptyInput = "Input was empty, please enter a number.";
        public const string ErrorInvalidChoice = "Invalid choice. Options: {0}";
        public const string ErrorInvalidInteger = "'{0}' is not a valid integer.";
        public const string ErrorOutOfRange = "Value out of range, enter a smaller or larger number.";
        public const string ErrorUnexpected = "An unexpected error occurred, please try again.";
        public const string ErrorBothZero = "Both numbers cannot be zero. GCD(0,0) is mathematically undefined.";
        public const string ErrorTooLarge = "One or both numbers exceed the allowed maximum ({0}). Please try with smaller numbers.";
        public const string NullDependency = "{0} cannot be null.";
        public const string CannotStart = "Cannot start Calculation without {0}.";
    }
}
