using DatingApp.API.DataProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.API.DataProviders.EntitiesMap {
    public class UserMap : IEntityTypeConfiguration<User> {
        public void Configure (EntityTypeBuilder<User> builder) {

            builder.ToTable("t_User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.LastName).HasMaxLength (100);
            builder.Property(u => u.FirstName).HasMaxLength (50);
            builder.Property(u => u.Email).HasMaxLength (250);
            builder.Property(u => u.PasswordHash);
            builder.Property(u => u.PasswordSalt);
        }
    }
}