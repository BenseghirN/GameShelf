namespace GameShelf.Domain.Entities
{
    public class Platform
    {
        public Guid Id { get; set; }
        public string NomPlateforme { get; set; } = string.Empty;

        public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
    }
}