namespace BackwardsCompatibility;

public sealed class CursedException : Exception
{
    public CursedException(string msg) : base(msg) { }
}