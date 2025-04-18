namespace GameShelf.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        public ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();

        public void CreateNew(string nom)
        {
            Nom = nom;
        }

        public void Update(string newName)
        {
            Nom = newName;
        }
    }
}