namespace GameShelf.Domain.Entities
{
    public class Platform
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();

        public void CreateNew(string nom, string? imagePath)
        {
            Nom = nom;
            ImagePath = imagePath;
        }

        public void Update(string newNom, string? newImagePath)
        {
            Nom = newNom;
            ImagePath = newImagePath;
        }
    }
}