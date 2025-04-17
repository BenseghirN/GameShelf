using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class LibraryService(IGameShelfDbContext dbContext, IMapper mapper, IUserSynchronizationService userSynchronizationService) : ILibraryService
    {
        public async Task<UserGameDto> AddGameToLibraryAsync(Guid gameId, GameStatus statut, int? note, string? imagePersoPath, CancellationToken cancellationToken = default)
        {
            User? user = await GetUserAsync(cancellationToken);
            user.AddGame(gameId, statut, note);
            UserGame userGame = user.UserGames.First(ug => ug.GameId == gameId);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserGameDto>(userGame);
        }

        public async Task<List<UserGameDto>> GetUserLibraryAsync(CancellationToken cancellationToken = default)
        {
            User user = await GetUserAsync(cancellationToken);
            return mapper.Map<List<UserGameDto>>(user.UserGames);
        }

        public async Task RemoveGameFromLibraryAsync(Guid gameId, CancellationToken cancellationToken = default)
        {
            User? user = await GetUserAsync(cancellationToken);
            user.RemoveGame(gameId);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<StatsDto> GetLibraryStatsAsync(CancellationToken cancellationToken = default)
        {
            User? user = await GetUserAsync(cancellationToken);
            return new StatsDto
            {
                NbTotalGames = user.UserGames.Count,
                NbOngoingGames = user.UserGames.Count(ug => ug.Statut == GameStatus.EnCours)
            };

        }

        public async Task<UserGameDto?> GetUserGameByGameIdAsync(Guid gameId, CancellationToken cancellationToken = default)
        {
            User? user = await GetUserAsync(cancellationToken);
            UserGame? userGame = user.UserGames.FirstOrDefault(ug => ug.GameId == gameId);
            return userGame != null
                ? mapper.Map<UserGameDto>(userGame)
                : null;
        }

        public async Task<UserGameDto> UpdateGameStatusAsync(Guid gameId, GameStatus statut, int? note, CancellationToken cancellationToken = default)
        {
            User? user = await GetUserAsync(cancellationToken);
            user.UpdateGameStatus(gameId, statut, note);
            await dbContext.SaveChangesAsync(cancellationToken);
            UserGame updated = user.UserGames.First(ug => ug.GameId == gameId);
            return mapper.Map<UserGameDto>(updated);
        }

        private async Task<User> GetUserAsync(CancellationToken cancellationToken)
        {
            User? currentUser = await userSynchronizationService.EnsureUserExistsAsync() ?? throw new UnauthorizedAccessException();
            User? user = await dbContext.Users
                            .Include(u => u.UserGames)
                                .ThenInclude(ug => ug.Game)
                                    .ThenInclude(g => g.GameTags)
                                        .ThenInclude(gt => gt.Tag)
                            .Include(u => u.UserGames)
                                .ThenInclude(ug => ug.Game)
                                    .ThenInclude(g => g.GamePlatforms)
                                        .ThenInclude(gp => gp.Platform)
                            .FirstOrDefaultAsync(u => u.Id == currentUser.Id, cancellationToken);

            return user ?? throw new Exception("Utilisateur introuvable.");
        }
    }
}