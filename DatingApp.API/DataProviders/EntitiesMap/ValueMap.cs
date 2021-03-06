using DatingApp.API.DataProviders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatingApp.API.DataProviders.EntitiesMap {
    public class ValueMap : IEntityTypeConfiguration<Value> {
        public void Configure (EntityTypeBuilder<Value> builder) {
            
            builder.ToTable("t_Value");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedOnAdd();
            builder.Property(v => v.Name).IsRequired ().HasMaxLength (100);
            builder.Property (v => v.Description).IsRequired().HasMaxLength(10);
            builder.Property (v => v.SqlServerSqLiteTogetherInMigrations).IsRequired().HasMaxLength(10);
        }
    }
}