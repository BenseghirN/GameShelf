using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IUserService
    {
        Task AddGameToUserAsync(Guid gameId, CancellationToken cancellationToken = default);
    }
}