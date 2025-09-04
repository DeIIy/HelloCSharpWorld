namespace HelloCSharpWorld.Interfaces
{
    /// <summary>
    ///     TR: Konsol üzerinden kullanıcı girişi alan arayüz.
    ///     EN: Interface that receives user input via console
    /// </summary>
    public interface IInputHandler
    {
        int GetInteger(string message);
        string GetChoice(string message, params string[] allowedChoices);
        (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage);
    }
}
