namespace HelloCSharpWorld.Interfaces
{
    public interface IInputHandler
    {
        int GetInteger(string message);
        string GetChoice(string message, params string[] allowedChoices);
        (int x, int y) GetNumberFromUser(string firstMessage, string secondMessage);
    }
}
