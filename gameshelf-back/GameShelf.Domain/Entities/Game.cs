namespace GameShelf.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DateSortie { get; set; }
        public string Editeur { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
        public ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();
        public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();

        public void CreateNew(
            string titre,
            string? description,
            DateTime? dateSortie,
            string? editeur,
            string? imagePath
        )
        {
            Titre = titre;
            Description = description ?? string.Empty;
            DateSortie = dateSortie;
            Editeur = editeur ?? string.Empty;
            ImagePath = imagePath ?? string.Empty;
        }

        public void Update(
            string titre,
            string? description,
            DateTime? dateSortie,
            string? editeur,
            string? imagePath)
        {
            Titre = titre;
            Description = description ?? string.Empty;
            DateSortie = dateSortie;
            Editeur = editeur ?? string.Empty;
            ImagePath = imagePath ?? string.Empty;
        }

        public void AddTags(IEnumerable<Tag> tagsToAdd)
        {
            foreach (Tag tag in tagsToAdd)
            {
                if (!GameTags.Any(gt => gt.TagId == tag.Id))
                {
                    GameTags.Add(new GameTag
                    {
                        GameId = Id,
                        TagId = tag.Id,
                        Tag = tag
                    });
                }
            }
        }

        public void RemoveTag(Guid tagId)
        {
            GameTag? tagToRemove = GameTags.FirstOrDefault(gt => gt.TagId == tagId);
            if (tagToRemove != null)
            {
                GameTags.Remove(tagToRemove);
            }
        }

        public void AddPlatforms(IEnumerable<Platform> platformsToAdd)
        {
            foreach (Platform platform in platformsToAdd)
            {
                if (!GamePlatforms.Any(gp => gp.PlatformId == platform.Id))
                {
                    GamePlatforms.Add(new GamePlatform
                    {
                        GameId = this.Id,
                        PlatformId = platform.Id,
                        Platform = platform
                    });
                }
            }
        }

        public void RemovePlatform(Guid platformId)
        {
            GamePlatform? platformToRemove = GamePlatforms.FirstOrDefault(gp => gp.PlatformId == platformId);
            if (platformToRemove != null)
            {
                GamePlatforms.Remove(platformToRemove);
            }
        }

    }
}
