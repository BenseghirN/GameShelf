namespace GameShelf.Domain.Entities
{
    public class UserGame
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public string Statut { get; set; } = "A voir"; // Vu, Possédé...
        public int? Note { get; set; }
        public string? ImagePersoPath { get; set; }
    }
}