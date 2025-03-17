using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WinLab4.Exceptions;
using WinLab4.Infrastructure.Persistence;
using WinLab4.Models;

namespace WinLab4.Infrastructure.Repositories;

public sealed class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : Entity
{
    public AppDbContext Context = dbContext;

    public async Task Add(T model, CancellationToken cancellationToken = default)
    {
        await Context.AddAsync(model, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(T model, CancellationToken cancellationToken = default)
    {
        Context.Remove(model);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? modifiers = null, CancellationToken cancellationToken = default)
    {
        return await GetCollection(modifiers).AnyAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? modifiers = null, CancellationToken cancellationToken = default)
    {
        return await GetCollection(modifiers).Where(predicate).ToListAsync(cancellationToken);   
    }

    public async Task<T> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? modifiers, CancellationToken cancellationToken = default)
    {
        return await GetCollection(modifiers).SingleOrDefaultAsync(predicate, cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(T).Name);
    }

    public async Task<T> Get(Guid id, Func<IQueryable<T>, IQueryable<T>>? modifiers = null, CancellationToken cancellationToken = default)
    {
        return await GetCollection(modifiers).SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(T).Name, id);
    }

    public async Task<T?> TryGet(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? modifiers = null, CancellationToken cancellationToken = default)
    {
        return await GetCollection(modifiers).Where(predicate).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task Update(T model, CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<T> GetCollection(Func<IQueryable<T>, IQueryable<T>>? modifiers = null)
    {
        var collection = Context.Set<T>().AsQueryable();

        return modifiers?.Invoke(collection) ?? collection;
    }
}
