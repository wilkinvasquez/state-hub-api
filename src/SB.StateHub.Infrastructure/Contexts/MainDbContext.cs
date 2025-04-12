using Microsoft.EntityFrameworkCore;
using SB.StateHub.Domain.Entities.GovermentEntities;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;
using SB.StateHub.Infrastructure.Contexts.Configurations.EntityTypes;
using SB.StateHub.Infrastructure.Contexts.Configurations.GovermentEntities;

namespace SB.StateHub.Infrastructure.Contexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<GovermentEntityType> GovermentEntityTypes { get; set; }
        public DbSet<GovermentEntity> GovermentEntities { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovermentEntityTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GovermentEntityConfiguration).Assembly);
        }
    }
}