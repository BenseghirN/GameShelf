namespace GameShelf.Application.DTOs
{
    public class UserProposalDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Titre { get; set; } = string.Empty;
        public Guid PlatformId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime DateSoumission { get; set; }
        public string Statut { get; set; } = "en attente";
    }
}