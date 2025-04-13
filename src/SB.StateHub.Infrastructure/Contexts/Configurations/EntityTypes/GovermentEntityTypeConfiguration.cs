using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.StateHub.Domain.Entities.GovermentEntityTypes;

namespace SB.StateHub.Infrastructure.Contexts.Configurations.EntityTypes
{
    public class GovermentEntityTypeConfiguration : IEntityTypeConfiguration<GovermentEntityType>
    {
        public void Configure(EntityTypeBuilder<GovermentEntityType> builder)
        {
            builder
                .ToTable("GovermentEntityTypes")
                .HasKey(get => get.Id);

            builder
                .Property(get => get.Name)
                .IsRequired();
        }
    }
}