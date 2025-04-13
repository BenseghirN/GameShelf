using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class GameService(IGameShelfDbContext dbContext, IMapper mapper) : IGameService
    {
        public async Task<GameDto> CreateAsync(GameDto gameDto, CancellationToken cancellationToken = default)
        {
            Game game = mapper.Map<Game>(gameDto);
            game.Id = Guid.NewGuid();

            // TODO: ADD METHOD INTO GAME ENTITY TO CREATE GAME
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<GameDto>(game);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Game? game = await dbContext.Games.FindAsync(new object[] { id }, cancellationToken);
            if (game == null) throw new Exception("Jeu introuvable");

            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<GameDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<Game> games = await dbContext.Games
                .Include(g => g.GameTags).ThenInclude(gt => gt.Tag)
                .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                .ToListAsync(cancellationToken);

            return mapper.Map<List<GameDto>>(games);
        }

        public async Task<GameDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Game? game = await dbContext.Games
                .Include(g => g.GameTags).ThenInclude(gt => gt.Tag)
                .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            return game == null ? null : mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> UpdateAsync(Guid id, GameDto gameDto, CancellationToken cancellationToken = default)
        {
            Game? game = await dbContext.Games.FindAsync(new object[] { id }, cancellationToken);
            if (game == null) throw new Exception("Jeu introuvable");

            // TODO: ADD METHOD INTO GAME ENTITY TO CREATE GAME
            game.Titre = gameDto.Titre;
            game.Description = gameDto.Description;
            game.DateSortie = gameDto.DateSortie;
            game.Editeur = gameDto.Editeur;
            game.ImagePath = gameDto.ImagePath;

            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<GameDto>(game);
        }
    }
}