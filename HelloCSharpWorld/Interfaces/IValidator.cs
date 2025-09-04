namespace HelloCSharpWorld.Interfaces
{
    public interface IValidator
    {
        (int? x, int? y) EnsureValidInputs(int x, int y);
    }
}
