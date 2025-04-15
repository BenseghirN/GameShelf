using GameShelf.Domain.Enums;

namespace GameShelf.Domain.Entities
{
    public class UserProposal
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public string Titre { get; set; } = string.Empty;
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; } = default!;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime DateSoumission { get; set; } = DateTime.UtcNow;
        public ProposalStatus Statut { get; set; } = ProposalStatus.EnAttente;

        public void Reject()
        {
            Statut = ProposalStatus.Refusee;
        }

        public void SetWaiting()
        {
            Statut = ProposalStatus.EnAttente;
        }
    }
}