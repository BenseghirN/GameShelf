using AutoMapper;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;

namespace GameShelf.Application.Services
{
    public class UserService(IGameShelfDbContext dbContext, IUserSynchronizationService userSynchronizationService) : IUserService
    {
        public async Task AddGameToUserAsync(Guid gameId, CancellationToken cancellationToken = default)
        {
            User user = await userSynchronizationService.EnsureUserExistsAsync(cancellationToken);
            user.AddGame(gameId, "A voir", null);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}