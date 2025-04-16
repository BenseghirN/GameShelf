namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données envoyées par un utilisateur pour proposer un nouveau jeu.
    /// </summary>
    public class NewProposalDto
    {
        /// <summary>Titre proposé pour le jeu.</summary>
        public string Titre { get; set; } = string.Empty;

        /// <summary>Plateforme principale associée à la proposition.</summary>
        public Guid PlatformId { get; set; }

        /// <summary>Date de soumission de la proposition.</summary>
        public DateTime DateSoumission { get; set; } = DateTime.UtcNow;
    }
}