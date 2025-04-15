using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données pour ajouter un jeu à la bibliothèque de l'utilisateur.
    /// </summary>
    public class AddToLibraryDto
    {
        /// <summary>Statut du jeu (possédé, terminé, etc.).</summary>
        public GameStatus Statut { get; set; } = GameStatus.Possede;
        /// <summary>Note personnelle (facultative).</summary>
        public int? Note { get; set; }
        /// <summary>Chemin de l'image personnalisée fournie par l'utilisateur (facultatif).</summary>
        public string? ImagePersoPath { get; set; }
    }
}