using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

public class GameShelfDbContext : DbContext, IGameShelfDbContext
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
    public DbSet<UserProposal> UserProposals => Set<UserProposal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Utilisation de GUIDs séquentiels pour les clés primaires
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty("Id");

            if (idProperty is not null && idProperty.ClrType == typeof(Guid))
            {
                idProperty.SetValueGeneratorFactory((p, e) => new SequentialGuidValueGenerator());
            }
        }

        modelBuilder.Entity<Game>()
            .Property(g => g.DateSortie)
            .HasConversion(
                v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null
            );

        // PK composées pour les entités de jointure
        modelBuilder.Entity<GameTag>()
            .HasKey(gt => new { gt.GameId, gt.TagId });

        modelBuilder.Entity<GamePlatform>()
            .HasKey(gp => new { gp.GameId, gp.PlatformId });

        modelBuilder.Entity<UserGame>()
            .HasKey(ug => new { ug.UserId, ug.GameId });

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<UserGame>()
            .Property(ug => ug.Statut)
            .HasConversion<string>();

        modelBuilder.Entity<UserProposal>()
            .Property(p => p.Statut)
            .HasConversion<string>();
    }
}
