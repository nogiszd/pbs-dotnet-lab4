namespace WinLab4.Exceptions;

public sealed class ResourceNotFoundException : AppException
{
    public ResourceNotFoundException() : this(string.Empty, string.Empty)
    {
    }

    public ResourceNotFoundException(string name) : this(name, string.Empty)
    {
        Details = new ResourceNotFoundExceptionDetails
        {
            Name = name,
        };
    }

    public ResourceNotFoundException(string name, Guid id) : this(name, id.ToString())
    {
    }

    public ResourceNotFoundException(string name, string? id)
    {
        Details = new ResourceNotFoundExceptionDetails
        {
            Name = name,
            Id = id,
        };
    }
}

public sealed class ResourceNotFoundExceptionDetails
{
    public string Name { get; init; } = null!;

    public string? Id { get; init; }
}