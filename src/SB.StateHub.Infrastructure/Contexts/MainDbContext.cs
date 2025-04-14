using Microsoft.EntityFrameworkCore;
using SB.StateHub.Domain.Entities.GovermentEntities;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;
using SB.StateHub.Domain.Entities.Users;
using SB.StateHub.Infrastructure.Contexts.Configurations.EntityTypes;
using SB.StateHub.Infrastructure.Contexts.Configurations.GovermentEntities;
using SB.StateHub.Infrastructure.Contexts.Configurations.Users;

namespace SB.StateHub.Infrastructure.Contexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<GovermentEntityType> GovermentEntityTypes { get; set; }
        public DbSet<GovermentEntity> GovermentEntities { get; set; }
        public DbSet<User> Users { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovermentEntityTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovermentEntityConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}