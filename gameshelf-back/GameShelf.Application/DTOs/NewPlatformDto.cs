namespace GameShelf.Application.DTOs
{
    /// <summary>
    /// Données envoyées par un administrateur pour ajouter ou editer une plateforme.
    /// </summary>
    public class NewPlatformDto
    {

        /// <summary>Nom de la plateforme.</summary>
        public string Nom { get; set; } = string.Empty;

        /// <summary>Chemin de l'image associée à la plateforme.</summary>
        public string ImagePath { get; set; } = string.Empty;
    }
}