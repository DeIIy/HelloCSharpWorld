namespace HelloCSharpWorld.Interfaces
{
    public interface IPrimeProvider
    {
        bool IsPrime(int n);
        int GetNextPrime(int currentPrime);
    }
}
