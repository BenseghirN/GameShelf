namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Représente un tag utilisé pour catégoriser un jeu (ex : RPG, multijoueur).
    /// </summary>
    public class TagDto
    {
        /// <summary>Identifiant unique du tag.</summary>
        public Guid Id { get; set; }

        /// <summary>Nom du tag.</summary>
        public string Nom { get; set; } = string.Empty;
    }
}