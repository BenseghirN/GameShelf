using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class GameService(IGameShelfDbContext dbContext, IMapper mapper) : IGameService
    {
        public async Task<GameDto> CreateAsync(NewGameDto gameDto, CancellationToken cancellationToken = default)
        {
            Game game = new Game();
            game.CreateNew(gameDto.Titre, gameDto.Description, gameDto.DateSortie, gameDto.Editeur, gameDto.ImagePath);
            if (gameDto.TagIds?.Any() == true)
            {
                List<Tag> tags = await dbContext.Tags
                    .Where(t => gameDto.TagIds.Contains(t.Id))
                    .ToListAsync(cancellationToken);

                game.AddTags(tags);
            }

            if (gameDto.PlatformIds?.Any() == true)
            {
                List<Platform> platforms = await dbContext.Platforms
                    .Where(p => gameDto.PlatformIds.Contains(p.Id))
                    .ToListAsync(cancellationToken);

                game.AddPlatforms(platforms);
            }
            await dbContext.Games.AddAsync(game, cancellationToken);
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

        public async Task<GameDto> UpdateAsync(Guid id, NewGameDto gameDto, CancellationToken cancellationToken = default)
        {
            Game? game = await dbContext.Games
                .Include(g => g.GameTags)
                .Include(g => g.GamePlatforms)
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
            if (game == null) throw new Exception("Jeu introuvable");

            game.Update(gameDto.Titre, gameDto.Description, gameDto.DateSortie, gameDto.Editeur, gameDto.ImagePath);

            // Clean and reset Tags
            List<Tag> tags = await dbContext.Tags
                .Where(t => gameDto.TagIds.Contains(t.Id))
                .ToListAsync(cancellationToken);

            List<Guid> currentTagIds = game.GameTags.Select(gt => gt.TagId).ToList();
            IEnumerable<Guid> tagsToRemove = currentTagIds.Except(gameDto.TagIds);
            IEnumerable<Guid> tagsToAdd = gameDto.TagIds.Except(currentTagIds);

            foreach (Guid tagId in tagsToRemove)
                game.RemoveTag(tagId);

            IEnumerable<Tag> newTags = tags.Where(t => tagsToAdd.Contains(t.Id));
            game.AddTags(newTags);

            // Clean and reset Platforms
            List<Platform> platforms = await dbContext.Platforms
                .Where(p => gameDto.PlatformIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            List<Guid> currentPlatformIds = game.GamePlatforms.Select(gp => gp.PlatformId).ToList();
            IEnumerable<Guid> platformsToRemove = currentPlatformIds.Except(gameDto.PlatformIds);
            IEnumerable<Guid> platformsToAdd = gameDto.PlatformIds.Except(currentPlatformIds);

            foreach (Guid pid in platformsToRemove)
                game.RemovePlatform(pid);

            IEnumerable<Platform> newPlatforms = platforms.Where(p => platformsToAdd.Contains(p.Id));
            game.AddPlatforms(newPlatforms);

            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<GameDto>(game);
        }
    }
}