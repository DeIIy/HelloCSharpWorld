namespace HelloCSharpWorld.Interfaces
{
    public interface IOutputHandler
    {
        void PrintSeparator();
        void PrintIntroMessage();
        void PrintLine(string message);
    }
}
