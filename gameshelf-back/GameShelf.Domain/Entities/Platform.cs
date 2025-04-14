namespace GameShelf.Domain.Entities
{
    public class Platform
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}