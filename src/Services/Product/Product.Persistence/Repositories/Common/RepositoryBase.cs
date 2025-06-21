using Microsoft.EntityFrameworkCore;
using Product.Domain.Common;
using Product.Application.Interfaces.Base;
using Product.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Product.Persistence.Repositories.Common;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
    protected readonly ProductDbContext DbContext;

    public RepositoryBase(ProductDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.Set<T>().AsNoTracking() : DbContext.Set<T>();
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        var query = !trackChanges ? DbContext.Set<T>().AsNoTracking() : DbContext.Set<T>();
        return await query.Where(expression).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await DbContext.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity) => await DbContext.Set<T>().AddAsync(entity);

    public void Update(T entity) => DbContext.Set<T>().Update(entity);

    public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
}