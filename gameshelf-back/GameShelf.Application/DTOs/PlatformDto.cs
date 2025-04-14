namespace GameShelf.Application.DTOs
{
    public class PlatformDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
    }
}