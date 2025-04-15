namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente une plateforme de jeu (ex : PC, PS5, Switch).
    /// </summary>
    public class PlatformDto
    {
        /// <summary>Identifiant unique de la plateforme.</summary>
        public Guid Id { get; set; }

        /// <summary>Nom de la plateforme.</summary>
        public string Nom { get; set; } = string.Empty;

        /// <summary>Chemin de l'image associée à la plateforme.</summary>
        public string? ImagePath { get; set; }
    }
}