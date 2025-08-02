using Product.Domain.Common;
using System.Linq.Expressions;

namespace Product.Application.Interfaces.Base
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync(bool trackChanges = false);
        Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges = false);
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}