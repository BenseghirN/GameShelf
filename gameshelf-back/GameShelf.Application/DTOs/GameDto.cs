namespace GameShelf.Application.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DateSortie { get; set; }
        public string? Editeur { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new();
        public List<string> Platforms { get; set; } = new();
    }
}