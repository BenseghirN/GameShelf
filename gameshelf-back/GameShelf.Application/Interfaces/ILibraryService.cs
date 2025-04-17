using GameShelf.Application.DTOs;
using GameShelf.Domain.Enums;

namespace GameShelf.Application.Interfaces
{

    public interface ILibraryService
    {
        Task<List<UserGameDto>> GetUserLibraryAsync(CancellationToken cancellationToken = default);
        Task<UserGameDto> AddGameToLibraryAsync(Guid gameId, GameStatus statut, int? note, string? imagePersoPath, CancellationToken cancellationToken = default);
        Task<UserGameDto> UpdateGameStatusAsync(Guid gameId, GameStatus statut, int? note, CancellationToken cancellationToken = default);
        Task RemoveGameFromLibraryAsync(Guid gameId, CancellationToken cancellationToken = default);
        Task<StatsDto> GetLibraryStatsAsync(CancellationToken cancellationToken = default);
        Task<UserGameDto?> GetUserGameByGameIdAsync(Guid gameId, CancellationToken cancellationToken = default);
    }
}