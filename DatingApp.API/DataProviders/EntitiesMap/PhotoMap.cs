using DatingApp.API.DataProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.API.DataProviders.EntitiesMap {
    public class PhotoMap : IEntityTypeConfiguration<Photo> {

        public void Configure (EntityTypeBuilder<Photo> builder) {

            builder.ToTable ("t_Photo");
            builder.HasKey (ph => ph.Id);
            builder.Property (ph => ph.Url);
            builder.Property (ph => ph.Description);
            builder.Property (ph => ph.DateAdded);
            builder.Property (ph => ph.IsMain);
            builder.Property (ph => ph.PublicId);
        }

    }
}