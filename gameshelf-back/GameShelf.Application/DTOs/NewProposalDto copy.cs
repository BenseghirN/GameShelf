namespace GameShelf.Application.DTOs
{
    public class NewProposalDto
    {
        public string Titre { get; set; } = string.Empty;
        public Guid PlatformId { get; set; }
        public DateTime DateSoumission { get; set; } = DateTime.UtcNow;
    }
}