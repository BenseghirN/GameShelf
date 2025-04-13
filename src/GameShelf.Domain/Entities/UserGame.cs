using GameShelf.Domain.Enums;

namespace GameShelf.Domain.Entities
{
    public class UserGame
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;
        public GameStatus Statut { get; set; } = GameStatus.Possédé;
        public int? Note { get; set; }
        public string? ImagePersoPath { get; set; }
        public DateTime DateAjout { get; set; } = DateTime.UtcNow;
    }
}