using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IGameService
    {
        Task<List<GameDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<GameDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<GameDto> CreateAsync(GameDto gameDto, CancellationToken cancellationToken = default);
        Task<GameDto> UpdateAsync(Guid id, GameDto gameDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}