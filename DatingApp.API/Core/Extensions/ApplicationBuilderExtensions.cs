using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.API.Core.Extensions
{
     public static class IApplicationBuilderExtensions
    {
        public static void SyncMigrations<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DbContext>();
                context.Database.Migrate();
            }
        }

        // public static void SeedIfCreated(this IApplicationBuilder app, IServiceCollection services)
        // {
            
        //     var context = services.GetService<DataProviders.Domain.DatingAppContext>()
        //     if (context.Database.GetService<IRelationalDatabaseCreator>().Exists())
        //         context.Database.Migrate();
        //     // var context = app.ApplicationServices.GetService<DataProviders.Domain.DatingAppContext>();

        //     // if (!context.Database.EnsureCreated())
        //     //     context.Database.Migrate();
        // }

        // public static void UpdateDatabase(this IApplicationBuilder app)
        // {
        //     using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //     {
        //         using (var context = serviceScope.ServiceProvider.GetService<DataProviders.Domain.DatingAppContext>())
        //         {
        //             if (context.Database.EnsureCreated())
        //                 context.Database.Migrate();
                    
        //         }
        //     }
        // }
        
    }
}