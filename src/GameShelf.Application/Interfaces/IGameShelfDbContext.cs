namespace GameShelf.Application.Interfaces
{
    using GameShelf.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IGameShelfDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Game> Games { get; }
        DbSet<Tag> Tags { get; }
        DbSet<GameTag> GameTags { get; }
        DbSet<Platform> Platforms { get; }
        DbSet<GamePlatform> GamePlatforms { get; }
        DbSet<UserGame> UserGames { get; }
        DbSet<UserProposal> UserCreatedGames { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}