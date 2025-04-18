using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IPlatformService
    {
        Task<List<PlatformDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PlatformDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PlatformDto> CreateAsync(NewPlatformDto dto, CancellationToken cancellationToken = default);
        Task<PlatformDto> UpdateAsync(Guid id, NewPlatformDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}