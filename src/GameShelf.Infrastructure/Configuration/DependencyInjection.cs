using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameShelf.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper (scan toutes les classes Profile de l'assembly Infrastructure)
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            // DbContext → PostgreSQL
            services.AddDbContext<GameShelfDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Services métier
            // services.AddScoped<IAuthService, AuthService>();
            // services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IGameService, GameService>();

            // Repositories (si tu les as séparés)
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }
}
