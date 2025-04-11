namespace GameShelf.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();
    }
}