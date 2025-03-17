namespace WinLab4.Models;

public interface IEntity : IIdentifiable
{
}

public abstract class Entity : IEntity
{
    public Guid Id { get; protected init; } = Guid.NewGuid();
}
