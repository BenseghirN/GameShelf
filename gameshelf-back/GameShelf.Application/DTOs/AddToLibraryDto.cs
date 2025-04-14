using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    public class AddToLibraryDto
    {
        public GameStatus Statut { get; set; } = GameStatus.Possédé;
        public int? Note { get; set; }
        public string? ImagePersoPath { get; set; }
    }
}