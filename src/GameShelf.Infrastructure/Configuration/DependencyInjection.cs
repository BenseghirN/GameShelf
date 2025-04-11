using GameShelf.Application.Interfaces;
using GameShelf.Application.Repositories;
using GameShelf.Domain.Entities;
using GameShelf.Infrastructure.Mapping;
using GameShelf.Infrastructure.Repositories;
using GameShelf.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameShelf.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext → PostgreSQL
            services.AddDbContext<GameShelfDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // AutoMapper 
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Hash de mot de passe + Authentification
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAuthenticationConfiguration(configuration);

            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IGameRepository, GameRepository>();
            #endregion

            #region Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            #endregion

            return services;
        }
    }
}
