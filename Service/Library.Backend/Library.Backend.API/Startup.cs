using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Backend.Business;
using Library.Backend.Contracts.Business;
using Library.Backend.Contracts.Repository;
using Library.Backend.Entities.Models;
using Library.Backend.Entities.Refetentials;
using Library.Backend.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Library.Backend.API
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            DependencySettings(services);
            DependencyRepositories(services);
            DependencyBusiness(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Library Service API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Autorization Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
            });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(int.Parse(Configuration["JWT:TimeMin"]))
                };
            });
        }
        
        private void DependencyRepositories(IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:LibraryDB"]));

            services.AddTransient<IGenericRepository<Author>, AuthorDB>();
            services.AddTransient<IGenericRepository<Book>, BookDB>();
            services.AddTransient<IGenericRepository<Category>, CategoryDB>();
        }

        private void DependencySettings(IServiceCollection services)
        {
            services.Configure<InfoJWT>(opt => Configuration.GetSection("JWT").Bind(opt));
        }

        
        private void DependencyBusiness(IServiceCollection services)
        {
            services.AddTransient<ISecurityBL, SecurityBL>();
            services.AddTransient<IAuthorBL, AuthorBL>();
            services.AddTransient<ICategoryBL, CategoryBL>();
            services.AddTransient<IBookBL, BookBL>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Service API");
                c.RoutePrefix = string.Empty;
            });

            loggerFactory.AddSerilog();
            app.UseMvc();
        }
    }
}
