using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SB.StateHub.Domain.Entities.Bases;
using SB.StateHub.Domain.Repositories.Bases;
using SB.StateHub.Infrastructure.Contexts;

namespace SB.StateHub.Infrastructure.Repositories.Bases
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly MainDbContext _context;

        public BaseRepository(MainDbContext context)
        {
            _context = context;
        }

        public Task<T?> GetByIdAsync(int? id)
        {
            return _context.Set<T>().Where(t => t.IsDeleted == false).FirstOrDefaultAsync(t => t.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> entities = _context.Set<T>().Where(t => t.IsDeleted == false);

            return entities;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = _context.Set<T>().Where(t => t.IsDeleted == false).Where(predicate);

            return entities;
        }

        public IQueryable<T> GetAllPaged(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = _context
                .Set<T>()
                .Where(t => t.IsDeleted == false)
                .Where(predicate)
                .Skip((pageNumber * pageSize) - pageSize)
                .Take(pageSize);

            return entities;
        }

        public async Task<T> CreateOrUpdateAsync(T entity)
        {
            if (entity.Id == null)
            {
                entity.CreationTime = DateTime.Now;
                await _context.Set<T>().AddAsync(entity);
            }
            else
            {
                entity.LastModificationTime = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
            }

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            T? entity = await _context.Set<T>().Where(t => t.IsDeleted == false).FirstOrDefaultAsync(t => t.Id == id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletionTime = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            T? entity = await _context.Set<T>().Where(t => t.IsDeleted == false).FirstOrDefaultAsync(predicate);

            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.DeletionTime = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}