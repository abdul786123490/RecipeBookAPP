using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.Application.Interfaces.Repositories;
using RecipeBook.Application.Interfaces.Services;
using RecipeBook.Infrastructure;
using RecipeBook.Infrastructure.Repositories;
using RecipeBook.Infrastructure;
using System.Text;
using RecipeBook.Application.Services.Implementations;

namespace RecipeBook.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // ✅ 1. Configure DbContext
            services.AddDbContext<RecipeBookDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("dbcs")));

            // ✅ 2. Register Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();


            // ✅ 3. Configure JWT Authentication (for Swagger & API security)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

            return services;
        }
    }
}
