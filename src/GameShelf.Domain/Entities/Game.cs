namespace GameShelf.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; } = DateTime.UtcNow;
        public bool EstActif { get; set; } = true;

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
        public ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();
        public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}
