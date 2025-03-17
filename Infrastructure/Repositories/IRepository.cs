using System.Linq.Expressions;
using WinLab4.Models;

namespace WinLab4.Infrastructure.Repositories;

public interface IRepository<T> where T : Entity
{
    Task Add(T model, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? modifiers = null, CancellationToken cancellationToken = default);

    Task<T> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? modifiers, CancellationToken cancellationToken = default);

    Task<T> Get(Guid id, Func<IQueryable<T>, IQueryable<T>>? modifiers = null, CancellationToken cancellationToken = default);

    Task Update(T model, CancellationToken cancellationToken = default);

    Task Delete(T model, CancellationToken cancellationToken = default);
}
