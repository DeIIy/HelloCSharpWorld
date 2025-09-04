namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: Konsol üzerinden kullanıcı bilgilendirmesi veren arayüz.
    ///     EN: Interface that provides user information via console.
    /// </summary>
    public interface IOutputHandler
    {
        void PrintSeparator();
        void PrintIntroMessage();
        void PrintLine(string message);
    }
}
