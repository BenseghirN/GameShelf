using GameShelf.Application.Interfaces;
using GameShelf.Application.Mapping;
using GameShelf.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameShelf.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper 
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddHttpContextAccessor();

            #region Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserSynchronizationService, UserSynchronizationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<IUserProposalService, UserProposalService>();
            services.AddScoped<ILibraryService, LibraryService>();
            #endregion

            return services;
        }
    }
}
