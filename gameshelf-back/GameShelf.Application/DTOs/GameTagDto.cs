namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente l'association entre un jeu et un tag.
    /// </summary>
    public class GameTagDto
    {
        /// <summary>Identifiant du jeu.</summary>
        public Guid GameId { get; set; }

        /// <summary>Identifiant du tag.</summary>
        public Guid TagId { get; set; }
    }
}
