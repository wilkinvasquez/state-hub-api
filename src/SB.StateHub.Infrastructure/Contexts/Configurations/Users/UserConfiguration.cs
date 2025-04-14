using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.StateHub.Domain.Entities.Users;

namespace SB.StateHub.Infrastructure.Contexts.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users")
                .HasKey(usr => usr.Id);

            builder
                .Property(usr => usr.Name)
                .IsRequired();

            builder
                .Property(usr => usr.Lastname)
                .IsRequired();

            builder
                .Property(usr => usr.Username)
                .IsRequired();

            builder
                .Property(usr => usr.Password)
                .IsRequired();
        }
    }
}