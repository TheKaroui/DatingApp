using DatingApp.API.DataProviders.Entities;
using DatingApp.API.DataProviders.EntitiesMap;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.DataProviders.Domain {
    public class DatingAppContext : DbContext {
        public DatingAppContext (DbContextOptions<DatingAppContext> options) : base (options) {

            Database.EnsureCreatedAsync();
        }

        protected override void OnModelCreating (ModelBuilder builder) {

            builder.HasDefaultSchema("sch_dta");
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new ValueMap());
            base.OnModelCreating(builder);
        }
    }
}