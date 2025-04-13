using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    public class UserGameDto
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public GameStatus Statut { get; set; } = GameStatus.Possédé;
        public int? Note { get; set; }
        public string? ImagePersoPath { get; set; }
        public DateTime DateAjout { get; set; }
    }
}