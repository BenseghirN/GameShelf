namespace GameShelf.Application.DTOs
{
    public class UserGameDto
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string Statut { get; set; } = "A voir";
        public int? Note { get; set; }
        public string? ImagePersoPath { get; set; }
    }
}