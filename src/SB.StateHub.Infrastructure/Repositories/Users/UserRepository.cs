using SB.StateHub.Domain.Entities.Users;
using SB.StateHub.Domain.Repositories.Users;
using SB.StateHub.Infrastructure.Contexts;
using SB.StateHub.Infrastructure.Repositories.Bases;

namespace SB.StateHub.Infrastructure.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MainDbContext context) : base(context)
        {

        }
    }
}