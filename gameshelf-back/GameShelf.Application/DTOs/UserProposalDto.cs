using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente une proposition de jeu soumise par un utilisateur.
    /// </summary>
    public class UserProposalDto
    {
        /// <summary>Identifiant unique de la proposition.</summary>
        public Guid Id { get; set; }

        /// <summary>Identifiant de l'utilisateur ayant soumis la proposition.</summary>
        public Guid UserId { get; set; }

        /// <summary>Titre proposé pour le jeu.</summary>
        public string Titre { get; set; } = string.Empty;

        /// <summary>Identifiant de la plateforme sélectionnée pour le jeu.</summary>
        public Guid PlatformId { get; set; }

        /// <summary>Plateforme sélectionnée pour le jeu.</summary>
        public PlatformDto? Platform { get; set; } = null;

        /// <summary>Chemin de l'image associée à la proposition.</summary>
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>Date à laquelle la proposition a été soumise.</summary>
        public DateTime DateSoumission { get; set; }

        /// <summary>Statut actuel de la proposition (en attente, acceptée, refusée).</summary>
        public ProposalStatus Statut { get; set; } = ProposalStatus.EnAttente;
    }
}