namespace GameShelf.Application.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; }
        public bool EstActif { get; set; }
    }
}