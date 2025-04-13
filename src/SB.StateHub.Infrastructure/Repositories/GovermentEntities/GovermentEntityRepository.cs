using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SB.StateHub.Domain.Entities.GovermentEntities;
using SB.StateHub.Domain.Repositories.GovermentEntities;
using SB.StateHub.Infrastructure.Contexts;
using SB.StateHub.Infrastructure.Repositories.Bases;

namespace SB.StateHub.Infrastructure.Repositories.GovermentEntities
{
    public class GovermentEntityRepository : BaseRepository<GovermentEntity>, IGovermentEntityRepository
    {
        public GovermentEntityRepository(MainDbContext context) : base(context)
        {

        }

        public override Task<GovermentEntity?> GetByIdAsync(int? id)
        {
            return _context
                .GovermentEntities
                .Include(gen => gen.EntityType)
                .Where(t => t.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override IQueryable<GovermentEntity> GetAll()
        {
            IQueryable<GovermentEntity> entities = _context
                .GovermentEntities
                .Include(gen => gen.EntityType)
                .Where(t => t.IsDeleted == false);

            return entities;
        }

        public override IQueryable<GovermentEntity> GetAll(Expression<Func<GovermentEntity, bool>> predicate)
        {
            IQueryable<GovermentEntity> entities = _context
                .GovermentEntities
                .Include(gen => gen.EntityType)
                .Where(t => t.IsDeleted == false)
                .Where(predicate);

            return entities;
        }

        public override IQueryable<GovermentEntity> GetAllPaged(int pageNumber, int pageSize, Expression<Func<GovermentEntity, bool>> predicate)
        {
            IQueryable<GovermentEntity> entities = _context
                .GovermentEntities
                .Include(gen => gen.EntityType)
                .Where(t => t.IsDeleted == false)
                .Where(predicate)
                .Skip((pageNumber * pageSize) - pageSize)
                .Take(pageSize);

            return entities;
        }
    }
}