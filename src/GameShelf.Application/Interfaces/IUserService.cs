using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IUserService
    {
        // Task AddGameToUserAsync(Guid gameId, CancellationToken cancellationToken = default);
        Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task PromoteToAdminAsync(Guid id, CancellationToken cancellationToken = default);
        Task DemoteToUserAsync(Guid id, CancellationToken cancellationToken = default);
    }
}