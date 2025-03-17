namespace WinLab4.Exceptions;

public abstract class AppException : Exception
{
    protected AppException()
    {
        Type = GetType().Name;
    }

    public string Type { get; }

    public object? Details { get; protected set; }
}
