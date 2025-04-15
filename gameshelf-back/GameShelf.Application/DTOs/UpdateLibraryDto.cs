using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données pour mettre à jour un jeu dans la bibliothèque de l'utilisateur.
    /// </summary>
    public class UpdateLibraryDto
    {
        /// <summary>Statut du jeu (possédé, terminé, etc.).</summary>
        public GameStatus Statut { get; set; } = GameStatus.Possede;

        /// <summary>Note personnelle attribuée au jeu (facultative).</summary>
        public int? Note { get; set; }
    }
}