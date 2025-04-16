using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UserDto> PromoteToAdminAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UserDto> DemoteToUserAsync(Guid id, CancellationToken cancellationToken = default);
    }
}