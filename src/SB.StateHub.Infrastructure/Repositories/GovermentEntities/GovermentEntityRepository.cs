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
    }
}