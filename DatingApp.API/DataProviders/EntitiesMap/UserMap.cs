using DatingApp.API.DataProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.API.DataProviders.EntitiesMap {
    public class UserMap : IEntityTypeConfiguration<User> {
        public void Configure (EntityTypeBuilder<User> builder) {

            builder.ToTable ("t_User");
            builder.HasKey (u => u.Id);
            builder.Property (u => u.Username);
            builder.HasIndex (u => u.Username).IsUnique();
            builder.Property (u => u.PasswordHash);
            builder.Property (u => u.PasswordSalt);

            builder.Property (u => u.Gender);
            builder.Property (u => u.DateOfBirth);
            builder.Property (u => u.KnownAs);
            builder.Property (u => u.Created);
            builder.Property (u => u.LastActive);
            builder.Property (u => u.Introduction);
            builder.Property (u => u.LookingFor);
            builder.Property (u => u.Interests);
            builder.Property (u => u.City);
            builder.Property (u => u.City);
            builder.Property (u => u.Country);

            // HasMany(pc => pc.Departements).WithMany(dp => dp.ContactsDuDepartement)
            //                 .Map(dppc => dppc.ToTable("t_DepartmentContactPoint", SchemaName)
            //                 .MapLeftKey("ContactPointId_fk")
            //                 .MapRightKey("DepartmentId_fk"));

            builder.HasMany(u => u.Photos)
            .WithOne(ph => ph.User).IsRequired()
            .HasConstraintName("UserId_fk")
            .OnDelete(DeleteBehavior.Cascade);

            // HasMany(pc => pc.Tickets)
            //     .WithRequired(ticket => ticket.ContactPoint)
            //     .Map(ptc => ptc.MapKey("ContactPointId_fk"));
        }
    }
}