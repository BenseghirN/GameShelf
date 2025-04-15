namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente l'association entre un jeu et une plateforme.
    /// </summary>
    public class GamePlatformDto
    {
        /// <summary>Identifiant du jeu.</summary>
        public Guid GameId { get; set; }

        /// <summary>Identifiant de la plateforme.</summary>
        public Guid PlatformId { get; set; }
    }
}