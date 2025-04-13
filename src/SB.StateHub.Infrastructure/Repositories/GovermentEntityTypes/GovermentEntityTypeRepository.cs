using SB.StateHub.Domain.Entities.GovermentEntityTypes;
using SB.StateHub.Domain.Repositories.GovermentEntityTypes;
using SB.StateHub.Infrastructure.Contexts;
using SB.StateHub.Infrastructure.Repositories.Bases;

namespace SB.StateHub.Infrastructure.Repositories.GovermentEntityTypes
{
    public class GovermentEntityTypeRepository : BaseRepository<GovermentEntityType>, IGovermentEntityTypeRepository
    {
        public GovermentEntityTypeRepository(MainDbContext context) : base(context)
        {
            
        }
    }
}