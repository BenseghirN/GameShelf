using GameShelf.Application.Interfaces;
using GameShelf.Application.Repositories;
using GameShelf.Domain.Entities;
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

            // AutoMapper (scan toutes les classes Profile de l'assembly Infrastructure)
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            // Hash de mot de passe
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

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
