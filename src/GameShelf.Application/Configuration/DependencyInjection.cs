using GameShelf.Application.Interfaces;
using GameShelf.Application.Mapping;
using GameShelf.Application.Services;
using GameShelf.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameShelf.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper 
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Hash de mot de passe + Authentification
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            #region Services
            // services.AddScoped<IAuthService, AuthService>();
            // services.AddScoped<IJwtService, JwtService>();
            #endregion

            return services;
        }
    }
}
