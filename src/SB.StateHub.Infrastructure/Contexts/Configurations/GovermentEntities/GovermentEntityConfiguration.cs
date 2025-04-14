using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.StateHub.Domain.Entities.GovermentEntities;

namespace SB.StateHub.Infrastructure.Contexts.Configurations.GovermentEntities
{
    public class GovermentEntityConfiguration : IEntityTypeConfiguration<GovermentEntity>
    {
        public void Configure(EntityTypeBuilder<GovermentEntity> builder)
        {
            builder
                .ToTable("GovermentEntities")
                .HasKey(gen => gen.Id);

            builder
                .HasOne(gen => gen.EntityType);

            builder
                .Property(gen => gen.Name)
                .IsRequired();

            builder
                .Property(gen => gen.Description)
                .IsRequired();
        }
    }
}