using GameShelf.Application.DTOs;
using GameShelf.Domain.Entities;

namespace GameShelf.Application.Interfaces
{
    public interface IUserSynchronizationService
    {
        Task<User> EnsureUserExistsAsync(CancellationToken cancellationToken = default);
        Task<UserDto> GetCurrentUserInfosAsync(CancellationToken cancellationToken = default);
    }
}