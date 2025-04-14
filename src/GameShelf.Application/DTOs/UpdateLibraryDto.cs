using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    public class UpdateLibraryDto
    {
        public GameStatus Statut { get; set; } = GameStatus.Possédé;
        public int? Note { get; set; }
    }
}