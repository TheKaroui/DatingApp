using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Core.Extensions;
using DatingApp.API.DataProviders.Domain;
using DatingApp.API.DataProviders.Domain.SeedData;
using DatingApp.API.DataProviders.Repository.RepoContracts;
using DatingApp.API.DataProviders.Repository.RepoImplementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DatingAPP.API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) 
        {
            var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            // .RequireRole("Admin", "SuperUser")
                            .Build();

            services.AddDbContext<DatingAppContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddCors();
            services.AddMvc(options => options.Filters.Add(new AuthorizeFilter(policy)))
                            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                            .AddJsonOptions(opt => {
                                opt.SerializerSettings.ReferenceLoopHandling = 
                                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                            });
            


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IValueRepository, ValueRepository>();
            services.AddTransient<Seed>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, Seed seeder) {

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } 
            else {
                // app.UseHsts();
                app.UseExceptionHandler
                ( 
                    builder => 
                    builder.Run
                    (
                        async context => 
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null) {
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message);
                            }
                        }
                    )
                );
            }

            // app.UseHttpsRedirection();
            //seeder.SeedUsers();

            
            //enabling cross origin CORS
            app.UseCors (x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            
            app.UseMvc ();
        }
    }
}