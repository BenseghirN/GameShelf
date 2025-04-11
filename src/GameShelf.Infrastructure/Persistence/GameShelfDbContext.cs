using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class GameShelfDbContext : DbContext
{
    public GameShelfDbContext(DbContextOptions<GameShelfDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<GameTag> GameTags => Set<GameTag>();
    public DbSet<Platform> Platforms => Set<Platform>();
    public DbSet<GamePlatform> GamePlatforms => Set<GamePlatform>();
    public DbSet<UserGame> UserGames => Set<UserGame>();
    public DbSet<UserProposal> UserCreatedGames => Set<UserProposal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Exemple de PK composite
        modelBuilder.Entity<GameTag>()
            .HasKey(gt => new { gt.GameId, gt.TagId });

        modelBuilder.Entity<GamePlatform>()
            .HasKey(gp => new { gp.GameId, gp.PlatformId });

        modelBuilder.Entity<UserGame>()
            .HasKey(ug => new { ug.UserId, ug.GameId });
    }
}
