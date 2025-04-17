using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente un jeu associé à un utilisateur dans sa bibliothèque personnelle.
    /// </summary>
    public class UserGameDto
    {
        /// <summary>Identifiant de l'utilisateur.</summary>
        public Guid UserId { get; set; }

        /// <summary>Identifiant du jeu.</summary>
        public Guid GameId { get; set; }

        /// <summary>Jeu.</summary>
        public GameDto? Game { get; set; } = null;

        /// <summary>Nom du jeu.</summary>
        public string GameName { get; set; } = string.Empty;

        /// <summary>Statut du jeu dans la collection (possédé, terminé, etc.).</summary>
        public GameStatus Statut { get; set; } = GameStatus.Possede;

        /// <summary>Note personnelle attribuée par l'utilisateur (facultative).</summary>
        public int? Note { get; set; }

        /// <summary>Chemin de l'image personnalisée définie par l'utilisateur (facultatif).</summary>
        public string? ImagePersoPath { get; set; }

        /// <summary>Date à laquelle le jeu a été ajouté à la bibliothèque.</summary>
        public DateTime DateAjout { get; set; }
    }
}