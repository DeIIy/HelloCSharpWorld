namespace HelloCSharpWorld.Interfaces
{
    public interface IGcdCalculator
    {
        string Name { get; }
        int CalculateGcd(int x, int y);
    }
}
