using System.Linq.Expressions;

namespace SB.StateHub.Domain.Repositories.Bases
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int? id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllPaged(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate);
        Task<T> CreateOrUpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}