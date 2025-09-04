namespace HelloCSharpWorld.Core
{
    /// <summary>
    ///     TR: Uygulamada kullanılan tüm mesaj ve sabit metinleri içerir.
    ///     EN: Contains all messages and constant text used in the application.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        ///     TR: Konsolda ayırıcı çizgi.
        ///     EN: Separator line for console output.
        /// </summary>
        public const string Separator = "=========================================";

        /// <summary>
        ///     TR: Uygulama başlığı.
        ///     EN: Application title.
        /// </summary>
        public const string AppTitle = "GCD Calculator";

        /// <summary>
        ///     TR: Uygulama tanıtım metni.
        ///     EN: Introduction text for the application.
        /// </summary>
        public const string Intro =
            "Welcome! This tool finds the Greatest Common Divisor (GCD).\n\n" +
            "Calculation Techniques:\n" +
            " 1) Prime Factorization\n" +
            " 2) Euclidean Algorithm";

        /// <summary>
        ///     TR: Kullanıcıya ipucu metni.
        ///     EN: Tip message shown to the user.
        /// </summary>
        public const string Tip = "Tip: Both techniques yield the same GCD, via different routes.";

        /// <summary>
        ///     TR: Kullanıcıdan algoritma seçimi isteme mesajı.
        ///     EN: Prompt message for user to select calculation technique.
        /// </summary>
        public const string ChoicePrompt = "Which option would you like to use (1 / 2): ";

        /// <summary>
        ///     TR: Kullanıcıdan birinci sayıyı girmesini isteme mesajı.
        ///     EN: Prompt message to enter the first number.
        /// </summary>
        public const string FirstNumberPrompt = "Enter the first number to calculate the GCD:";

        /// <summary>
        ///     TR: Kullanıcıdan ikinci sayıyı girmesini isteme mesajı.
        ///     EN: Prompt message to enter the second number.
        /// </summary>
        public const string SecondNumberPrompt = "Enter the second number to calculate the GCD:";

        /// <summary>
        ///     TR: Kullanıcıdan birinci sayıyı yeniden girmesini isteme mesajı.
        ///     EN: Prompt message to re-enter the first number.
        /// </summary>
        public const string RetryFirstNumber = "Enter the first number again:";

        /// <summary>
        ///     TR: Kullanıcıdan ikinci sayıyı yeniden girmesini isteme mesajı.
        ///     EN: Prompt message to re-enter the second number.
        /// </summary>
        public const string RetrySecondNumber = "Enter the second number again:";

        /// <summary>
        ///     TR: EBOB sonucunu kullanıcıya yazdırırken önek mesajı.
        ///     EN: Prefix message when printing the GCD result.
        /// </summary>
        public const string ResultPrefix = "The GCD result is: ";

        /// <summary>
        ///     TR: Boş giriş hatası mesajı.
        ///     EN: Error message for empty input.
        /// </summary>
        public const string ErrorEmptyInput = "Input was empty, please enter a number.";

        /// <summary>
        ///     TR: Geçersiz seçim hatası mesajı.
        ///     EN: Error message for invalid choice.
        /// </summary>
        public const string ErrorInvalidChoice = "Invalid choice. Options: {0}";

        /// <summary>
        ///     TR: Geçersiz tam sayı hatası mesajı.
        ///     EN: Error message when input is not a valid integer.
        /// </summary>
        public const string ErrorInvalidInteger = "'{0}' is not a valid integer.";

        /// <summary>
        ///     TR: Sayı aralığı dışına çıkıldığında gösterilen hata mesajı.
        ///     EN: Error message when value is out of allowed range.
        /// </summary>
        public const string ErrorOutOfRange = "Value out of range, enter a smaller or larger number.";

        /// <summary>
        ///     TR: Beklenmeyen hata durumunda gösterilen mesaj.
        ///     EN: Message displayed for unexpected errors.
        /// </summary>
        public const string ErrorUnexpected = "An unexpected error occurred, please try again.";

        /// <summary>
        ///     TR: Her iki sayının sıfır olması durumunda gösterilen hata mesajı.
        ///     EN: Error message when both numbers are zero (GCD undefined).
        /// </summary>
        public const string ErrorBothZero = "Both numbers cannot be zero. GCD(0,0) is mathematically undefined.";

        /// <summary>
        ///     TR: Sayılardan biri veya ikisi izin verilen maksimum değeri aştığında gösterilen mesaj.
        ///     EN: Message displayed when one or both numbers exceed the allowed maximum.
        /// </summary>
        public const string ErrorTooLarge = "One or both numbers exceed the allowed maximum ({0}). Please try with smaller numbers.";

        /// <summary>
        ///     TR: Null bağımlılık hatası mesajı.
        ///     EN: Message displayed when a dependency is null.
        /// </summary>
        public const string NullDependency = "{0} cannot be null.";

        /// <summary>
        ///     TR: Hesaplamaya başlanamayacağı durum mesajı.
        ///     EN: Message displayed when calculation cannot start due to missing dependency.
        /// </summary>
        public const string CannotStart = "Cannot start Calculation without {0}.";
    }
}
